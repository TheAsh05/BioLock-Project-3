using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject playerPrefab; // The prefab of the player to spawn
    public Transform spawnPoint; // The spawn point for the player
    private GameObject playerInstance;

    private static PlayerSpawnManager instance;

    // Singleton pattern to ensure only one instance exists across scenes
    public static PlayerSpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerSpawnManager>();
                if (instance == null)
                {
                    GameObject manager = new GameObject("PlayerSpawnManager");
                    instance = manager.AddComponent<PlayerSpawnManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Prevent duplicate instances of the spawn manager
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject); // Persist this object across scene loads
        instance = this;
    }

    private void Start()
    {
        // Ensure the player is only spawned if it doesn't already exist
        if (playerInstance == null)
        {
            SpawnPlayer();
        }
    }

    // Method to spawn the player if it doesn't already exist
    public void SpawnPlayer()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            // // Check if the player already exists in the scene
            playerInstance = GameObject.FindWithTag("Player");

            // // If a player instance exists, destroy it before spawning a new one
            // if (playerInstance != null)
            // {
            //     //Destroy(playerInstance);
            // }

            // Instantiate the player at the spawn point
            //playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            //playerInstance.tag = "Player"; // Ensure the new player is tagged correctly

            playerInstance.transform.position = spawnPoint.position;

            // Ensure the FirstPersonController is enabled
            FirstPersonController controller = playerInstance.GetComponentInChildren<FirstPersonController>();
            if (controller != null)
            {
                controller.enabled = true;  // Enable the controller for movement
            }
            else
            {
                Debug.LogWarning("FirstPersonController not found on player prefab!");
            }
        }
    }


    // Method to update spawn point if needed
    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
