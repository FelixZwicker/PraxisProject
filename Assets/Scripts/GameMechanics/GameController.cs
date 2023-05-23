using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject machineGunBulletPrefab;
    private GameObject rocketLauncherPrefab;

    private MashineGunBullet machineGunBulletScript;
    private ExplosionBullet explosionBulletScript;
    void Start()
    {
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
