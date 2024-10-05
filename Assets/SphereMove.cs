using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMove : MonoBehaviour
{
    public float moveSpeed = 5f;         // Normal movement speed
    public float sprintMultiplier = 2f;  // Speed multiplier when sprinting
    public float jumpForce = 5f;         // Force applied when jumping
    private bool isGrounded;             // Checks if the sphere is on the ground

    private Rigidbody rb;                // Reference to Rigidbody component

    void Start()
    {
        // Get the Rigidbody component attached to the sphere
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from keyboard (WASD keys)
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        // Check if the sprint key (Shift) is held down
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Calculate the current movement speed
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

        // Maintain current vertical velocity while setting horizontal velocity
        Vector3 velocity = rb.velocity;
        velocity.x = moveX * currentSpeed;
        velocity.z = moveZ * currentSpeed;

        // Update Rigidbody's velocity to apply movement
        rb.velocity = velocity;

        // Jump when the space bar is pressed and the sphere is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Set isGrounded to false when jumping
        }
    }

    // Detect if the sphere is touching the ground
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the sphere is colliding with an object tagged "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set isGrounded to true when touching the ground
        }
    }
}