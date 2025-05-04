using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Transform duckObjectGrabPointTransform;
    [SerializeField] private Transform potionObjectGrabPointTransform;
    [SerializeField] private Transform keyGrabPointTransform;

    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private LayerMask duckPickUpLayerMask;
    [SerializeField] private LayerMask potionPickUpLayerMask;
    [SerializeField] private LayerMask keyPickUpLayerMask; 



    // For Lock Interactable
    public static PlayerPickUpDrop Instance; // Singleton pattern
    
       
    private ObjectGrabbable objectGrabbable;
    private DuckObjectGrabbable duckObjectGrabbable;
    private PotionObjectGrabbable potionObjectGrabbable;
    private KeyObjectGrabbable keyObjectGrabbable;


    // Keeping the Duck and Potion in the Inventory
    public GameObject duck; // The duck object in the player scene
    public GameObject potion; // The potion object


    private void Awake()
    {
        // for Key grabbable
        Instance = this; // Set reference
    }

    private void Start()
    {
        //StartCoroutine(DelayedRegrabHeldItems());
        StartCoroutine(RestoreHeldItemsAfterSceneLoad());
        //duckObjectGrabbable.GetComponent<Collider>().enabled = false;
    }

    private IEnumerator RestoreHeldItemsAfterSceneLoad()
    {
        yield return null; // Wait one frame to ensure scene is ready

        if (PlayerInventory.wasHoldingDuck && PlayerInventory.currentDuck != null)
        {
            duckObjectGrabbable = PlayerInventory.currentDuck.GetComponent<DuckObjectGrabbable>();
            if (duckObjectGrabbable != null)
            {
                duckObjectGrabbable.Grab(duckObjectGrabPointTransform);
                Debug.Log("Restored duck after scene load.");
            }
            else
            {
                Debug.LogError("DuckObjectGrabbable is null.");
            }
        }

        if (PlayerInventory.wasHoldingPotion && PlayerInventory.currentPotion != null)
        {
            potionObjectGrabbable = PlayerInventory.currentPotion.GetComponent<PotionObjectGrabbable>();
            if (potionObjectGrabbable != null)
            {
                potionObjectGrabbable.Grab(potionObjectGrabPointTransform);
                Debug.Log("Restored potion after scene load.");
            }
            else
            {
                Debug.LogError("PotionObjectGrabbable is null.");
            }
        }
    }

    // private IEnumerator RestoreHeldItemsAfterSceneLoad()
    // {
    //     yield return null; // Wait one frame to ensure scene is ready

    //     if (PlayerInventory.wasHoldingDuck && PlayerInventory.currentDuck != null)
    //     {
    //         duckObjectGrabbable = PlayerInventory.currentDuck.GetComponent<DuckObjectGrabbable>();
    //         duckObjectGrabbable.Grab(duckObjectGrabPointTransform);
    //         Debug.Log("Restored duck after scene load.");
    //     }

    //     if (PlayerInventory.wasHoldingPotion && PlayerInventory.currentPotion != null)
    //     {
    //         potionObjectGrabbable = PlayerInventory.currentPotion.GetComponent<PotionObjectGrabbable>();
    //         potionObjectGrabbable.Grab(potionObjectGrabPointTransform);
    //         Debug.Log("Restored potion after scene load.");
    //     }
    // }

    // private IEnumerator RestoreHeldObjects()
    // {
    //     yield return null; // Wait one frame to ensure scene load completes

    //     if (PlayerInventory.currentDuck != null && PlayerInventory.wasHoldingDuck)
    //     {
    //         duckObjectGrabbable = PlayerInventory.currentDuck.GetComponent<DuckObjectGrabbable>();
    //         duckObjectGrabbable.Grab(duckObjectGrabPointTransform);
    //         Debug.Log("Restored duck to player's hand.");
    //     }

    //     if (PlayerInventory.currentPotion != null && PlayerInventory.wasHoldingPotion)
    //     {
    //         potionObjectGrabbable = PlayerInventory.currentPotion.GetComponent<PotionObjectGrabbable>();
    //         potionObjectGrabbable.Grab(potionObjectGrabPointTransform);
    //         Debug.Log("Restored potion to player's hand.");
    //     }
    // }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float pickUpDistance = 2f;

            // --- Check for Key ---
            if (keyObjectGrabbable == null &&
                Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit keyHit, pickUpDistance, keyPickUpLayerMask))
            {
                if (keyHit.transform.TryGetComponent(out KeyObjectGrabbable key))
                {
                    keyObjectGrabbable = key;
                    key.Grab(keyGrabPointTransform);
                    PlayerInventory.hasKey = true;
                    Debug.Log("Picked up key.");
                    return;
                }
            }

            // --- Check for Duck ---
            if (duckObjectGrabbable == null &&
                Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit duckHit, pickUpDistance, duckPickUpLayerMask))
            {
                if (duckHit.transform.TryGetComponent(out DuckObjectGrabbable duck))
                {
                    duckObjectGrabbable = duck;
                    duck.Grab(duckObjectGrabPointTransform);

                    PlayerInventory.currentDuck = duckObjectGrabbable.gameObject;
                    PlayerInventory.wasHoldingDuck = true;
                    DontDestroyOnLoad(duckObjectGrabbable.gameObject);
                    Debug.Log("Picked up duck.");
                    return;
                }
            }

            // --- Check for Potion ---
            if (potionObjectGrabbable == null &&
                Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit potionHit, pickUpDistance, potionPickUpLayerMask))
            {
                if (potionHit.transform.TryGetComponent(out PotionObjectGrabbable potion))
                {
                    potionObjectGrabbable = potion;
                    potion.Grab(potionObjectGrabPointTransform);

                    PlayerInventory.currentPotion = potionObjectGrabbable.gameObject;
                    PlayerInventory.wasHoldingPotion = true;

                    DontDestroyOnLoad(potionObjectGrabbable.gameObject);
                    Debug.Log("Picked up potion.");
                    return;
                }
            }

            // --- Check for Generic Object ---
            if (objectGrabbable == null &&
                Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit objectHit, pickUpDistance, pickUpLayerMask))
            {
                if (objectHit.transform.TryGetComponent(out ObjectGrabbable obj))
                {
                    objectGrabbable = obj;
                    obj.Grab(objectGrabPointTransform);
                    Debug.Log("Picked up object.");
                    return;
                }
            }

            // --- Drop Objects ---
            if (objectGrabbable != null)
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
                Debug.Log("Dropped object.");
                return;
            }

            if (duckObjectGrabbable != null)
            {
                duckObjectGrabbable.Drop();
                duckObjectGrabbable = null;
                PlayerInventory.wasHoldingDuck = false;
                Debug.Log("Dropped duck.");
                return;
            }

            if (potionObjectGrabbable != null)
            {
                potionObjectGrabbable.Drop();
                potionObjectGrabbable = null;
                PlayerInventory.wasHoldingPotion = false;
                Debug.Log("Dropped potion.");
                return;
            }
        }
    }
   
    

    // For Key Pickup
    public bool IsHoldingKey()
    {
        Debug.Log("Checking if holding key: " + (keyObjectGrabbable != null));
        return keyObjectGrabbable != null;
    }

    public void RemoveKey()
    {
        if (keyObjectGrabbable != null)
        {
            Destroy(keyObjectGrabbable.gameObject);
            keyObjectGrabbable = null;
            PlayerInventory.hasKey = false;
            Debug.Log("Key removed from inventory.");
        }
    }


    // //Inventory Code
    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     if (PlayerInventory.currentDuck != null)
    //     {
    //         PlayerInventory.currentDuck.SetActive(true);
    //         DontDestroyOnLoad(PlayerInventory.currentDuck);
    //     }

    //     if (PlayerInventory.currentPotion != null)
    //     {
    //         PlayerInventory.currentPotion.SetActive(true);
    //         DontDestroyOnLoad(PlayerInventory.currentPotion);
    //     }
    //     // Do NOT reset the inventory here unless this is the final level or the game should reset
    //     // PlayerInventory.ResetInventory(); // REMOVE or control this separately
    // }
}
