// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class DuckObjectGrabbable : MonoBehaviour
// {
//     private Rigidbody objectRigidbody;
//     private Collider objectCollider;
//     private Transform duckObjectGrabPointTransform;
//     public GameObject dialogueBox; // UI element to show dialogue
//     public string dialogueText; // Text to display

//     private void Awake()
//     {
//         objectRigidbody = GetComponent<Rigidbody>();
//         objectCollider = GetComponent<Collider>();
//     }


//     public void Grab(Transform duckObjectGrabPointTransform)
//     {
//         this.duckObjectGrabPointTransform = duckObjectGrabPointTransform;
//         objectRigidbody.useGravity = false;
//         //make it a child of duckobject grab point transform

//         dialogueBox.SetActive(true);
//         dialogueBox.GetComponentInChildren<Text>().text = dialogueText; // Display the dialogue text
//     }

//     public void Drop()
//     {
//         this.duckObjectGrabPointTransform = null;
//         objectRigidbody.useGravity = true;
//     }

//     private void FixedUpdate()
//     {
//         if (duckObjectGrabPointTransform != null)
//         {
//             float lerpSpeed = 10f;
//             UnityEngine.Vector3 newPosition = UnityEngine.Vector3.Lerp(transform.position, duckObjectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
//             objectRigidbody.MovePosition(newPosition);

//             // Make the duck face the player

//         }
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         dialogueBox.SetActive(false);
//     }
// }