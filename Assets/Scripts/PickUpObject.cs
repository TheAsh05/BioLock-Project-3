using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHands;
    bool canpickup;
    GameObject ObjectIwantToPickUp;
    bool hasItem;

    void Start()
    {
        canpickup = false;
        hasItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canpickup == true)
        {
            if (Input.GetKeyDown("e"))
            {
                ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
                ObjectIwantToPickUp.transform.position = myHands.transform.position;
                ObjectIwantToPickUp.transform.parent = myHands.transform;
                hasItem = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "object")
        {
            canpickup = true;
            ObjectIwantToPickUp = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canpickup = false;
    }
}
