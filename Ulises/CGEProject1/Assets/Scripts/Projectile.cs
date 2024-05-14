using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //component of projectile
    private Rigidbody2D rb;

    //projectile speed
    public float speed = 20f;

    public int damage = 20;


    public GameObject impactEffect;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
        
    }

    

    void OnTriggerEnter2D(Collider2D hitInfo)

    {
        // Get th Enemy component of theobject that was hit
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        //if it has an Enemy component
        if (enemy != null)
        {


            enemy.TakeDamage(damage);

        }

        

        if (hitInfo.gameObject.tag != "Player")
        {

            Instantiate(impactEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);

        }


    }



}
