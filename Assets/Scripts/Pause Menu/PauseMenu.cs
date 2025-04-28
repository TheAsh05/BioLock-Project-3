using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;

    private StarterAssets.FirstPersonController playerController; // Add this

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        // Find the player movement script
        playerController = FindObjectOfType<StarterAssets.FirstPersonController>(); 

        //Make sure the cursor is hidden and locked at the start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;

        // Disable the movement script
        if(playerController != null)
        {
            playerController.enabled = false;
        }

        // Unlock and Show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;

        // Enable player movement
        if(playerController != null)
        {
            playerController.enabled = true;
        }

        // Lock and Hide Cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
