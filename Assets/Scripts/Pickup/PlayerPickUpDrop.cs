using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Transform duckObjectGrabPointTransform;
    [SerializeField] private Transform potionObjectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private LayerMask duckPickUpLayerMask;
    [SerializeField] private LayerMask potionPickUpLayerMask;


    // For Lock Interactable
    public static PlayerPickUpDrop Instance; // Singleton pattern
    [SerializeField] private Transform keyGrabPointTransform;
    [SerializeField] private LayerMask keyPickUpLayerMask;    


    private ObjectGrabbable objectGrabbable;
    private DuckObjectGrabbable duckObjectGrabbable;
    private PotionObjectGrabbable potionObjectGrabbable;
    private KeyObjectGrabbable keyObjectGrabbable;


    // Keeping the Duck and Potion
    public GameObject duck; // The duck object in the player scene
    public GameObject potion; // The potion object


    private void Awake()
    {
        // for Key grabbable
        Instance = this; // Set reference
    }
   

    private void Update()
    {
        // Object Pickup (old key pickup)
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                //Not Carryng and object, try to grab
                float pickUpDistance = 2f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        Debug.Log(objectGrabbable);
                    }
                }
            } else 
            {
                //Currently carrying something
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }



        // Duck Pickup
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (duckObjectGrabbable == null)
            {
                //Not Carryng and object, try to grab
                float pickUpDistance = 2f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, duckPickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out duckObjectGrabbable))
                    {
                        duckObjectGrabbable.Grab(duckObjectGrabPointTransform);
                        Debug.Log(duckObjectGrabbable);
                    }
                }
            } 
            else 
            {
                //Currently carrying something
                duckObjectGrabbable.Drop();
                duckObjectGrabbable = null;
            }
        }



        // Potion Pickup
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (potionObjectGrabbable == null)
            {
                //Not Carryng and object, try to grab
                float pickUpDistance = 2f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, potionPickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out potionObjectGrabbable))
                    {
                        potionObjectGrabbable.Grab(potionObjectGrabPointTransform);
                        Debug.Log(potionObjectGrabbable);
                    }
                }
            } else 
            {
                //Currently carrying something
                potionObjectGrabbable.Drop();
                potionObjectGrabbable = null;
            }
        }



        // Key Pickup
        if (Input.GetKeyDown(KeyCode.E)) // Or Q or whatever
        {
            if (keyObjectGrabbable == null)
            {
                float pickUpDistance = 2f;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, pickUpDistance, keyPickUpLayerMask))
                {
                    if (hit.transform.TryGetComponent(out KeyObjectGrabbable key))
                    {
                        keyObjectGrabbable = key;
                        keyObjectGrabbable.Grab(keyGrabPointTransform);
                    }
                }
            }
            else
            {
                keyObjectGrabbable.Drop();
                keyObjectGrabbable = null;
            }
        }
    }

    // For Key Pickup
    public bool IsHoldingKey()
    {
        return keyObjectGrabbable != null;
    }
}
