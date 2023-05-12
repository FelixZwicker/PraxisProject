using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMoney : MonoBehaviour
{
    private WaveController waveControllerScript;
    private int amount;

    private void Start()
    {
        waveControllerScript = GameObject.Find("GameController").GetComponent<WaveController>();
    }

    private void Update()
    {
        if (waveControllerScript.finishedWave)
        {
            MoveToPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerController.money += amount;
        }
    }

    public void UpdateMoneyAmount(int newAmount)
    {
        amount = newAmount;
    }

    void MoveToPlayer()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, 0.05f);
    }
}
