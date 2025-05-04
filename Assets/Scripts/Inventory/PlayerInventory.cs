using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static GameObject currentDuck; // Store the duck
    public static GameObject currentPotion; // Store the potion
    public static bool hasKey = false; // Track if the key is held

    public static bool wasHoldingDuck = false;
    public static bool wasHoldingPotion = false;


    // When the scene is loaded, call this function to persist items
    public static void SaveItems(GameObject duck, GameObject potion)
    {
        currentDuck = duck;
        currentPotion = potion;
    }

    public static void ResetInventory()
    {
        currentDuck = null;
        currentPotion = null;
        hasKey = false;
        wasHoldingDuck = false;
        wasHoldingPotion = false;
    }

    // void OnLevelWasLoaded(int level)
    // {
    //     PlayerInventory.ResetInventory();
    // }


    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
