using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health = 100;

    public DisplayBar healthBar;

    private Rigidbody2D rb;

    public float knockbackForce = 5f;

    public AudioClip deathSound;
    private AudioSource deathAudio;

    public GameObject playerDeathEffect;

    //static bool keeps track if player hit recently
    public static bool hitRecently;

    public float hitRecoveryTime = 0.2f;

    public AudioClip hitSound;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        //set rigid body reference
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        deathAudio = GetComponent<AudioSource>();
        //check if null
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on player");
        }
        //set max value of health bar to enemy health
        healthBar.SetMaxValue(health);

        //initialize hitRecently to false
        hitRecently = false;



    }


    // funtion to knock player back when collide with enemy
    public void Knockback(Vector3 enemyPosition)
    {
        if (hitRecently)
        {
            return;
        }

        hitRecently = true;

        if (gameObject.activeSelf)
        {
            //start coroutine to reset hit recently
            StartCoroutine(RecoverFromHit());
        }


        //calculate knockback direction
        Vector2 direction = transform.position - enemyPosition;

        //normalize direction vector
        // gives conisistent knockback force regardless of distance
        //between player and enemy
        direction.Normalize();

        //add upward direction to knockback
        direction.y = direction.y * 0.5f + 0.5f;

        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

    }

    //coroutine to reset hitRecently after hitRecoveryTime seconds
    IEnumerator RecoverFromHit()
    {
       //wait for hitRecoverytime (o.2) seconds
        yield return new WaitForSeconds(hitRecoveryTime);

        hitRecently = false;

        animator.SetBool("hit", false);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {

        //subtract damage from health
        health -= damage;

        //update health bar
        healthBar.SetValue(health);
        
        //ToDo: Play sfx and Animation when player takes damage

        //if health is less then or equal to 0
        if (health <= 0)
        {
            
            Die();
        }
        else 
        { 
            deathAudio.PlayOneShot(hitSound);

            animator.SetBool("hit", true);
        }
    }

    public void Die()
    {

        //set gameover to true
        ScoreManager.gameOver = true;

        //ToDo: play sfx and animation when player dies

        

        // Instantiate death effect at player's position
        GameObject deathEffect = Instantiate(playerDeathEffect, transform.position, Quaternion.identity);

        //destroy death effect after 2 seconds
        Destroy(deathEffect, 2f);
        //disable player object
        gameObject.SetActive(false);


    }


}
