using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public Enemy_Healthbar Enemy_HealthbarScript;

    public static int damageTaken = 1;
    public static int enemyMaxHealth = 1;

    private int currentHealth;
    private bool hit = true;


    private void Start()
    {
        currentHealth = enemyMaxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            EnemyKilled();
        }
        Enemy_HealthbarScript.SetEnemyHealthbar(enemyMaxHealth, currentHealth);
    }

    public IEnumerator EnemyTakeDamage(int Damage)
    {
        hit = false;
        currentHealth -= Damage;
        yield return new WaitForSeconds(0.1f);
        hit = true;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.transform.gameObject.CompareTag("Bullet"))
        {
            if (currentHealth > 0)
            {
                currentHealth -= collider.transform.gameObject.GetComponent<Bullet>().damage;
            }
            else
            {
                EnemyKilled();
            }
        }
    }

    private void EnemyKilled()
    {
        Destroy(gameObject);
        PlayerController.money += 50;
    }
}
