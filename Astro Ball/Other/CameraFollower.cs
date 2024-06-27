using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;

    public float distance = 10f;
    public float height = 5f;
    public float smoothSpeed = 0.1f;
    public float mouseSensitivity = 100f;

    [SerializeField] private LayerMask walls;
    [SerializeField] private Transform _AudioListener;

    [SerializeField] private float _Fovsensitivity;

    private float _minFov = 35f;
    private float _maxFov = 60f;

    private float rotationX = 0f;

    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0f, height, -distance);
    }

    private void LateUpdate()
    {
        _AudioListener.position = target.position;

        //input camera rotation
        if (Input.GetMouseButton(1))
        {
            rotationX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        }

        Quaternion rotation = Quaternion.Euler(0, rotationX, 0f);
        Vector3 Position = target.position + (rotation * offset);

        RaycastHit hit;

        //check if theres an object between the camera and the player.
        if (Physics.Linecast(target.position, Position, out hit, walls))
        {
            Vector3 adjustedPosition = hit.point + (hit.normal * 0.2f);
            transform.position = Vector3.Lerp(transform.position, adjustedPosition, smoothSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, Position, smoothSpeed * Time.deltaTime);
        }

        transform.LookAt(target);

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -_Fovsensitivity;
        fov = Mathf.Clamp(fov, _minFov, _maxFov);
        Camera.main.fieldOfView = fov;
    }

    //private void OnApplicationFocus(bool focus) // Lock or unlock the cursor when the application gains or loses focus
    //{
    //    if (focus)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //}
}
