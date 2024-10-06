using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;                // Reference to the player's position
    public float detectionRange = 15f;      // Range within which the monster detects the player
    public float attackRange = 2f;          // Range within which the monster attacks the player
    public float moveSpeed = 5f;            // Movement speed of the monster
    public float attackCooldown = 2f;       // Time between attacks
    public int damageAmount = 10;           // Damage dealt per attack
    public int maxHealth = 100;             // Maximum health of the monster
    public int regenRate = 3;               // Amount of health regenerated per second
    public GameObject objective;            // The objective attached to the monster (e.g., key, goal item)

    private int currentHealth;              // Current health of the monster
    private float lastAttackTime = 0f;      // To track when the monster last attacked

    // Reference to the player's health script (if the player has one)
    private PlayerHealth playerHealth;

    void Start()
    {
        // Initialize the monster's health
        currentHealth = maxHealth;

        // Start the regeneration coroutine
        StartCoroutine(RegenerateHealth());

        // Get reference to player's health script (if exists)
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // If the monster is dead, don't do anything
        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        // Calculate distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Move towards the player
            MoveTowardsPlayer(distanceToPlayer);
        }
    }

    void MoveTowardsPlayer(float distanceToPlayer)
    {
        // Check if the monster is within attack range
        if (distanceToPlayer <= attackRange)
        {
            // Try to attack the player
            TryAttack();
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

    void TryAttack()
    {
        // Check if the attack cooldown has passed
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // Damage the player if the player has a health script
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Monster dealt 10 damage to the player.");
            }
        }
    }

    public void TakeDamage(int amount)
    {
        // Reduce the monster's health
        currentHealth -= amount;
        Debug.Log("Monster took " + amount + " damage. Current health: " + currentHealth);

        // Check if the monster's health has dropped to 0 or below
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Monster died.");
        StopCoroutine(RegenerateHealth());  // Stop regenerating health on death

        // Check if there is an objective attached to the monster
        if (objective != null)
        {
            // Destroy or disable the objective when the monster dies
            Destroy(objective); // Or you can use objective.SetActive(false) to disable it
        }

        // Destroy the monster GameObject
        Destroy(gameObject);
    }

    // Coroutine for health regeneration
    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            // Regenerate health every second, capped at max health
            if (currentHealth < maxHealth)
            {
                currentHealth += regenRate;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Make sure health doesn't exceed maxHealth
                Debug.Log("Monster regenerated health. Current health: " + currentHealth);
            }

            // Wait for 1 second before regenerating again
            yield return new WaitForSeconds(1f);
        }
    }
}