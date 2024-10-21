using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] Transform target;  // Reference to the player or Rigidbody2D's transform

    [SerializeField] Transform boundStart;
    [SerializeField] Transform boundEnd;

    Vector2 minBounds; // Lower-left corner of the level
    Vector2 maxBounds; // Upper-right corner of the level
    [SerializeField] float smoothSpeed = 0.125f;  // Smooth following speed

    float cameraHalfWidth;
    float cameraHalfHeight;

    void Start()
    {
        // Calculate half the size of the camera view in world units
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

        minBounds = new Vector2(boundStart.position.x, boundEnd.position.y);
        maxBounds = new Vector2(boundEnd.position.x, boundStart.position.y);
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Smoothly follow the target's position
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Clamp the camera within level bounds
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x + cameraHalfWidth, maxBounds.x - cameraHalfWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

        // Set the new camera position
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}



