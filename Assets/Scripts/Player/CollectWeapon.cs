using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWeapon : MonoBehaviour
{
    private Shooting ShootingScript;
    private Laser LaserScript;
    private GameObject WeaponType;

    private float distanceToPlayer;
    private bool isLaserWeapon = false;

    private void Start()
    {
        ShootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        LaserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();

        if (gameObject.name == "ExplosionWeapon(Clone)")
        {
            WeaponType = Resources.Load("Prefab/Weapon/ExplosionBullet") as GameObject;
        }
        else if(gameObject.name == "MashineGunWeapon(Clone)")
        {
            WeaponType = Resources.Load("Prefab/Weapon/MashineGunBullet") as GameObject;
        }
        else if(gameObject.name == "LaserWeapon(Clone)")
        {
            isLaserWeapon = true;
        }
    }

    private void Update()
    {
        if(ShopInteraction.openedShop)
        {
            Destroy(gameObject);
        }

        distanceToPlayer = Vector2.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (distanceToPlayer < 1.5f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(isLaserWeapon)
                {
                    LaserPickUp();
                }
                else
                {
                    PrefabWeaponPickUp();
                }
                ShootingScript.currentAmmo = ShootingScript.maxAmmo;
                Destroy(gameObject);
            }
        }
    }

    void LaserPickUp()
    {
        LaserScript.enabled = true;
        ShootingScript.canShoot = false;
    }

    void PrefabWeaponPickUp()
    {
        ShootingScript.canShoot = true;
        LaserScript.enabled = false;
        ShootingScript.bulletPrefab = WeaponType;
    }
}