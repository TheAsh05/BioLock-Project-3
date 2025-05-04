using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LockInteractable : MonoBehaviour
{
    public TextMeshProUGUI messageText; // assign in inspector
    public string nextSceneName = "NextLevel"; // your next scene

    private void Start()
    {
        messageText.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked lock");

        if (PlayerPickUpDrop.Instance != null && PlayerPickUpDrop.Instance.IsHoldingKey())
        {
            Debug.Log("Player is holding key - loading scene...");

            // Optionally: Remove or destroy the key before changing scene
            PlayerPickUpDrop.Instance.RemoveKey(); // Only if you implement it
            SceneManager.LoadScene(nextSceneName);
        }


        // if (PlayerPickUpDrop.Instance.IsHoldingKey())
        // {
        //     // Remove Key
        //     PlayerPickUpDrop.Instance.RemoveKey();
        //     // Load next scene
        //     SceneManager.LoadScene(nextSceneName);
        // }
        else
        {
            Debug.Log("Player is NOT holding key");
            
            // Show message to the player if they don't have the key
            StartCoroutine(ShowMessage("You need the key to unlock this!", 2f));
        }
    }

    private IEnumerator ShowMessage(string message, float delay)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        messageText.gameObject.SetActive(false);
    }

    // public Text messageText; // Assign in inspector
    // public string nextSceneName; // The name of the next scene to load

    // private void Start()
    // {
    //     messageText.gameObject.SetActive(false);
    // }

    // private void OnMouseDown()
    // {
    //     // Raycast requires a collider on this object!
    //     if (PlayerHoldingKey())
    //     {
    //         SceneManager.LoadScene("Level 2");
    //     }
    //     else
    //     {
    //         Debug.Log("You need a key to unlock this");
    //     }
    // }

    // private bool PlayerHoldingKey()
    // {
    //     // Simple static reference pattern
    //     return PlayerPickUpDrop.Instance != null && PlayerPickUpDrop.Instance.IsHoldingKey();
    // }
}
