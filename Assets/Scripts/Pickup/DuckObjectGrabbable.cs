using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DuckObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Collider objectCollider;
    private Transform duckObjectGrabPointTransform;
    public GameObject dialogueBox; // UI element to show dialogue
    public string dialogueText; // Text to display

    private bool isFirstScene;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
    }

    public void Grab(Transform duckObjectGrabPointTransform)
    {
        if (this == null || this.gameObject == null)
        {
            Debug.LogError("Duck object has been destroyed or is null, cannot grab.");
            return;
        }

        this.duckObjectGrabPointTransform = duckObjectGrabPointTransform;

        // Turn off physics
        objectRigidbody.isKinematic = true;
        objectRigidbody.interpolation = RigidbodyInterpolation.None; // Just extra safe
        objectCollider.enabled = false;

        // Parent to the grab point
        transform.SetParent(duckObjectGrabPointTransform);

        // Snap exactly onto the grab point
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity; // Reset rotation relative to grab point

        // Show dialogue only in first scene
        if (isFirstScene && dialogueBox != null)
        {
            dialogueBox.SetActive(true);
            dialogueBox.GetComponentInChildren<TextMeshProUGUI>().text = dialogueText;
        }
        // // Show dialogue
        // dialogueBox.SetActive(true);
        // dialogueBox.GetComponentInChildren<TextMeshProUGUI>().text = dialogueText;
    }

    public void Drop()
    {
        // this.duckObjectGrabPointTransform = null;

        // // Unparent
        // transform.SetParent(null);

        // // Restore physics
        // objectRigidbody.isKinematic = false;
        // objectCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Check if this is the first scene
        string sceneName = SceneManager.GetActiveScene().name;
        isFirstScene = sceneName == "Level 1"; // <-- replace with your actual scene name

        if (isFirstScene && dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
        //dialogueBox.SetActive(false);
    }
}
