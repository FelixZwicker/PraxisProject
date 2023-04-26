using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveController : MonoBehaviour
{
    public ShopController shopControllerScript;
    public PlayerController playerControllerScript;
    public PauseMenu pauseScript;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI roundNr;
    public GameObject[] enemies;
    public GameObject ShopUI;
    public GameObject IngameUI;
    public static bool canUseHeal;

    public int waveCounter = 1;                     //keeps track of how many waves were survived
    private float currentWaveDuration = 60f;        //how long a wave keeps spawning enemys 
    public float maxWaveDuration = 60f;
    public float enemySpawnCooldown = 1.5f;
    private float spawnRadius = 20;
    private bool gameStarted = false;

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

        roundNr.text = waveCounter.ToString();
    }

    public void StartWave()
    {
        pauseScript.UnfreezeGame();
        currentWaveDuration = maxWaveDuration;
        canUseHeal = true;
        StartCoroutine(SpawnEnemys(waveCounter));
    }

    void WaveOver()
    {
        //stop player controlles
        pauseScript.FreezeGame();

        // destroy remaining ememys
        GameObject[] remainingCloseEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] remainingRangedEnemies = GameObject.FindGameObjectsWithTag("Ranged");
        foreach (GameObject closeEnemy in remainingCloseEnemies)
        {
            GameObject.Destroy(closeEnemy);
        }

        foreach (GameObject rangedEnemy in remainingRangedEnemies)
        {
            GameObject.Destroy(rangedEnemy);
        }

        //adjust gamestats
        waveCounter++;
        UpdateDifficulty();

        //open shop
        ShopUI.SetActive(true);
        IngameUI.SetActive(false);
        shopControllerScript.LoadShop();
    }

    void UpdateDifficulty()
    {
        maxWaveDuration += waveCounter * 2;
        enemySpawnCooldown *= 0.9f;
        Enemy_Health.enemyMaxHealth += 1;
    }

    IEnumerator SpawnEnemys(int numberOfWave)
    {
        int enemyArrayLenght = 1; 

        if(numberOfWave > 4)
        {
            enemyArrayLenght = 2;
        }
        else
        {
            enemyArrayLenght = 4;
        }
        Vector2 spawnPos = GameObject.Find("Player").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        if(currentWaveDuration > 0)
        {
            if(spawnPos.x < 33 && spawnPos.x > -31 && spawnPos.y < 22 && spawnPos.y > -16)
            {
                Instantiate(enemies[Random.Range(0, enemyArrayLenght)], spawnPos, Quaternion.identity);        //spawns random enemy from array enemys
                yield return new WaitForSeconds(enemySpawnCooldown);
            }
            
            StartCoroutine(SpawnEnemys(waveCounter));
        }
        else
        {
            new WaitForSeconds(2f);
            WaveOver();
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
