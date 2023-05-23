using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyHealthbar EnemyHealthbarScript;
    public GameObject DeathExplosion;

    public GameObject MoneyPrefab;
    public GameObject[] WeaponPrefabs;

    public static bool canDropLaser;
    public static bool canDropExplosion;
    public static bool canDropMachinGun;

    public static float damageTaken = 1f;
    public static float enemyMaxHealth = 1f;
    public int dropValue;

    private Laser laserScript;
    private Vector3 dropOffset;
    private float currentHealth;

    private void Start()
    {
        laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();

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
        if (Random.value < 0.15f && !CollectWeapon.RocketLauncherEquipped && canDropExplosion)
        { 
            Instantiate(WeaponPrefabs[0], transform.position, Quaternion.identity);
            canDropExplosion = false;
        }
        else if (Random.value < 0.15f && !laserScript.enabled && canDropLaser) 
        {
            Instantiate(WeaponPrefabs[2], transform.position, Quaternion.identity);
            canDropLaser = false;
        }
        else if(Random.value < 0.2f && !CollectWeapon.MachineGunEquipped && canDropMachinGun)
        {
            Instantiate(WeaponPrefabs[1], transform.position, Quaternion.identity);
            canDropMachinGun = false;
        }
    }

    private void EnemyKilled()
    {
        StartCoroutine(PlayParticleSystemOnDeath());
        DropMoney();
        DropWeapon();
        PlayerController.score += 10;  // -> singleton
        Destroy(gameObject);
    }

    IEnumerator PlayParticleSystemOnDeath()
    {
        GameObject particle = Instantiate(DeathExplosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(DeathExplosion.GetComponent<ParticleSystem>().main.duration);
       DestroyImmediate(particle);
    }
}
