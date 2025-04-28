using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Collider objectCollider;
    private Transform potionObjectGrabPointTransform;
    

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
    }


    public void Grab(Transform potionObjectGrabPointTransform)
    {
        this.potionObjectGrabPointTransform = potionObjectGrabPointTransform;

        // Turn off physics
        objectRigidbody.isKinematic = true;
        // objectRigidbody.interpolation = RigidbodyInterpolation.None; // Just extra safe
        objectCollider.enabled = false;

        // Parent to the grab point
        transform.SetParent(potionObjectGrabPointTransform);

        // Snap exactly onto the grab point
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity; // Reset rotation relative to grab point
    }

    public void Drop()
    {
        this.potionObjectGrabPointTransform = null;

        // Unparent
        transform.SetParent(null);

        // Restore physics
        objectRigidbody.isKinematic = false;
        objectCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        
    }
}
