using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public GameObject deathEffect;

    //reference to health bar
    private DisplayBar healthBar;


    public int damage = 10;


    private void Start()
    {
        //find heath bar in children of enemy
        healthBar = GetComponentInChildren<DisplayBar>();

        if (healthBar == null)
        {
            Debug.LogError("HealthBar (DisplayBar script) not found");
            return;
        }

        healthBar.SetMaxValue(health); 

    }


    // Start is called before the first frame update
    public void TakeDamage(int damage)
    {

        health -= damage;

        //update health bar
        healthBar.SetValue(health);

        if(health <= 0)
        {

            Die();
        }


    }

    // Update is called once per frame
    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth == null) 
            {
                Debug.LogError("PlayerHealth script not found on player");
                return;
            }

            playerHealth.TakeDamage(damage);

            playerHealth.Knockback(transform.position);

        }
    }
    

    
}
