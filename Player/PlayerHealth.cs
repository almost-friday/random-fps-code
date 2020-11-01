using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float temp;

    private float currentHealth;
    public static float startingHealth;

    // Start is called before the first frame update
    void Awake()
    {
        startingHealth = 100;
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(float damageValue)
    {
        currentHealth -= damageValue;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }
}
