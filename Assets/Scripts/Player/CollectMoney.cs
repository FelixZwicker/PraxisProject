using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMoney : MonoBehaviour
{
    private int amount;
    private float distanceToPlayer;

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (distanceToPlayer < 6)
        {
            MoveToPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.CompareTag("Player"))
        {
            PlayerController.money += amount;
            Destroy(gameObject);
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
