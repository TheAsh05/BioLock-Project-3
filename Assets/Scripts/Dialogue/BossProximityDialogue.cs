using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

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

    private IEnumerator Start()
    {
        // Wait for player to exist in scene
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            yield return null;
        }

        // Look for PotionGrabPoint in player hierarchy
        potionGrabPoint = FindChildRecursive(player.transform, "PotionGrabPoint");
        if (potionGrabPoint == null)
        {
            Debug.LogWarning("PotionGrabPoint not found under player!");
        }

        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
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

        dialogueComponent.StartDialogue();

        while (dialogueBox.activeSelf)
        {
            yield return null;
        }

        if (sceneDecided) yield break;

        // Check for potion in hand
        if (potionGrabPoint != null && potionGrabPoint.childCount > 0)
        {
            //SceneManager.LoadScene(winSceneName);
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            //SceneManager.LoadScene(loseSceneName);
            SceneManager.LoadScene("LoseScene");
        }

        sceneDecided = true;
    }

    private Transform FindChildRecursive(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;

            Transform found = FindChildRecursive(child, childName);
            if (found != null)
                return found;
        }
        return null;
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

    // private IEnumerator Start()
    // {
    //     // Wait until the player exists in the scene
    //     GameObject player = null;
    //     while (player == null)
    //     {
    //         player = GameObject.FindGameObjectWithTag("Player");
    //         yield return null;
    //     }

    //     // Try to find the PotionGrabPoint under the player
    //     potionGrabPoint = FindChildRecursive(player.transform, "PotionGrabPoint");
    //     if (potionGrabPoint == null)
    //     {
    //         Debug.LogWarning("PotionGrabPoint not found under player!");
    //     }

    //     // Proceed with rest of logic (e.g., hiding dialogue box at start)
    //     if (dialogueBox != null)
    //     {
    //         dialogueBox.SetActive(false);
    //     }
    // }

    // private Transform FindChildRecursive(Transform parent, string childName)
    // {
    //     foreach (Transform child in parent)
    //     {
    //         if (child.name == childName)
    //             return child;

    //         Transform result = FindChildRecursive(child, childName);
    //         if (result != null)
    //             return result;
    //     }
    //     return null;
    // }

    // // private void Start()
    // // {
    // //     player = GameObject.FindWithTag("Player");

    // //     if (player == null)
    // //     {
    // //         Debug.LogError("Player not found in scene!");
    // //         return;
    // //     }

    // //     // Look for child named "PotionGrabPoint"
    // //     potionGrabPoint = player.transform.Find("PotionGrabPoint");
    // //     if (potionGrabPoint == null)
    // //     {
    // //         Debug.LogWarning("PotionGrabPoint not found under player!");
    // //     }

    // //     if (dialogueBox != null)
    // //     {
    // //         dialogueComponent = dialogueBox.GetComponent<Dialogue>();
    // //         if (dialogueComponent == null)
    // //         {
    // //             Debug.LogError("Dialogue component not found on dialogueBox!");
    // //         }
    // //     }
    // //     else
    // //     {
    // //         Debug.LogError("DialogueBox is not assigned!");
    // //     }

    // //     dialogueBox.SetActive(false);
    // // }

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

    //     // Start the dialogue
    //     dialogueComponent.StartDialogue();

    //     // Wait for dialogue to finish
    //     while (dialogueBox.activeSelf)
    //     {
    //         yield return null;
    //     }

    //     if (sceneDecided) yield break;

    //     // Check if potion is in player's hand
    //     if (potionGrabPoint != null && potionGrabPoint.childCount > 0)
    //     {
    //         SceneManager.LoadScene(winSceneName);
    //     }
    //     else
    //     {
    //         SceneManager.LoadScene(loseSceneName);
    //     }

    //     sceneDecided = true;
    // }
}
