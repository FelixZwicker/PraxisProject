using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void PlayUISound()
    {
        SoundManager.PlaySounds(SoundManager.Sound.UIClickSound);
    }

    // Start Menu
    public void LoadGame()
    {
        SceneManager.LoadScene("NavAgent_Test");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
