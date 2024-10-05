using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMove : MonoBehaviour
{
    public float moveSpeed = 5f;    // Speed for movement
    public float jumpForce = 5f;    // Force applied when jumping
    private bool isGrounded;        // Boolean to check if the sphere is on the ground

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

        // Create a new Vector3 based on the input and apply speed
        Vector3 move = new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;

        // Move the sphere in the direction based on input
        transform.Translate(move);

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
        // Check if the sphere is colliding with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set isGrounded to true when touching the ground
        }
    }
}
