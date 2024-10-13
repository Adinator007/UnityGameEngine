using UnityEngine;

public class TeleportOnTouch : MonoBehaviour
{
    public Transform teleportDestination; // The destination to teleport to

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player")) // Correctly checking the tag
        {
            // Teleport the player to the destination
            other.transform.position = teleportDestination.position;
        }
    }
}
