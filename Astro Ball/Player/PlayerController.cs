using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody _rb;

    [HideInInspector] public bool IsGround;
    [HideInInspector] public bool IsSloped;
    [HideInInspector] public bool IsBox;

    [SerializeField] private Transform _Cam;

    [SerializeField] private LayerMask _JumpableGround;
    [SerializeField] private LayerMask _JumpableSlope;
    [SerializeField] private LayerMask _GroundPound;

    [SerializeField] public float _MoveSpeed;
    [SerializeField] private float _MoveSpeedAir;
    [SerializeField] private float _MaxSpeed;
    [SerializeField] private float _JumpSpeed;
    [SerializeField] private float _JumpSpeedDown;
    [SerializeField] private float _JumpStartTime;

    [SerializeField] private AudioSource _jump;
    [SerializeField] private AudioClip _jumpEffect;

    private SpeedBoost _speedbooster;
    private JumpBoost _jumpBooster;

    private float _jumpTime;
    private bool _isJumping = false;

    private RaycastHit _hit;

    private float _xInput;
    private float _zInput;
    private float _timer;

    private bool Drag2ndAmount;

    private bool MusicFadeOutEnabled = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _speedbooster = FindObjectOfType<SpeedBoost>();
        _jumpBooster = FindObjectOfType<JumpBoost>();
        Drag2ndAmount = false;
    }

    void Update()
    {
        Movement();
        Jump();
        Drag();

        if (IsGrounded())
        {
            IsGround = true;
        }
        else
        {
            IsGround = false;
        }
        if (IsSlope())
        {
            IsSloped = true;
        }
        else
        {
            IsSloped = false;
        }
        if (IsBoxed())
        {
            IsBox = true;
        }
        else
        {
            IsBox = false;
        }
    }

    private void Movement()
    {

        _xInput = Input.GetAxis("Horizontal");
        _zInput = Input.GetAxis("Vertical");

        // Get the forward and right direction relative to the camera
        Vector3 camForward = _Cam.forward;
        Vector3 camRight = _Cam.right;

        camForward.y = 0;
        camRight.y = 0;

        // Calculate the forward and right movement relative to the camera
        Vector3 forwardRelative = _zInput * camForward;
        Vector3 rightRelative = _xInput * camRight;

        Vector3 moveDirection = forwardRelative + rightRelative;

        // if the player is on the ground or a slope
        if (IsGround || IsSloped || IsBox)
        {
            _rb.AddForce(new Vector3(moveDirection.x, 0, moveDirection.z).normalized * _MoveSpeed * Time.deltaTime);
            _MoveSpeed = Mathf.Min(_MoveSpeed, _MaxSpeed);
        }
        // if the player is in the air
        else if (!IsGround || !IsSloped)
        {
            _rb.AddForce(new Vector3(moveDirection.x, 0, moveDirection.z).normalized * _MoveSpeedAir * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (IsGround || IsSloped || IsBox)
        {
            if (Input.GetButtonDown("Jump"))
            {
                MusicFadeOutEnabled = false;
                if (!MusicFadeOutEnabled)
                {
                    _jump.volume = 1;
                    _jump.PlayOneShot(_jumpEffect);
                }

                _isJumping = true;
                _jumpTime = _JumpStartTime;
                Vector3 jump = new Vector3(0, 20, 0);
                _rb.AddForce(jump * _JumpSpeed * Time.deltaTime);
            }
        }
        if (Input.GetButton("Jump") && _isJumping)
        {
            if (_jumpTime > 0) // Apply a vertical force to the player as long as there's jump time remaining
            {
                Vector3 jump = new Vector3(0, 20, 0);
                _rb.AddForce(jump * _JumpSpeed * Time.deltaTime);
                _jumpTime -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            _isJumping = false;
            MusicFadeOutEnabled = true;
        }

        if (!_isJumping && !IsGround)
        {
            Vector3 jump = new Vector3(0, 20, 0);
            _rb.AddForce(jump * _JumpSpeedDown * Time.deltaTime);
        }

        if (MusicFadeOutEnabled)
        {
            if (_jump.volume <= 0.01f)
            {
                _jump.Stop();
                MusicFadeOutEnabled = false;
            }
            else
            {
                float newVolume = _jump.volume - (5f * Time.deltaTime);
                if (newVolume < 0f)
                {
                    newVolume = 0f;
                }
                _jump.volume = newVolume;
            }
        }
    }

    private void Drag()
    {
        if (IsGround)
        {
            if (_xInput == 0 && _zInput == 0 &&  // Check if the player is not providing any movement input, and if there are no speed or jump boosts active
                !_speedbooster.IsMoving && !_jumpBooster.IsJumping)
            {
                if (!Drag2ndAmount)
                {
                    _timer += Time.deltaTime;
                    if (_timer > 0.4f)
                    {
                        _rb.drag += 0.05f;
                        _timer = 0;
                    }
                }
            }
            if (_rb.drag > 0.5f)
            {
                Drag2ndAmount = true;  // Set Drag2ndAmount to true, indicating that the second stage of drag can now be applied
            }
            if (Drag2ndAmount)
            {
                _timer += Time.deltaTime;
                if (_timer > 0.2f)
                {
                    _rb.drag += 0.05f;
                    _timer = 0;
                }
            }
            if (_rb.drag > 1f)
            {
                _rb.drag = 1;
            }
        }

        // drag resets to 0;
        if (_xInput > 0 || _zInput > 0 || _xInput < 0 || _zInput < 0 ||
            Input.GetButtonDown("Jump") || _speedbooster.IsMoving || _jumpBooster.IsJumping)
        {
            _rb.drag = 0;
        }
    }


    // checks if the player is on on/over a type of surface
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out _hit, 0.3f, _JumpableGround);
    }

    private bool IsSlope()
    {
        return Physics.Raycast(transform.position, Vector3.down, out _hit, 0.3f, _JumpableSlope);
    }

    private bool IsBoxed()
    {
        return Physics.Raycast(transform.position, Vector3.down, out _hit, 5f, _GroundPound);
    }
}



