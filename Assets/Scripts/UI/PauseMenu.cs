using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Shooting ShootingScript;
    public GameObject PauseMenuUI;
    public GameObject InGameUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            PauseMenuUI.SetActive(true);
            InGameUI.SetActive(false);
            FreezeGame();
        }
    }

    public void FreezeGame()
    {
        Time.timeScale = 0f;
        ShootingScript.enabled = false;
    }

    public void UnfreezeGame()
    {
        Time.timeScale = 1f;
        ShootingScript.enabled = true;
    }
}
