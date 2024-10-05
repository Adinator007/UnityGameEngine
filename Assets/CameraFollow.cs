using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Reference to the target (sphere)
    public Transform target;

    // Offset between the camera and the target for TPS mode
    public Vector3 offset = new Vector3(0, 5, -10);

    // Smoothness factor for camera movement
    public float smoothSpeed = 0.125f;

    // Flag to determine if we're in FPS or TPS mode
    private bool isFPS = false;

    // Camera offset for FPS mode (usually minimal offset)
    public Vector3 fpsOffset = new Vector3(0, 0.5f, 0); // Adjust as needed

    void Start()
    {
        // Find the sphere by name or tag and set it as the target
        if (target == null)
        {
            GameObject sphere = GameObject.Find("Sphere"); // Assuming the sphere is named "Sphere"
            if (sphere != null)
            {
                target = sphere.transform;
            }
            else
            {
                Debug.LogError("CameraFollow: Sphere not found. Please assign the target manually.");
            }
        }
    }

    void Update()
    {
        // Toggle between FPS and TPS modes when the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFPS = !isFPS;
        }
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        // Check if the target is assigned
        if (target != null)
        {
            if (isFPS)
            {
                // First-Person View
                // Position the camera at the target's position with fpsOffset
                transform.position = target.position + fpsOffset;

                // Match the camera's rotation to the target's rotation
                transform.rotation = target.rotation;
            }
            else
            {
                // Third-Person View
                // Calculate the desired position of the camera
                Vector3 desiredPosition = target.position + offset;

                // Smoothly move the camera to the desired position
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

                // Set the camera's position to the smoothed position
                transform.position = smoothedPosition;

                // Optional: Keep the camera looking at the target
                transform.LookAt(target);
            }
        }
    }
}