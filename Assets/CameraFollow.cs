using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Reference to the target (sphere)
    public Transform target;

    // Offset between the camera and the target (nicely positioned behind and above)
    public Vector3 offset = new Vector3(0, 5, -10);

    // Smoothness factor for camera movement
    public float smoothSpeed = 0.125f;

    void Start()
    {
        // Find the sphere by name or tag and set it as the target
        GameObject sphere = GameObject.Find("Sphere"); // Assuming the sphere is named "Sphere"
        if (sphere != null)
        {
            target = sphere.transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Check if the target is assigned
        if (target != null)
        {
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
