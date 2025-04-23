using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Transform duckObjectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private LayerMask duckPickUpLayerMask;
    

    private ObjectGrabbable objectGrabbable;
    private DuckObjectGrabbable duckObjectGrabbable;


    private void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.Q))
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
            } else 
            {
                //Currently carrying something
                duckObjectGrabbable.Drop();
                duckObjectGrabbable = null;
            }
        }
    }
}
