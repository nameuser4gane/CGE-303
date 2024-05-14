using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject projectilePrefab;

    //reference to firepoint transform
    //where projectile will be instantiated
    //make empty child of player and 
    //position where projectile is to be fired
    //assign variable in inspector
    public Transform firePoint;

    

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetButtonDown("Fire1"))
        {

            Shoot();
        }


        void Shoot()
        {

            GameObject firedProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            Destroy(firedProjectile, 3f);

        }


    }
}
