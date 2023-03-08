using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveController : MonoBehaviour
{
    public ShopController shopControllerScript;
    public PlayerController playerControllerScript;

    public TextMeshProUGUI timeText;
    public GameObject[] enemies;
    public GameObject ShopUI;
    public GameObject IngameUI;

    public int waveCounter = 1;                     //keeps track of how many waves were survived
    private float currentWaveDuration = 60f;        //how long a wave keeps spawning enemys 
    public float maxWaveDuration = 60f;
    public float enemySpawnCooldown = 1.5f;
    private float spawnRadius = 2;
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
    }

    public void StartWave()
    {
        playerControllerScript.enabled = true;

        currentWaveDuration = maxWaveDuration;
        StartCoroutine(SpawnEnemys());
    }

    void WaveOver()
    {
        //stop player controlles
        playerControllerScript.enabled = false;

        // destroy remaining ememys
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in remainingEnemies)
        {
            GameObject.Destroy(enemy);
        }

        //adjust gamestrats
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
        enemySpawnCooldown *= 0.5f;
    }

    IEnumerator SpawnEnemys()
    {
        Vector2 spawnPos = GameObject.Find("Player").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        if(currentWaveDuration > 0)
        {
            
                Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);        //spawns random enemy from array enemys
                yield return new WaitForSeconds(enemySpawnCooldown);
            
            StartCoroutine(SpawnEnemys());
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
