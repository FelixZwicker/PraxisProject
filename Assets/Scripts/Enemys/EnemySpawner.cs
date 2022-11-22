using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius = 3;
    public float time = 1.5f;

    public GameObject[] enemys;


    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        Vector2 spawnPos = GameObject.Find("Player").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        
        if(spawnPos.y <= 2.0 && spawnPos.y >=-2.5 && spawnPos.x <= 3.5 && spawnPos.x >= -5.5)
        {
            Instantiate(enemys[Random.Range(0, enemys.Length)], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
        StartCoroutine(SpawnEnemy());
    }
}
