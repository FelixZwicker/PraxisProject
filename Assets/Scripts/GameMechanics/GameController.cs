using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public EnemyHealth[] enemyHealthsScripts;

    private GameObject machineGunBulletPrefab;
    private GameObject rocketLauncherPrefab;

    private MashineGunBullet machineGunBulletScript;
    private ExplosionBullet explosionBulletScript;

    //resetting all statics and prefabs
    void Start()
    {
        enemyHealthsScripts[0].enemyMaxHealth = 2;
        enemyHealthsScripts[1].enemyMaxHealth = 1;
        enemyHealthsScripts[2].enemyMaxHealth = 1;
        enemyHealthsScripts[3].enemyMaxHealth = 0.5f;

        CollectWeapon.machineGunEquipped = true;
        CollectWeapon.rocketLauncherEquipped = false;
        CollectWeapon.laserGunEquipped = false;

        machineGunBulletPrefab = Resources.Load("Prefab/Player/Weapon/MashineGunBullet") as GameObject;
        machineGunBulletScript = machineGunBulletPrefab.GetComponent<MashineGunBullet>();
        rocketLauncherPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        explosionBulletScript = rocketLauncherPrefab.GetComponent<ExplosionBullet>();

        machineGunBulletScript.damage = 1;
        explosionBulletScript.damage = 2;
        explosionBulletScript.enviromentalDamage = 1;
        explosionBulletScript.radius = 2.8f;
    }
}
