using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMove : MonoBehaviour
{
    // Speed of the sphere movement
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Get input from keyboard (WASD keys)
        float moveX = Input.GetAxis("Horizontal"); // A/D for left/right
        float moveZ = Input.GetAxis("Vertical");   // W/S for forward/backward

        // Create a new Vector3 based on the input and apply speed
        Vector3 move = new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;

        // Move the sphere in the direction based on input
        transform.Translate(move);
    }
}
