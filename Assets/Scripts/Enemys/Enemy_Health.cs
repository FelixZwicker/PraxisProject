using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public Enemy_Healthbar Enemy_HealthbarScript;

    public static float damageTaken = 1f;
    public static float enemyMaxHealth = 1f;

    private float currentHealth;

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


    public IEnumerator EnemyTakeDamage(float Damage)
    { 
        currentHealth -= Damage;
        yield return new WaitForSeconds(0.1f);
    }
   
    /*
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.transform.gameObject.CompareTag("Bullet"))
        {
            EnemyTakeDamage(collider.transform.gameObject.GetComponent<Bullet>().damage);
        }
    }
    */
    

    private void EnemyKilled()
    {   
        PlayerController.money += 50;
        PlayerController.score += 10;
        Destroy(gameObject);
    }
}
