using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Linq;
using StarterAssets;

public class BossProximityDialogue : MonoBehaviour
{
    public float triggerDistance = 5f;
    public GameObject dialogueBox; // Assign the Dialogue Box Canvas here
    public string winSceneName;
    public string loseSceneName;

    private GameObject player;
    private Transform potionGrabPoint;
    private Dialogue dialogueComponent;
    private bool dialogueStarted = false;
    private bool sceneDecided = false;

    private void Start()
    {
        // Find the player by looking for the FirstPersonController (or any other unique identifier)
        player = GameObject.FindObjectOfType<FirstPersonController>()?.gameObject;

        if (player == null)
        {
            Debug.LogError("Player not found in scene!");
            return;
        }

        // Look for child named "PotionGrabPoint" under MainCamera
        //potionGrabPoint = player.transform.Find("MainCamera/PotionGrabPoint");


        // if (potionGrabPoint == null)
        // {
        //     Debug.LogWarning("PotionGrabPoint not found under player!");
        // }

        if (dialogueBox != null)
        {
            dialogueComponent = dialogueBox.GetComponent<Dialogue>();
            if (dialogueComponent == null)
            {
                Debug.LogError("Dialogue component not found on dialogueBox!");
            }
        }
        else
        {
            Debug.LogError("DialogueBox is not assigned!");
        }

        dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (dialogueStarted || player == null) return;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= triggerDistance)
        {
            StartCoroutine(HandleDialogue());
        }
    }

    private IEnumerator HandleDialogue()
    {
        dialogueStarted = true;
        dialogueBox.SetActive(true);

        if (dialogueComponent == null || dialogueComponent.lines == null || dialogueComponent.lines.Length == 0)
        {
            Debug.LogError("No dialogue lines provided!");
            yield break;
        }

        // Start the dialogue
        dialogueComponent.StartDialogue();

        // Wait for dialogue to finish
        while (dialogueBox.activeSelf)
        {
            yield return null;
        }

        if (sceneDecided) yield break;

        // Find potion with TAG
        var foundPotion = GameObject.FindGameObjectWithTag("Potion");

        // Check if potion is in player's hand
        if (foundPotion != null)
        {
            SceneManager.LoadScene(winSceneName);
        }
        else
        {
            SceneManager.LoadScene(loseSceneName);
        }

        sceneDecided = true;
    }




























    // public float triggerDistance = 5f;
    // public GameObject dialogueBox; // Assign the Dialogue Box Canvas here
    // public string winSceneName;
    // public string loseSceneName;

    // private GameObject player;
    // private Transform potionGrabPoint;
    // private Dialogue dialogueComponent;
    // private bool dialogueStarted = false;
    // private bool sceneDecided = false;

    // private void Start()
    // {
    //     var controller = FindObjectOfType<StarterAssets.FirstPersonController>();
    //     if (controller == null)
    //     {
    //         Debug.LogError("FirstPersonController not found!");
    //         return;
    //     }

    //     player = controller.gameObject;

    //     Debug.Log("Searching under: " + player.name);
    //     PrintChildren(player.transform, 0); // NEW: Print hierarchy to console
        

    //     // Look for "PotionGrabPoint" anywhere under the player
    //     potionGrabPoint = FindChildRecursive(player.transform, "PotionGrabPoint");
    //     if (potionGrabPoint == null)
    //     {
    //         Debug.LogWarning("PotionGrabPoint not found under player!");
    //     }

    //     if (dialogueBox != null)
    //     {
    //         dialogueComponent = dialogueBox.GetComponent<Dialogue>();
    //         if (dialogueComponent == null)
    //         {
    //             Debug.LogError("Dialogue component not found on dialogueBox!");
    //         }
    //         dialogueBox.SetActive(false);
    //     }
    //     else
    //     {
    //         Debug.LogError("DialogueBox is not assigned!");
    //     }



    //     // // Find the player via FirstPersonController or similar component
    //     // var controller = FindObjectOfType<StarterAssets.FirstPersonController>();
    //     // if (controller == null)
    //     // {
    //     //     Debug.LogError("FirstPersonController not found!");
    //     //     return;
    //     // }

    //     // player = controller.gameObject;

    //     // // Look for "PotionGrabPoint" anywhere under the player
    //     // potionGrabPoint = FindChildRecursive(player.transform, "PotionGrabPoint");
    //     // if (potionGrabPoint == null)
    //     // {
    //     //     Debug.LogWarning("PotionGrabPoint not found under player!");
    //     // }

    //     // if (dialogueBox != null)
    //     // {
    //     //     dialogueComponent = dialogueBox.GetComponent<Dialogue>();
    //     //     if (dialogueComponent == null)
    //     //     {
    //     //         Debug.LogError("Dialogue component not found on dialogueBox!");
    //     //     }
    //     //     dialogueBox.SetActive(false);
    //     // }
    //     // else
    //     // {
    //     //     Debug.LogError("DialogueBox is not assigned!");
    //     // }
    // }

    // private void Update()
    // {
    //     if (dialogueStarted || player == null) return;

    //     float distance = Vector3.Distance(player.transform.position, transform.position);
    //     if (distance <= triggerDistance)
    //     {
    //         StartCoroutine(HandleDialogue());
    //     }
    // }

    // private IEnumerator HandleDialogue()
    // {
    //     dialogueStarted = true;
    //     dialogueBox.SetActive(true);

    //     if (dialogueComponent == null || dialogueComponent.lines == null || dialogueComponent.lines.Length == 0)
    //     {
    //         Debug.LogError("No dialogue lines provided!");
    //         yield break;
    //     }

    //     dialogueComponent.StartDialogue();

    //     // Wait for dialogue to finish (i.e. box is deactivated)
    //     while (dialogueBox.activeSelf)
    //     {
    //         yield return null;
    //     }

    //     if (sceneDecided) yield break;

    //     // Check if potion is being held
    //     if (potionGrabPoint != null && potionGrabPoint.childCount > 0)
    //     {
    //         SceneManager.LoadScene("WinScene");
    //     }
    //     else
    //     {
    //         SceneManager.LoadScene("LoseScene");
    //     }

    //     sceneDecided = true;
    // }

    // // Recursively find a child transform by name
    // private Transform FindChildRecursive(Transform parent, string name)
    // {
    //     foreach (Transform child in parent)
    //     {
    //         if (child.name == name)
    //             return child;

    //         var result = FindChildRecursive(child, name);
    //         if (result != null)
    //             return result;
    //     }
    //     return null;
    // }

    // private void PrintChildren(Transform parent, int level)
    // {
    //     string indent = new string('-', level * 2);
    //     Debug.Log(indent + parent.name);
    //     foreach (Transform child in parent)
    //     {
    //         PrintChildren(child, level + 1);
    //     }
    // }
}
