using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//add this to use TMP_Text
using TMPro;



public class DialogueManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
   
    public TMP_Text textbox;
    public string[] sentences;
    private int index;
    public float typingSpeed;




    public GameObject continueButton;
    public GameObject dialoguePanel;


    //function on that only works when somethign is enabled
    void OnEnable()
    {
        continueButton.SetActive(false);
        StartCoroutine(Type());

    }

    IEnumerator Type()
    {
        textbox.text = "";
        foreach (char letter in sentences[index])

        {
            
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        continueButton.SetActive(true);
    }





    public void NextSentence()
        {



            continueButton.SetActive(false);

            if(index < sentences.Length - 1)
            {
                index++;
                textbox.text = "";
                StartCoroutine(Type());
            } else
            {
                textbox.text = "";
                dialoguePanel.SetActive(false);
            }
        }
   


    // Update is called once per frame
    void Update()
    {
        
    }
}
