using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyHealthbar enemyHealthbarScript;
    public GameObject deathExplosion;

    public GameObject moneyPrefab;
    public GameObject[] weaponPrefabs;

    public static bool canDropLaser;
    public static bool canDropExplosion;
    public static bool canDropMachinGun;

    public static float enemyMaxHealth = 1f;
    public int dropValue;

    private Laser laserScript;
    private PlayerController playerControllerScript;
    private Vector3 dropOffset;
    private float currentHealth;

    private void Start()
    {
        laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        currentHealth = enemyMaxHealth;
        dropOffset.Set(0, 0.5f, 0);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            EnemyKilled();
        }
        enemyHealthbarScript.SetEnemyHealthbar(enemyMaxHealth, currentHealth);
    }


    public IEnumerator EnemyTakeDamage(float _damage)
    { 
        currentHealth -= _damage;
        yield return new WaitForSeconds(0.1f);
    }
    
    void DropMoney()
    {
        GameObject Coin;
        Coin = Instantiate(moneyPrefab, transform.position + dropOffset, Quaternion.identity);
        CollectMoney collectMoneyScript = Coin.GetComponent<CollectMoney>();
        collectMoneyScript.UpdateMoneyAmount(dropValue);
    }

    void DropWeapon()
    {
        if (Random.value < 0.15f && !CollectWeapon.rocketLauncherEquipped && canDropExplosion)
        { 
            Instantiate(weaponPrefabs[0], transform.position, Quaternion.identity);
            canDropExplosion = false;
        }
        else if (Random.value < 0.15f && !laserScript.enabled && canDropLaser) 
        {
            Instantiate(weaponPrefabs[2], transform.position, Quaternion.identity);
            canDropLaser = false;
        }
        else if(Random.value < 0.2f && !CollectWeapon.machineGunEquipped && canDropMachinGun)
        {
            Instantiate(weaponPrefabs[1], transform.position, Quaternion.identity);
            canDropMachinGun = false;
        }
    }

    private void EnemyKilled()
    {
        StartCoroutine(PlayParticleSystemOnDeath());
        DropMoney();
        DropWeapon();
        playerControllerScript.score += 10;  // -> singleton
        Destroy(gameObject);
    }

    IEnumerator PlayParticleSystemOnDeath()
    {
        GameObject particle = Instantiate(deathExplosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(deathExplosion.GetComponent<ParticleSystem>().main.duration);
       DestroyImmediate(particle);
    }
}
