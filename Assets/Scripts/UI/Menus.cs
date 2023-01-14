using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    // Start Menu
    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
    //---------------------------------------------------

    //Pause Menu

}
