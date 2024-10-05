using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMove : MonoBehaviour
{
    public float moveSpeed = 5f;    // Speed for movement
    public float jumpForce = 5f;    // Force applied when jumping

    private Rigidbody rb;           // Reference to Rigidbody component

    void Start()
    {
        // Get the Rigidbody component attached to the sphere
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from keyboard (WASD keys)
        float moveX = Input.GetAxis("Horizontal"); // A/D for left/right
        float moveZ = Input.GetAxis("Vertical");   // W/S for forward/backward

        // Maintain current vertical velocity while setting horizontal velocity
        Vector3 velocity = rb.velocity;
        velocity.x = moveX * moveSpeed;
        velocity.z = moveZ * moveSpeed;

        // Update Rigidbody's velocity to apply movement
        rb.velocity = velocity;

        // Jump when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  // Apply upward force for jumping
        }
    }
}