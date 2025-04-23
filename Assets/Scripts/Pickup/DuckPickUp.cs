using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckPickUp : MonoBehaviour
{
    // I AM NOT USING THIS SCRIPT

    public string triggerTag = "Player"; // Tag of the object that triggers pickup
    public GameObject dialogueBox; // UI element to show dialogue
    public string dialogueText; // Text to display

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            // Detect collission with trigger object
            ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        // Make sure UI element is visible
        dialogueBox.SetActive(true);
        dialogueBox.GetComponentInChildren<Text>().text = dialogueText; // Display the dialogue text
    }



    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
