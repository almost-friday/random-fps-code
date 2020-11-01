using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls Enemy Health, if you couldn't tell.
// Methods:
// TakeDamage(float damage) // reduces the enemy's current health by damage.

public class EnemyHealth : MonoBehaviour
{
    public readonly static float StartingHealth = 100; // THIS MUST BE CHANGED LATER!
    private float currentHealth;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = StartingHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
