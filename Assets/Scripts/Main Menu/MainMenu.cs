using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Load Scene
    public void Play()
    {
        //I can manage this with numbers if I use the buildScene route. Check 9 minutes on video in Design Doc
        SceneManager.LoadScene("Level 1");
    }

    //This as suggested when I typed in play
    //public void OnPlayerConnected(NetworkPlayer player)
    //{
        
    //})

    //Quit Game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has quit the game");
    }

    //Suggested when I type Quit
    //     public void OnApplicationQuit()
    //     {
    //         Application.Quit();
    //         Debug.Log("Player has quit the game");
    //     }

    void Start()
    {
        FindObjectOfType<FirstPersonController>();

        FirstPersonController controller = FindObjectOfType<FirstPersonController>();
        if (controller != null)
            {
                Destroy(controller.transform.parent.gameObject);
            }
    }
}
