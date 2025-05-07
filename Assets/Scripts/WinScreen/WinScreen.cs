using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    void Start()
    {
        // Destroy the First Person Controller (and its parent, if applicable)
        FirstPersonController controller = FindObjectOfType<FirstPersonController>();
        if (controller != null)
        {
            Destroy(controller.transform.parent.gameObject);
        }

        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        // FindObjectOfType<FirstPersonController>();

        // FirstPersonController controller = FindObjectOfType<FirstPersonController>();
        // if (controller != null)
        //     {
        //         Destroy(controller.transform.parent.gameObject);
        //     }
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
