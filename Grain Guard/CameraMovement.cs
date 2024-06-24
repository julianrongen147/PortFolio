using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private float zoomStep, minCamSize, maxCamSize;
    [SerializeField] private float edgeScrollSpeed;

    private Vector3 dragOrigin;

    [SerializeField] private SpriteRenderer mapRenderer;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;
        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private void Update()
    {
        PanCamera();
        Zoom();
        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private void PanCamera()
    {
        Vector3 moveDirection = Vector3.zero;

        // Edge scrolling
        if (Input.mousePosition.x <= 0)
        {
            moveDirection += Vector3.left;
        }
        else if (Input.mousePosition.x >= Screen.width - 1)
        {
            moveDirection += Vector3.right;
        }

        if (Input.mousePosition.y <= 0)
        {
            moveDirection += Vector3.down;
        }
        else if (Input.mousePosition.y >= Screen.height - 1)
        {
            moveDirection += Vector3.up;
        }

        // Normalize the move direction and add speed
        if (moveDirection != Vector3.zero)
        {
            moveDirection = moveDirection.normalized * edgeScrollSpeed * Time.deltaTime;
            cam.transform.position += moveDirection;
        }

        // Mouse drag panning
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference;
        }
    }

    private void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newSize = cam.orthographicSize - scrollInput * zoomStep * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
