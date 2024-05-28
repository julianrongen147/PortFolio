using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMover : MonoBehaviour
{
    // The target position for the game object to move to
    public Transform targetTransform;
    public Transform lookTarget;

    // The speed at which the game object should move
    public float smoothSpeed = 0.125f;

    // The maximum distance that the game object can be from the target position before it snaps to it
    public float snapDistance = 0.1f;

    private void Update()
    {
        // Calculate the current position and the position we want the game object to be in
        Vector3 currentPosition = transform.position;
        Vector3 target = targetTransform.position;
        Vector3 look = lookTarget.position;
        Vector3 easedPosition = Vector3.Lerp(currentPosition, look, smoothSpeed * Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(look - currentPosition) * Quaternion.Euler(0f, 180f, 0f);

        // Smoothly rotate the game object towards the target rotatio
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        // Move the game object to the eased position
        transform.position = easedPosition;
        // If the game object is close enough to the target position, snap it to the target position
        if (Vector3.Distance(currentPosition, target) <= snapDistance)
        {
            //transform.position = target;
        }
        else
        {
            // Otherwise, move the game object towards the target position
            Vector3 direction = (target - currentPosition).normalized;
            transform.Translate(direction * smoothSpeed * Time.deltaTime);
        }
    }
}
