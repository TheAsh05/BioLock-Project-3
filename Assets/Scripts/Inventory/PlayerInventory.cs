using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    // Persistent flags and object references
    public static bool wasHoldingDuck = false;
    public static bool wasHoldingPotion = false;
    public static bool hasKey = false;

    public static GameObject currentDuck = null;
    public static GameObject currentPotion = null;

    private void Awake()
    {
        // Singleton pattern to avoid duplicates
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void ResetInventory()
    {
        wasHoldingDuck = false;
        wasHoldingPotion = false;
        hasKey = false;

        if (currentDuck != null)
        {
            Destroy(currentDuck);
            currentDuck = null;
        }

        if (currentPotion != null)
        {
            Destroy(currentPotion);
            currentPotion = null;
        }
    }




    // public static GameObject currentDuck; // Store the duck
    // public static GameObject currentPotion; // Store the potion
    // public static bool hasKey = false; // Track if the key is held

    // public static bool wasHoldingDuck = false;
    // public static bool wasHoldingPotion = false;


    // // When the scene is loaded, call this function to persist items
    // public static void SaveItems(GameObject duck, GameObject potion)
    // {
    //     currentDuck = duck;
    //     currentPotion = potion;
    // }

    // public static void ResetInventory()
    // {
    //     currentDuck = null;
    //     currentPotion = null;
    //     hasKey = false;
    //     wasHoldingDuck = false;
    //     wasHoldingPotion = false;
    // }

    // void Awake()
    // {
    //     DontDestroyOnLoad(this.gameObject);
    // }

    // // void OnLevelWasLoaded(int level)
    // // {
    // //     PlayerInventory.ResetInventory();
    // // }


    // // // Start is called before the first frame update
    // // void Start()
    // // {
        
    // // }

    // // // Update is called once per frame
    // // void Update()
    // // {
        
    // // }
}
