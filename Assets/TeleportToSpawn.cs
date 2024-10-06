using UnityEngine;

public class TeleportToSpawn : MonoBehaviour
{
    public Transform spawnPoint; // Reference to the spawn point

    void Start() {
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
        }
        else
        {
            Debug.LogWarning("Spawn point is not assigned!");
        }
    }
}