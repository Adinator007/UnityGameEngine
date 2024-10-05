using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Camera playerCamera;               // Camera from which we shoot the grappling hook
    public Transform player;                  // Player transform (to teleport)
    public float maxGrappleDistance = 50f;    // Maximum distance for grappling
    public float grappleSpeed = 10f;          // Speed at which the player is pulled toward the hit point
    public LineRenderer chainRenderer;        // Line renderer to simulate chain
    public LayerMask grappleMask;             // Layers that the grappling hook can hit

    private bool isGrappling = false;
    private Vector3 grapplePoint;

    void Start()
    {
        // Disable the chain at the start
        chainRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click to grapple
        {
            StartGrapple();
        }

        if (isGrappling)
        {
            MoveToGrapplePoint();
        }
    }

    void StartGrapple()
    {
        // Cast a ray from the center of the screen
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // Check if the ray hits something within the maximum grapple distance and on the grappleMask layer
        if (Physics.Raycast(ray, out RaycastHit hit, maxGrappleDistance, grappleMask))
        {
            // We hit a valid surface, start the grappling process
            isGrappling = true;
            grapplePoint = hit.point;

            // Enable the chain and set its start and end positions
            chainRenderer.enabled = true;
            chainRenderer.SetPosition(0, player.position);  // Start from the player's position
            chainRenderer.SetPosition(1, grapplePoint);     // End at the grapple point
        }
    }

    void MoveToGrapplePoint()
    {
        // Move the player towards the grapple point
        player.position = Vector3.MoveTowards(player.position, grapplePoint, grappleSpeed * Time.deltaTime);

        // Update the chain's position to follow the player
        chainRenderer.SetPosition(0, player.position);  // Start position of the chain
        chainRenderer.SetPosition(1, grapplePoint);     // End position remains at the grapple point

        // Stop grappling if we have reached the target
        if (Vector3.Distance(player.position, grapplePoint) < 1f)
        {
            StopGrapple();
        }
    }

    void StopGrapple()
    {
        isGrappling = false;
        chainRenderer.enabled = false;  // Disable the chain when done
    }
}