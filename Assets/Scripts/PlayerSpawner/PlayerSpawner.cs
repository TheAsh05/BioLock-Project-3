using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Assign your player prefab
    public Transform spawnPoint; // Assign this in the inspector (your PlayerSpawnPoint)

    private void Start()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            DontDestroyOnLoad(player); // Optional, if player persists between scenes
        }
        else
        {
            Debug.LogWarning("Missing playerPrefab or spawnPoint reference.");
        }
    }
}
