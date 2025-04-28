using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Collider objectCollider;
    private Transform duckObjectGrabPointTransform;
    public GameObject dialogueBox; // UI element to show dialogue
    public string dialogueText; // Text to display

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
    }


    public void Grab(Transform duckObjectGrabPointTransform)
    {
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

        // Show dialogue
        dialogueBox.SetActive(true);
        dialogueBox.GetComponentInChildren<Text>().text = dialogueText;
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
        dialogueBox.SetActive(false);
    }
}
