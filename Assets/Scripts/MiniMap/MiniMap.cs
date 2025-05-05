using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        // Try to find the player in the scene automatically by tag or name
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming the player has the "Player" tag

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene. Please assign the player to the MiniMap script.");
        }
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            // If the playerTransform is still null, exit the update to avoid errors
            return;
        }

        // Safely access the player's position if the transform is valid
        Vector3 playerPosition = playerTransform.position;
        // Update the minimap with the player's position (Example: move minimap icon)
        UpdateMiniMapPosition(playerPosition);
    }

    private void UpdateMiniMapPosition(Vector3 position)
    {
        // Update minimap icon or camera position based on the player's position
        transform.position = new Vector3(position.x, transform.position.y, position.z);
    }


    // [SerializeField] private Transform player;

    // public Transform playerTransform; // Reference to the player's transform (set in inspector or via script)

    // private void Start()
    // {
    //     // Ensure playerTransform is assigned before using it
    //     if (playerTransform == null)
    //     {
    //         Debug.LogError("Player transform is not assigned to the MiniMap!");
    //     }
    // }

    // private void Update()
    // {
    //     if (playerTransform == null)
    //     {
    //         // Handle case where playerTransform is missing (player might have been destroyed or not set)
    //         Debug.LogWarning("Player transform is missing in MiniMap script!");
    //         return; // Exit Update if no player transform is available
    //     }

    //     // Safely access the player's position if the transform is valid
    //     Vector3 playerPosition = playerTransform.position;
    //     // Update the minimap with the player's position (Example: move mini map icon)
    //     UpdateMiniMapPosition(playerPosition);
    // }

    // private void UpdateMiniMapPosition(Vector3 position)
    // {
    //     // Update minimap icon or camera position based on the player's position
    //     // For example:
    //     transform.position = new Vector3(position.x, transform.position.y, position.z);
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     Vector3 newPosition = player.position;
    //     newPosition.y = transform.position.y;
    //     transform.position = newPosition;        
    // }

    // void Start()
    // {
    //     if (player == null)
    //     {
    //         GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
    //         if (foundPlayer != null)
    //         {
    //             player = foundPlayer.transform;
    //         }
    //         else
    //         {
    //             Debug.LogError("Player GameObject not found in scene. Make sure it's tagged 'Player'.");
    //         }
    //     }
    // }
}
