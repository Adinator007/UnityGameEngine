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

    // Reference to the sphere's Rigidbody
    private Rigidbody targetRigidbody;

    // Smoothness factor for camera rotation
    public float rotationSmoothSpeed = 5f;

    // Zooming variables
    public float zoomSpeed = 2f;
    public float minZoom = 2f;
    public float maxZoom = 15f;
    private float currentZoom = 10f; // Start at the default zoom level

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

        // Get the Rigidbody component from the sphere
        if (target != null)
        {
            targetRigidbody = target.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        // Toggle between FPS and TPS modes when the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFPS = !isFPS;
        }

        // Handle zooming with the mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            currentZoom -= scrollInput * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
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
                // Position the camera at the target's position with fpsOffset adjusted by currentZoom
                transform.position = target.position + fpsOffset;

                // Get the sphere's velocity
                Vector3 velocity = targetRigidbody.velocity;
                velocity.y = 0;

                // If the sphere is moving, update the camera's rotation
                if (velocity.magnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
                }
            }
            else
            {
                // Third-Person View
                // Calculate the desired position of the camera with currentZoom
                Vector3 desiredPosition = target.position + offset.normalized * currentZoom;

                // Smoothly move the camera to the desired position
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;

                // Keep the camera looking at the target
                transform.LookAt(target);
            }
        }
    }
}
