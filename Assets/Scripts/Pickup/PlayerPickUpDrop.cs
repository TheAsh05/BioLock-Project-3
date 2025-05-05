using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPickUpDrop : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Transform duckObjectGrabPointTransform;
    [SerializeField] private Transform potionObjectGrabPointTransform;
    [SerializeField] private Transform keyGrabPointTransform;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private LayerMask duckPickUpLayerMask;
    [SerializeField] private LayerMask potionPickUpLayerMask;
    [SerializeField] private LayerMask keyPickUpLayerMask;

    [Header("Inventory Objects")]
    public GameObject duck;
    public GameObject potion;

    // Singleton
    public static PlayerPickUpDrop Instance;

    private ObjectGrabbable objectGrabbable;
    private DuckObjectGrabbable duckObjectGrabbable;
    private PotionObjectGrabbable potionObjectGrabbable;
    private KeyObjectGrabbable keyObjectGrabbable;

    public GameObject currentDuck; // reference to the duck object
    public GameObject duckPrefab; // Assign in Inspector
    private string heldItemName = "Duck"; //Example values: "duck", "potion"

    private void Awake()
    {
        Instance = this;
        // Ensure the duck is preserved across scenes
        if (currentDuck != null)
        {
            DontDestroyOnLoad(currentDuck);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(RestoreHeldItemsAfterSceneLoad());
    }

    private IEnumerator RestoreHeldItemsAfterSceneLoad()
    {
        //yield return null; // Wait one frame to ensure scene is ready
        yield return new WaitForEndOfFrame();  // Wait until end of frame to ensure scene is ready

        // Inside your coroutine:
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Level 1") // replace with your actual first scene name
        {
            Debug.Log("Skipping item restore in the first level.");
            yield break;
        }

        if (heldItemName == "Duck" && duckPrefab != null)
        {
            Debug.Log("Instantiating Duck prefab after scene load...");
            GameObject newDuck = Instantiate(duckPrefab);
            // Position it at player’s grab point (adjust as needed)
            newDuck.transform.position = duckObjectGrabPointTransform.position;
            newDuck.GetComponent<DuckObjectGrabbable>().Grab(duckObjectGrabPointTransform);
        }
        else
        {
            Debug.Log("No item to restore or prefab is missing.");
        }

        // // Attempt to restore Duck if it's still valid and not destroyed
        // if (PlayerInventory.wasHoldingDuck && PlayerInventory.currentDuck != null)
        // {
        //     try
        //     {
        //         // Check if the Duck object exists and is not destroyed
        //         if (PlayerInventory.currentDuck != null && PlayerInventory.currentDuck.gameObject != null)
        //         {
        //             duckObjectGrabbable = PlayerInventory.currentDuck.GetComponent<DuckObjectGrabbable>();

        //             // If the DuckObjectGrabbable exists, grab it
        //             if (duckObjectGrabbable != null)
        //             {
        //                 duckObjectGrabbable.Grab(duckObjectGrabPointTransform);
        //                 Debug.Log("Restored duck after scene load.");
        //             }
        //             else
        //             {
        //                 Debug.LogError("DuckObjectGrabbable is missing or has been destroyed.");
        //             }
        //         }
        //         else
        //         {
        //             Debug.LogError("PlayerInventory.currentDuck is null or has been destroyed, cannot restore.");
        //         }
        //     }
        //     catch (System.Exception ex)
        //     {
        //         Debug.LogError("Error restoring Duck: " + ex.Message);
        //     }
        // }

        // Attempt to restore Potion if it's still valid and not destroyed
        if (PlayerInventory.wasHoldingPotion && PlayerInventory.currentPotion != null)
        {
            try
            {
                // Check if PotionObjectGrabbable is valid
                if (PlayerInventory.currentPotion != null && PlayerInventory.currentPotion.gameObject != null)
                {
                    potionObjectGrabbable = PlayerInventory.currentPotion.GetComponent<PotionObjectGrabbable>();

                    // If the PotionObjectGrabbable exists, grab it
                    if (potionObjectGrabbable != null)
                    {
                        potionObjectGrabbable.Grab(potionObjectGrabPointTransform);
                        Debug.Log("Restored potion after scene load.");
                    }
                    else
                    {
                        Debug.LogError("PotionObjectGrabbable is missing or has been destroyed.");
                    }
                }
                else
                {
                    Debug.LogError("PlayerInventory.currentPotion is null or has been destroyed, cannot restore.");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error restoring Potion: " + ex.Message);
            }
        }



        // yield return null; // Wait one frame to ensure scene is ready

        // // Attempt to restore Duck if it's still valid and not destroyed
        // if (PlayerInventory.wasHoldingDuck && PlayerInventory.currentDuck != null)
        // {
        //     try
        //     {
        //         // Check if DuckObjectGrabbable is valid and the object exists
        //         if (PlayerInventory.currentDuck != null && PlayerInventory.currentDuck.activeInHierarchy)
        //         {
        //             duckObjectGrabbable = PlayerInventory.currentDuck.GetComponent<DuckObjectGrabbable>();

        //             // If the DuckObjectGrabbable exists, grab it
        //             if (duckObjectGrabbable != null)
        //             {
        //                 duckObjectGrabbable.Grab(duckObjectGrabPointTransform);
        //                 Debug.Log("Restored duck after scene load.");
        //             }
        //             else
        //             {
        //                 Debug.LogError("DuckObjectGrabbable is missing or has been destroyed.");
        //             }
        //         }
        //         else
        //         {
        //             Debug.LogError("PlayerInventory.currentDuck is null or inactive, cannot restore.");
        //         }
        //     }
        //     catch (System.Exception ex)
        //     {
        //         Debug.LogError("Error restoring Duck: " + ex.Message);
        //     }
        // }

        // // Attempt to restore Potion if it's still valid and not destroyed
        // if (PlayerInventory.wasHoldingPotion && PlayerInventory.currentPotion != null)
        // {
        //     try
        //     {
        //         // Check if PotionObjectGrabbable is valid
        //         if (PlayerInventory.currentPotion != null)
        //         {
        //             potionObjectGrabbable = PlayerInventory.currentPotion.GetComponent<PotionObjectGrabbable>();

        //             // If the PotionObjectGrabbable exists, grab it
        //             if (potionObjectGrabbable != null)
        //             {
        //                 potionObjectGrabbable.Grab(potionObjectGrabPointTransform);
        //                 Debug.Log("Restored potion after scene load.");
        //             }
        //             else
        //             {
        //                 Debug.LogError("PotionObjectGrabbable is missing or has been destroyed.");
        //             }
        //         }
        //         else
        //         {
        //             Debug.LogError("PlayerInventory.currentPotion is null, cannot restore.");
        //         }
        //     }
        //     catch (System.Exception ex)
        //     {
        //         Debug.LogError("Error restoring Potion: " + ex.Message);
        //     }
        // }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float pickUpDistance = 2f;

            // --- Pick up Key ---
            if (keyObjectGrabbable == null &&
                Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit keyHit, pickUpDistance, keyPickUpLayerMask))
            {
                if (keyHit.transform.TryGetComponent(out KeyObjectGrabbable key))
                {
                    keyObjectGrabbable = key;
                    key.Grab(keyGrabPointTransform);
                    PlayerInventory.hasKey = true;
                    Debug.Log("🔑 Picked up key.");
                    return;
                }
            }

            // --- Pick up Duck ---
            if (duckObjectGrabbable == null &&
                Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit duckHit, pickUpDistance, duckPickUpLayerMask))
            {
                if (duckHit.transform.TryGetComponent(out DuckObjectGrabbable duck))
                {
                    heldItemName ="Duck";
                    duckObjectGrabbable = duck;
                    duck.Grab(duckObjectGrabPointTransform);

                    PlayerInventory.currentDuck = duckObjectGrabbable.gameObject;
                    PlayerInventory.wasHoldingDuck = true;

                    GameObject duckRoot = duckObjectGrabbable.gameObject.transform.root.gameObject;
                    //DontDestroyOnLoad(duckObjectGrabbable.gameObject);  // Ensure it persists across scenes
                    DontDestroyOnLoad(duckRoot); // 👈 Make sure it's the root object


                    // duckObjectGrabbable = duck;
                    // duck.Grab(duckObjectGrabPointTransform);

                    // // PlayerInventory.currentDuck = duck.gameObject;
                    // // PlayerInventory.wasHoldingDuck = true;
                    // // DontDestroyOnLoad(duck.gameObject);

                    // PlayerInventory.currentDuck = duckObjectGrabbable.gameObject;
                    // PlayerInventory.wasHoldingDuck = true;
                    // DontDestroyOnLoad(duckObjectGrabbable.gameObject.transform.root.gameObject);

                    Debug.Log("🦆 Picked up duck.");
                    return;
                }
            }

            // --- Pick up Potion ---
            if (potionObjectGrabbable == null &&
                Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit potionHit, pickUpDistance, potionPickUpLayerMask))
            {
                if (potionHit.transform.TryGetComponent(out PotionObjectGrabbable potion))
                {
                    potionObjectGrabbable = potion;
                    potion.Grab(potionObjectGrabPointTransform);

                    PlayerInventory.currentPotion = potionObjectGrabbable.gameObject;
                    PlayerInventory.wasHoldingPotion = true;

                    GameObject potionRoot = potionObjectGrabbable.gameObject.transform.root.gameObject;
                    //DontDestroyOnLoad(potionObjectGrabbable.gameObject);  // Ensure it persists across scenes
                    DontDestroyOnLoad(potionRoot);


                    // potionObjectGrabbable = potion;
                    // potion.Grab(potionObjectGrabPointTransform);

                    // // PlayerInventory.currentPotion = potion.gameObject;
                    // // PlayerInventory.wasHoldingPotion = true;
                    // // DontDestroyOnLoad(potion.gameObject);

                    // PlayerInventory.currentPotion = potionObjectGrabbable.gameObject;
                    // PlayerInventory.wasHoldingPotion = true;
                    // DontDestroyOnLoad(potionObjectGrabbable.gameObject.transform.root.gameObject);

                    Debug.Log("🧪 Picked up potion.");
                    return;
                }
            }

            // --- Pick up Generic Object ---
            if (objectGrabbable == null &&
                Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit objectHit, pickUpDistance, pickUpLayerMask))
            {
                if (objectHit.transform.TryGetComponent(out ObjectGrabbable obj))
                {
                    objectGrabbable = obj;
                    obj.Grab(objectGrabPointTransform);
                    Debug.Log("📦 Picked up object.");
                    return;
                }
            }

            // // --- Drop Objects ---
            // if (objectGrabbable != null)
            // {
            //     objectGrabbable.Drop();
            //     objectGrabbable = null;
            //     Debug.Log("📦 Dropped object.");
            //     return;
            // }

            // if (duckObjectGrabbable != null)
            // {
            //     duckObjectGrabbable.Drop();
            //     duckObjectGrabbable = null;
            //     PlayerInventory.wasHoldingDuck = false;
            //     Debug.Log("🦆 Dropped duck.");
            //     return;
            // }

            // if (potionObjectGrabbable != null)
            // {
            //     potionObjectGrabbable.Drop();
            //     potionObjectGrabbable = null;
            //     PlayerInventory.wasHoldingPotion = false;
            //     Debug.Log("🧪 Dropped potion.");
            //     return;
            // }
        }
    }

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
    // void Start()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }




    // [SerializeField] private Transform playerCameraTransform;
    // [SerializeField] private Transform objectGrabPointTransform;
    // [SerializeField] private Transform duckObjectGrabPointTransform;
    // [SerializeField] private Transform potionObjectGrabPointTransform;
    // [SerializeField] private Transform keyGrabPointTransform;

    // [SerializeField] private LayerMask pickUpLayerMask;
    // [SerializeField] private LayerMask duckPickUpLayerMask;
    // [SerializeField] private LayerMask potionPickUpLayerMask;
    // [SerializeField] private LayerMask keyPickUpLayerMask; 



    // // For Lock Interactable
    // public static PlayerPickUpDrop Instance; // Singleton pattern


    // private ObjectGrabbable objectGrabbable;
    // private DuckObjectGrabbable duckObjectGrabbable;
    // private PotionObjectGrabbable potionObjectGrabbable;
    // private KeyObjectGrabbable keyObjectGrabbable;


    // // Keeping the Duck and Potion in the Inventory
    // public GameObject duck; // The duck object in the player scene
    // public GameObject potion; // The potion object


    // private void Awake()
    // {
    //     // for Key grabbable
    //     Instance = this; // Set reference
    // }

    // private void Start()
    // {
    //     //StartCoroutine(DelayedRegrabHeldItems());
    //     StartCoroutine(RestoreHeldItemsAfterSceneLoad());
    //     //duckObjectGrabbable.GetComponent<Collider>().enabled = false;

    //     if (duckObjectGrabbable != null)
    //     {
    //         duckObjectGrabbable.GetComponent<Collider>().enabled = false;
    //     }
    // }

    // private IEnumerator RestoreHeldItemsAfterSceneLoad()
    // {
    //     yield return null; // Wait one frame to ensure scene is ready

    //     if (PlayerInventory.wasHoldingDuck && PlayerInventory.currentDuck != null)
    //     {
    //         duckObjectGrabbable = PlayerInventory.currentDuck.GetComponent<DuckObjectGrabbable>();
    //         if (duckObjectGrabbable != null)
    //         {
    //             duckObjectGrabbable.Grab(duckObjectGrabPointTransform);
    //             Debug.Log("Restored duck after scene load.");
    //         }
    //         else
    //         {
    //             Debug.LogError("DuckObjectGrabbable is null.");
    //         }
    //     }

    //     if (PlayerInventory.wasHoldingPotion && PlayerInventory.currentPotion != null)
    //     {
    //         potionObjectGrabbable = PlayerInventory.currentPotion.GetComponent<PotionObjectGrabbable>();
    //         if (potionObjectGrabbable != null)
    //         {
    //             potionObjectGrabbable.Grab(potionObjectGrabPointTransform);
    //             Debug.Log("Restored potion after scene load.");
    //         }
    //         else
    //         {
    //             Debug.LogError("PotionObjectGrabbable is null.");
    //         }
    //     }
    // }

    // // private IEnumerator RestoreHeldItemsAfterSceneLoad()
    // // {
    // //     yield return null; // Wait one frame to ensure scene is ready

    // //     if (PlayerInventory.wasHoldingDuck && PlayerInventory.currentDuck != null)
    // //     {
    // //         duckObjectGrabbable = PlayerInventory.currentDuck.GetComponent<DuckObjectGrabbable>();
    // //         duckObjectGrabbable.Grab(duckObjectGrabPointTransform);
    // //         Debug.Log("Restored duck after scene load.");
    // //     }

    // //     if (PlayerInventory.wasHoldingPotion && PlayerInventory.currentPotion != null)
    // //     {
    // //         potionObjectGrabbable = PlayerInventory.currentPotion.GetComponent<PotionObjectGrabbable>();
    // //         potionObjectGrabbable.Grab(potionObjectGrabPointTransform);
    // //         Debug.Log("Restored potion after scene load.");
    // //     }
    // // }

    // // private IEnumerator RestoreHeldObjects()
    // // {
    // //     yield return null; // Wait one frame to ensure scene load completes

    // //     if (PlayerInventory.currentDuck != null && PlayerInventory.wasHoldingDuck)
    // //     {
    // //         duckObjectGrabbable = PlayerInventory.currentDuck.GetComponent<DuckObjectGrabbable>();
    // //         duckObjectGrabbable.Grab(duckObjectGrabPointTransform);
    // //         Debug.Log("Restored duck to player's hand.");
    // //     }

    // //     if (PlayerInventory.currentPotion != null && PlayerInventory.wasHoldingPotion)
    // //     {
    // //         potionObjectGrabbable = PlayerInventory.currentPotion.GetComponent<PotionObjectGrabbable>();
    // //         potionObjectGrabbable.Grab(potionObjectGrabPointTransform);
    // //         Debug.Log("Restored potion to player's hand.");
    // //     }
    // // }


    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         float pickUpDistance = 2f;

    //         // --- Check for Key ---
    //         if (keyObjectGrabbable == null &&
    //             Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit keyHit, pickUpDistance, keyPickUpLayerMask))
    //         {
    //             if (keyHit.transform.TryGetComponent(out KeyObjectGrabbable key))
    //             {
    //                 keyObjectGrabbable = key;
    //                 key.Grab(keyGrabPointTransform);
    //                 PlayerInventory.hasKey = true;
    //                 Debug.Log("Picked up key.");
    //                 return;
    //             }
    //         }

    //         // --- Check for Duck ---
    //         if (duckObjectGrabbable == null &&
    //             Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit duckHit, pickUpDistance, duckPickUpLayerMask))
    //         {
    //             if (duckHit.transform.TryGetComponent(out DuckObjectGrabbable duck))
    //             {
    //                 duckObjectGrabbable = duck;
    //                 duck.Grab(duckObjectGrabPointTransform);

    //                 PlayerInventory.currentDuck = duckObjectGrabbable.gameObject;
    //                 PlayerInventory.wasHoldingDuck = true;
    //                 DontDestroyOnLoad(duckObjectGrabbable.gameObject);
    //                 Debug.Log("Picked up duck.");
    //                 return;
    //             }
    //         }

    //         // --- Check for Potion ---
    //         if (potionObjectGrabbable == null &&
    //             Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit potionHit, pickUpDistance, potionPickUpLayerMask))
    //         {
    //             if (potionHit.transform.TryGetComponent(out PotionObjectGrabbable potion))
    //             {
    //                 potionObjectGrabbable = potion;
    //                 potion.Grab(potionObjectGrabPointTransform);

    //                 PlayerInventory.currentPotion = potionObjectGrabbable.gameObject;
    //                 PlayerInventory.wasHoldingPotion = true;

    //                 DontDestroyOnLoad(potionObjectGrabbable.gameObject);
    //                 Debug.Log("Picked up potion.");
    //                 return;
    //             }
    //         }

    //         // --- Check for Generic Object ---
    //         if (objectGrabbable == null &&
    //             Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit objectHit, pickUpDistance, pickUpLayerMask))
    //         {
    //             if (objectHit.transform.TryGetComponent(out ObjectGrabbable obj))
    //             {
    //                 objectGrabbable = obj;
    //                 obj.Grab(objectGrabPointTransform);
    //                 Debug.Log("Picked up object.");
    //                 return;
    //             }
    //         }

    //         // --- Drop Objects ---
    //         if (objectGrabbable != null)
    //         {
    //             objectGrabbable.Drop();
    //             objectGrabbable = null;
    //             Debug.Log("Dropped object.");
    //             return;
    //         }

    //         if (duckObjectGrabbable != null)
    //         {
    //             duckObjectGrabbable.Drop();
    //             duckObjectGrabbable = null;
    //             PlayerInventory.wasHoldingDuck = false;
    //             Debug.Log("Dropped duck.");
    //             return;
    //         }

    //         if (potionObjectGrabbable != null)
    //         {
    //             potionObjectGrabbable.Drop();
    //             potionObjectGrabbable = null;
    //             PlayerInventory.wasHoldingPotion = false;
    //             Debug.Log("Dropped potion.");
    //             return;
    //         }
    //     }
    // }



    // // For Key Pickup
    // public bool IsHoldingKey()
    // {
    //     Debug.Log("Checking if holding key: " + (keyObjectGrabbable != null));
    //     return keyObjectGrabbable != null;
    // }

    // public void RemoveKey()
    // {
    //     if (keyObjectGrabbable != null)
    //     {
    //         Destroy(keyObjectGrabbable.gameObject);
    //         keyObjectGrabbable = null;
    //         PlayerInventory.hasKey = false;
    //         Debug.Log("Key removed from inventory.");
    //     }
    // }


    // //Inventory Code
    // private void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // private void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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

    //     StartCoroutine(RestoreHeldItemsAfterSceneLoad());
    // }
}
