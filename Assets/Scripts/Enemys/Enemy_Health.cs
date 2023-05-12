using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public Enemy_Healthbar EnemyHealthbarScript;
    //public CollectMoney CollectMoneyScript;

    public GameObject MoneyPrefab;
    public GameObject[] WeaponPrefabs;

    public static float damageTaken = 1f;
    public static float enemyMaxHealth = 1f;

    public int dropValue;

    private Vector3 dropOffset;
    private float currentHealth;

    private void Start()
    {
        currentHealth = enemyMaxHealth;
        dropOffset.Set(0, 0.5f, 0);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            EnemyKilled();
        }
        EnemyHealthbarScript.SetEnemyHealthbar(enemyMaxHealth, currentHealth);
    }


    public IEnumerator EnemyTakeDamage(float Damage)
    { 
        currentHealth -= Damage;
        yield return new WaitForSeconds(0.1f);
    }
    
    void DropMoney()
    {
        GameObject Coin;
        Coin = Instantiate(MoneyPrefab, transform.position + dropOffset, Quaternion.identity);
        CollectMoney collectMoneyScript = Coin.GetComponent<CollectMoney>();
        collectMoneyScript.UpdateMoneyAmount(dropValue);
    }

    void DropWeapon()
    {
        Shooting shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();

        if (Random.value < 0.15f && shootingScript.bulletPrefab != Resources.Load("Prefab/Weapon/ExplosionBullet"))
        {
            Instantiate(WeaponPrefabs[0], transform.position, Quaternion.identity);
        }
        else if(Random.value < 0.15f && shootingScript.bulletPrefab != Resources.Load("Prefab/Weapon/MashineGunBullet"))
        {
            Instantiate(WeaponPrefabs[1], transform.position, Quaternion.identity);
        }
    }

    private void EnemyKilled()
    {
        DropMoney();
        DropWeapon();
        PlayerController.score += 10;  // -> singleton
        Destroy(gameObject);
    }
}
