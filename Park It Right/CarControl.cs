using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public enum groundCheck { rayCast, sphereCaste };
    public enum MovementMode { Velocity, AngularVelocity };
    public MovementMode movementMode;
    public groundCheck GroundCheck;
    public LayerMask drivableSurface;

    public float MaxSpeed, accelaration, turn, gravity = 7f, downforce = 5f;

    public bool AirControl = false;

    public Rigidbody rb, carBody;

    [HideInInspector]
    public RaycastHit hit;
    public AnimationCurve frictionCurve;
    public AnimationCurve turnCurve;
    public PhysicMaterial frictionMaterial;
    [Header("Visuals")]
    public Transform BodyMesh;
    public Transform[] FrontWheels = new Transform[2];
    public Transform[] RearWheels = new Transform[2];
    [HideInInspector]
    public Vector3 carVelocity;

    [Range(0, 10)]
    public float BodyTilt;

    [Header("Audio settings")]
    public AudioSource engineSound;
    [Range(0, 1)]
    public float minPitch;

    [Range(1, 3)]
    public float maxPitch;

    public AudioSource SkidSound;

    [HideInInspector]
    public float skidWidth;


    [HideInInspector] public float radius = 2f, horizontalInput, verticalInput;
    private Vector3 origin;

    private CarSpawn carSpawn;

    private void Start()
    {
        radius = rb.GetComponent<SphereCollider>().radius;
        carSpawn = FindObjectOfType<CarSpawn>();

        if (movementMode == MovementMode.AngularVelocity)
        {
            Physics.defaultMaxAngularSpeed = 100;
        }
    }
    private void Update()
    {
        if (!carSpawn.frozenState)
        {
            horizontalInput = Input.GetAxis("Horizontal"); //turning input
            verticalInput = Input.GetAxis("Vertical");     //accelaration input
            Visuals();
        }

        AudioManager();

        if (!grounded())
        {
            AirControl = true;
        }
        else
        {
            AirControl = false;
        }
    }
    public void AudioManager()
    {
        float targetPitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Abs(carVelocity.z) / MaxSpeed);

        engineSound.pitch = Mathf.MoveTowards(engineSound.pitch, targetPitch, Time.deltaTime * 2f);

        if (Mathf.Abs(carVelocity.x) > 10 && grounded())
        {
            SkidSound.mute = false;
        }
        else
        {
            SkidSound.mute = true;
        }

        if (AirControl && !grounded())
        {
            engineSound.volume = Mathf.Lerp(engineSound.volume, 0f, Time.deltaTime * 5f);
        }
        else if (!AirControl && grounded())
        {
            engineSound.volume = Mathf.Lerp(engineSound.volume, 1f, Time.deltaTime * 5f);
        }
    }


    void FixedUpdate()
    {
        carVelocity = carBody.transform.InverseTransformDirection(carBody.velocity);

        if (Mathf.Abs(carVelocity.x) > 0)
        {
            // changes friction according to sideways speed of car
            frictionMaterial.dynamicFriction = frictionCurve.Evaluate(Mathf.Abs(carVelocity.x / 100));
        }

        if (grounded())
        {
            // turn logic
            float sign = Mathf.Sign(carVelocity.z);
            float TurnMultiplier = turnCurve.Evaluate(carVelocity.magnitude / MaxSpeed);

            if (verticalInput > 0.1f || carVelocity.z > 1)
            {
                carBody.AddTorque(Vector3.up * horizontalInput * sign * turn * 100 * TurnMultiplier);
            }
            else if (verticalInput < -0.1f || carVelocity.z < -1)
            {
                carBody.AddTorque(Vector3.up * horizontalInput * sign * turn * 100 * TurnMultiplier);
            }

            // acceleration logic
            if (movementMode == MovementMode.AngularVelocity)
            {
                if (Mathf.Abs(verticalInput) > 0.1f)
                {
                    rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, carBody.transform.right * verticalInput * MaxSpeed / radius, accelaration * Time.deltaTime);
                }
            }
            else if (movementMode == MovementMode.Velocity)
            {
                if (Mathf.Abs(verticalInput) > 0.1f)
                {
                    rb.velocity = Vector3.Lerp(rb.velocity, carBody.transform.forward * verticalInput * MaxSpeed, accelaration / 10 * Time.deltaTime);
                }
            }

            if (Input.GetKey(KeyCode.JoystickButton6) || Input.GetKey(KeyCode.S))
            {
                Vector3 forwardVelocity = transform.forward * Vector3.Dot(rb.velocity, transform.forward);

                // Calculate the brake force to apply
                float brakeForceToApply = 2000 * Time.fixedDeltaTime;

                // Limit the brake force to avoid abrupt stops
                if (forwardVelocity.magnitude < 5)
                {
                    brakeForceToApply = Mathf.Min(brakeForceToApply, forwardVelocity.magnitude / Time.fixedDeltaTime);
                }

                // Apply the brake force smoothly
                Vector3 brakeForceVector = -forwardVelocity.normalized * brakeForceToApply;
                rb.AddForce(brakeForceVector * 2, ForceMode.Acceleration);
            }
            // down force
            rb.AddForce(-transform.up * downforce * rb.mass);

            // body tilt
            carBody.MoveRotation(Quaternion.Slerp(carBody.rotation, Quaternion.FromToRotation(carBody.transform.up, hit.normal) * carBody.transform.rotation, 0.12f));
        }
        else
        {
            if (AirControl)
            {
                float TurnMultiplier = turnCurve.Evaluate(carVelocity.magnitude / MaxSpeed);
                carBody.AddTorque(Vector3.up * horizontalInput * turn * 100 * TurnMultiplier);
            }


            carBody.MoveRotation(Quaternion.Slerp(carBody.rotation, Quaternion.FromToRotation(carBody.transform.up, Vector3.up) * carBody.transform.rotation, 0.02f));


            rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity + Vector3.down * gravity, Time.deltaTime * gravity);

        }

        if (carSpawn.isDrowning)
        {
            rb.drag = 20;
        }
        else
        {
            rb.drag = 0;
        }
    }

    public void Visuals()
    {

        //tires
        foreach (Transform FW in FrontWheels)
        {
            FW.localRotation = Quaternion.Slerp(FW.localRotation, Quaternion.Euler(FW.localRotation.eulerAngles.x,
                               30 * horizontalInput, FW.localRotation.eulerAngles.z), 0.7f * Time.deltaTime / Time.fixedDeltaTime);
            FW.GetChild(0).localRotation = rb.transform.localRotation;
        }
        RearWheels[0].localRotation = rb.transform.localRotation;
        RearWheels[1].localRotation = rb.transform.localRotation;

        //Body
        if (carVelocity.z > 1)
        {
            BodyMesh.localRotation = Quaternion.Slerp(BodyMesh.localRotation, Quaternion.Euler(Mathf.Lerp(0, -5, carVelocity.z / MaxSpeed),
                               BodyMesh.localRotation.eulerAngles.y, BodyTilt * horizontalInput), 0.4f * Time.deltaTime / Time.fixedDeltaTime);
        }
        else
        {
            BodyMesh.localRotation = Quaternion.Slerp(BodyMesh.localRotation, Quaternion.Euler(0, 0, 0), 0.4f * Time.deltaTime / Time.fixedDeltaTime);
        }


    }

    public float groundCheckDistance = 1.0f;

    public bool grounded()
    {
        origin = rb.position + rb.GetComponent<SphereCollider>().radius * Vector3.up;
        var direction = -transform.up;
        var maxdistance = rb.GetComponent<SphereCollider>().radius + 0.2f;

        if (GroundCheck == groundCheck.rayCast)
        {
            if (Physics.Raycast(rb.position, Vector3.down, out hit, maxdistance, drivableSurface))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (GroundCheck == groundCheck.sphereCaste)
        {
            if (Physics.SphereCast(origin, radius + 0.1f, direction, out hit, maxdistance, drivableSurface))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    [SerializeField] private AudioSource carCrashFence;
    [SerializeField] private AudioClip carCrashFenceClip;

    private bool hasPlayedSound = false;

    private IEnumerator PlayCrashSoundDelayed()
    {
        hasPlayedSound = true;
        carCrashFence.PlayOneShot(carCrashFenceClip);

        // Wait for a short duration before allowing the sound to play again
        yield return new WaitForSeconds(1f); // Adjust the duration as needed

        hasPlayedSound = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!hasPlayedSound && other.gameObject.CompareTag("Fence") && rb.velocity.magnitude > 1)
        {
            float relativeVelocity = other.relativeVelocity.magnitude;

            // Map the relative velocity to a pitch range
            float pitchRange = 1 - 0.1f;
            float velocityRange = 50 - 1;

            // Calculate the mapped pitch (reverse the mapping)
            float mappedPitch = 0.1f + ((relativeVelocity - 1) / velocityRange) * pitchRange;

            mappedPitch = Mathf.Min(mappedPitch, 1f);

            carCrashFence.volume = mappedPitch;

            StartCoroutine(PlayCrashSoundDelayed());
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the ground check raycast
        Vector3 raycastStart = rb.position;
        Vector3 raycastEnd = rb.position - transform.up * (radius + 0.2f);

        if (grounded())
        {
            // If the car is grounded, set the end of the line to the hit point
            raycastEnd = hit.point;
        }

        // Draw the line
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(raycastStart, raycastEnd);
    }
}
