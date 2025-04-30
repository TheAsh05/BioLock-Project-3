using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Collider objectCollider;
    private Transform keyObjectGrabPointTransform;
    

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
    }


    public void Grab(Transform keyObjectGrabPointTransform)
    {
        this.keyObjectGrabPointTransform = keyObjectGrabPointTransform;

        // Turn off physics
        objectRigidbody.isKinematic = true;
        // objectRigidbody.interpolation = RigidbodyInterpolation.None; // Just extra safe
        objectCollider.enabled = false;

        // Parent to the grab point
        transform.SetParent(keyObjectGrabPointTransform);

        // Snap exactly onto the grab point
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity; // Reset rotation relative to grab point

        // Ignore collisions between the key and the player
        Collider playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
        Physics.IgnoreCollision(objectCollider, playerCollider, true);
    }

    public void Drop()
    {
        // I may not make it so you can drop the key
        // this.keyObjectGrabPointTransform = null;

        // // Unparent
        // transform.SetParent(null);

        // // Restore physics
        // objectRigidbody.isKinematic = false;
        // objectCollider.enabled = true;

        // // Re-enable collision
        // Collider playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
        // Physics.IgnoreCollision(objectCollider, playerCollider, false);
    }

    private void LateUpdate()
    {
        if (keyObjectGrabPointTransform != null)
        {
            // Force position to follow exactly in case parenting lags
            transform.position = keyObjectGrabPointTransform.position;
            transform.rotation = keyObjectGrabPointTransform.rotation;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
