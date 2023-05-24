using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMoney : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private ShopInteraction shopInteractionScipt;
    private GameObject player;

    private int amount;
    private float distanceToPlayer;

    private void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shopInteractionScipt = GameObject.FindGameObjectWithTag("Shop").GetComponent<ShopInteraction>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);

        //coin moves to player in small radius
        if(distanceToPlayer < 0.5f)
        {
            playerControllerScript.money += amount;
            Destroy(gameObject);
        }

        if(shopInteractionScipt.openedShop)
        {
            Destroy(gameObject);
        }       
    }

    private void FixedUpdate()
    {
        if (distanceToPlayer < 6)
        {
            MoveToPlayer();
        }
    }

    public void UpdateMoneyAmount(int newAmount)
    {
        amount = newAmount;
    }

    void MoveToPlayer()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, 0.5f);
    }
}
