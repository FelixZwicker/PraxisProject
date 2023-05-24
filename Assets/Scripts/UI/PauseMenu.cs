using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Shooting shootingScript;
    public Laser laserScript;
    public GameObject pauseMenuUI;
    public GameObject inGameUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            pauseMenuUI.SetActive(true);
            inGameUI.SetActive(false);
            FreezeGame();
        }
    }

    public void FreezeGame()
    {
        Time.timeScale = 0f;
        shootingScript.enabled = false;
        laserScript.enabled = false;
    }

    public void UnfreezeGame()
    {
        Time.timeScale = 1f;
        shootingScript.enabled = true;
        if (CollectWeapon.laserGunEquipped)
        {
            laserScript.enabled = true;
        }
    }
}
