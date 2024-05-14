using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterDelay : MonoBehaviour
{

    public AudioClip deathSound;
    private AudioSource deathAudio;
    public float delay = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        deathAudio = GetComponent<AudioSource>();
        deathAudio.PlayOneShot(deathSound, 1.0f);
        Destroy(gameObject, delay);
    }

    
}
