using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    bool active = true;
    // Start is called before the first frame update
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (active && collision.gameObject.tag == "Player")
        {

            active = false;
            
            ScoreManager.score++;
        }
    }

 
}