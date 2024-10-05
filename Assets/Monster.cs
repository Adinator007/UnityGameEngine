using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform player;                // Reference to the player's position
    
    public float detectionRange = 15f;      // Range within which the monster detects the player
    public float followDistance = 2f;       // Distance at which the monster starts circling the player
    public float moveSpeed = 5f;            // Movement speed of the monster

    void Start(){
        GameObject playerObject = GameObject.Find("Sphere");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player with ID 'Sphere' not found!");
        }
    }
    
    void Update()
    {
        // Calculate distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Move towards the player or circle around the player
            MoveTowardsOrAroundPlayer(distanceToPlayer);
        }
    }

    void MoveTowardsOrAroundPlayer(float distanceToPlayer)
    {
        // If within follow distance, circle around the player
        if (distanceToPlayer <= followDistance)
        {
            // Calculate a direction to move in a circular path around the player
            Vector3 direction = Vector3.Cross(Vector3.up, (player.position - transform.position).normalized);
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Optionally, make the monster look at the player
            transform.LookAt(player);
        }
    }
}
