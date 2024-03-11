using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    bool active = true;
    // Start is called before the first frame update


    public AudioClip triggerSound;

    private AudioSource playerAudio;

    private void OnTriggerEnter2D (Collider2D collision)
    {

        playerAudio = GetComponent<AudioSource>();
        if (active && collision.gameObject.tag == "Player")
        {

            active = false;
            
            ScoreManager.score++;

            playerAudio.PlayOneShot(triggerSound, 1.0f);
        }
    }

 
}