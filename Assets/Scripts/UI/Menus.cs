using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    //possible functions for buttons
    public void PlayUISound()
    {
        SoundManager.PlaySounds(SoundManager.Sound.UIClickSound);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
