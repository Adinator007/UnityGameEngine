using UnityEngine;
using System.Collections; // Add this to fix the IEnumerator issue

public class TeleportToSpawn : MonoBehaviour
{
    public Transform spawnPoint; // Reference to the spawn point
    public float movementSpeed = 5f; // Default movement speed
    private bool isMoving = true; // Flag to check if the player can move

    void Start()
    {
        GameObject spawnPointObject = GameObject.Find("SpawnPoint");

        if (spawnPointObject != null)
        {
            spawnPoint = spawnPointObject.transform;
        }
        else
        {
            Debug.LogError("Spawn Point with ID 'SpawnPoint' not found!");
        }
    }

    void Update()
    {
        // Check if the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            Teleport(); // Call the teleport method
        }

        // Handle movement if the player is allowed to move
        if (isMoving)
        {
            // Add your movement code here
            // Example: Move the player based on input
            float moveHorizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
            float moveVertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
            transform.Translate(new Vector3(moveHorizontal, 0, moveVertical));
        }
    }

    void Teleport()
    {
        if (spawnPoint != null)
        {
            // Log current position and spawn point position
            Debug.Log($"Current Position: {transform.position}, Spawn Point Position: {spawnPoint.position}");

            // Teleport the player to the spawn point's position and rotation
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation; // Optional: keep the same rotation

            // Log confirmation of teleportation
            Debug.Log("Teleported to spawn point.");

            // Start coroutine to stop movement
            StartCoroutine(StopMovementTemporarily());
        }
        else
        {
            Debug.LogWarning("Spawn point is not assigned!");
        }
    }

    private IEnumerator StopMovementTemporarily()
    {
        isMoving = false; // Disable movement
        Debug.Log("Movement stopped for 3 seconds.");

        yield return new WaitForSeconds(3); // Wait for 3 seconds

        isMoving = true; // Re-enable movement
        Debug.Log("Movement resumed.");
    }
}
