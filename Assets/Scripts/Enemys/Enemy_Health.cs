using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public static int health;
    public static int damageTaken = 1;

    private void Start()
    {
        health = 2;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.transform.gameObject.tag == "Bullet")
        {
            if (health > 1)
            {
                health -= damageTaken;
            }
            else
            {
                Destroy(gameObject);
                PlayerController.money += 20;
            }
        }
    }
}
