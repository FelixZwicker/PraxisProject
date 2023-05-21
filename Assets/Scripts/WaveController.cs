using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveController : MonoBehaviour
{
    public ShopController shopControllerScript;
    public PlayerController playerControllerScript;
    public PauseMenu pauseScript;
    public RemainingEnemies remainingEnemiesScript;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI roundNr;
    public GameObject[] enemies;
    public GameObject ShopUI;
    public GameObject IngameUI;

    public int waveCounter = 1;                     //keeps track of how many waves were survived
    public float maxWaveDuration = 60f;
    public float enemySpawnCooldown = 1.5f;
    public float currentWaveDuration = 60f;        //how long a wave keeps spawning enemys 
    public bool finishedWave = false;

    private Vector2 spawnPosition;
    private int enemyArrayLenght = 1;
    private bool gameStarted = false;
    private bool clearing = false;

    void Update()
    {
        if (!gameStarted)
        {
            StartWave();
            gameStarted = true;
        }
        UpdateWaveTime();
        DisplayWaveTime();

        //debug - skip wave
        if(Input.GetKeyDown(KeyCode.L))
        {
            currentWaveDuration = 0.5f;
        }

        if(clearing)
        {
            WaitForPlayerClearing();
        }

        roundNr.text = waveCounter.ToString();
    }

    public void StartWave()
    {
        finishedWave = false;
        pauseScript.UnfreezeGame();
        currentWaveDuration = maxWaveDuration;
        ShopInteraction.openedShop = false;
        remainingEnemiesScript.wasPlayed = false;

        Enemy_Health.canDropMachinGun = true;
        if(shopControllerScript.boughtLaserWeapon)
        {
            Enemy_Health.canDropLaser = true;
        }
        if(shopControllerScript.boughtRocketLauncher)
        {
            Enemy_Health.canDropExplosion = true;
        }


        if (waveCounter % 5 == 0)
        {
            enemyArrayLenght += 1;
        }
        StartCoroutine(SpawnEnemys());
    }

    public void WaveOver()
    {
        //stop player controlles
        pauseScript.FreezeGame();

        //adjust gamestats
        waveCounter++;
        UpdateDifficulty();

        //open shop
        ShopUI.SetActive(true);
        IngameUI.SetActive(false);
        shopControllerScript.LoadShop();
    }

    void UpdateDifficulty() //movement speed, damage anheben oder gleichbleibend
    {
        maxWaveDuration += waveCounter * 2;
        enemySpawnCooldown *= 0.9f;
        Enemy_Health.enemyMaxHealth += 1f;
    }

    IEnumerator SpawnEnemys()
    {
        int spawnNr = Random.Range(1, 7);
        SetSpawnPosition(spawnNr);

        if (currentWaveDuration > 0)
        {
            if(Vector2.Distance(spawnPosition, GameObject.FindGameObjectWithTag("Player").transform.position) > 6)
            {
                Instantiate(enemies[Random.Range(0, enemyArrayLenght)], spawnPosition, Quaternion.identity);        //spawns random enemy from array enemys
                yield return new WaitForSeconds(enemySpawnCooldown);
            }
            StartCoroutine(SpawnEnemys());
        }
        else
        {
            clearing = true;
        }
    }

    void WaitForPlayerClearing()
    {
        if(GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            clearing = false;
            finishedWave = true;
        }
    }

    void SetSpawnPosition(int positionNr)
    {
        switch(positionNr)
        {
            case 1:
                spawnPosition = new Vector2(-50, 12);
                break;
            case 2:
                spawnPosition = new Vector2(-50, 4);
                break;
            case 3:
                spawnPosition = new Vector2(-22, -10);
                break;
            case 4:
                spawnPosition = new Vector2(-20, 7.5f);
                break;
            case 5:
                spawnPosition = new Vector2(-12, 3.5f);
                break;
            case 6:
                spawnPosition = new Vector2(-33, -12);
                break;
            default:
                spawnPosition = new Vector2(0, 0);
                break;
        }
    }

    void UpdateWaveTime()
    {
        if (currentWaveDuration > 0)
        {
            currentWaveDuration -= Time.deltaTime;
        }
    }

    void DisplayWaveTime()
    {
        float minutes = Mathf.FloorToInt(currentWaveDuration / 60);
        float seconds = Mathf.FloorToInt(currentWaveDuration % 60);

        if(currentWaveDuration < 0)
        {
            timeText.text = "00:00";
        }
        else
        {
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
