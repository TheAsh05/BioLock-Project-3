using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BossProximityDialogue : MonoBehaviour
{
    // public Transform player;
    // public float triggerDistance = 5f;

    // public GameObject dialogueBox;
    // public TextMeshProUGUI textComponent;
    // public string[] lines;
    // public float textSpeed = 0.05f;

    // public string potionTag = "Potion";
    // public string grabPointTag = "GrabPoint";

    // public GameObject postDialogueCanvas; // UI Canvas to show after dialogue if potion is held

    // private int index;
    // private bool dialogueStarted = false;
    // private bool dialogueFinished = false;

    // private void Start()
    // {
    //     if (dialogueBox != null)
    //         dialogueBox.SetActive(false);

    //     if (postDialogueCanvas != null)
    //         postDialogueCanvas.SetActive(false);

    //     textComponent.text = string.Empty;
    // }

    // private void Update()
    // {
    //     if (!dialogueStarted)
    //     {
    //         float distance = Vector3.Distance(player.position, transform.position);
    //         if (distance <= triggerDistance)
    //         {
    //             dialogueStarted = true;
    //             dialogueBox.SetActive(true);
    //             StartDialogue();
    //         }
    //     }
    //     else if (Input.GetMouseButtonDown(0) && !dialogueFinished)
    //     {
    //         if (textComponent.text == lines[index])
    //         {
    //             NextLine();
    //         }
    //         else
    //         {
    //             StopAllCoroutines();
    //             textComponent.text = lines[index];
    //         }
    //     }
    // }

    // void StartDialogue()
    // {
    //     index = 0;
    //     StartCoroutine(TypeLine());
    // }

    // IEnumerator TypeLine()
    // {
    //     textComponent.text = "";
    //     foreach (char c in lines[index])
    //     {
    //         textComponent.text += c;
    //         yield return new WaitForSeconds(textSpeed);
    //     }
    // }

    // void NextLine()
    // {
    //     if (index < lines.Length - 1)
    //     {
    //         index++;
    //         textComponent.text = "";
    //         StartCoroutine(TypeLine());
    //     }
    //     else
    //     {
    //         dialogueBox.SetActive(false);
    //         dialogueFinished = true;

    //         // Now check if the potion is in hand
    //         CheckPotionInHand();
    //     }
    // }

    // void CheckPotionInHand()
    // {
    //     GameObject potion = GameObject.FindGameObjectWithTag(potionTag);
    //     GameObject grabPoint = GameObject.FindGameObjectWithTag(grabPointTag);

    //     if (potion != null && grabPoint != null && potion.transform.parent == grabPoint.transform)
    //     {
    //         if (postDialogueCanvas != null)
    //             postDialogueCanvas.SetActive(true);
    //     }
    // }



    public Transform player;
    public float triggerDistance = 5f;

    public GameObject dialogueBox;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;

    private int index;
    private bool playerInRange = false;
    private bool dialogueStarted = false;

    [HideInInspector]
    public bool dialogueFinished = false;

    private void Start()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        textComponent.text = string.Empty;
    }

    private void Update()
    {
        if (!dialogueStarted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= triggerDistance)
            {
                dialogueStarted = true;
                dialogueBox.SetActive(true);
                StartDialogue();
            }
        }
        else if (Input.GetMouseButtonDown(0) && !dialogueFinished)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = "";
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueBox.SetActive(false);
            dialogueFinished = true;
        }
    }
}
