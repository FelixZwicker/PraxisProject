using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectWeapon : MonoBehaviour
{
    public Sprite[] WeaponSprites;
    public static bool MachineGunEquipped;
    public static bool LaserGunEquipped;
    public static bool RocketLauncherEquipped;

    private GameObject WeaponUI;
    private Shooting ShootingScript;
    private Laser LaserScript;
    private GameObject WeaponType;

    private float distanceToPlayer;
    private bool isLaserWeapon = false;
    private bool isMachineGun = true;

    private void Start()
    {
        ShootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        LaserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();

        if (gameObject.name == "ExplosionWeapon(Clone)")
        {
            WeaponType = Resources.Load("Prefab/Weapon/ExplosionBullet") as GameObject;
            isMachineGun = false;
        }
        else if(gameObject.name == "MashineGunWeapon(Clone)")
        {
            WeaponType = Resources.Load("Prefab/Weapon/MashineGunBullet") as GameObject;
            isMachineGun = true;
        }
        else if(gameObject.name == "LaserWeapon(Clone)")
        {
            isLaserWeapon = true;
        }

        WeaponUI = GameObject.Find("WeaponUI");
    }

    private void Update()
    {
        if(ShopInteraction.openedShop)
        {
            Destroy(gameObject);
        }

        distanceToPlayer = Vector2.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (distanceToPlayer < 1.2f)
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
        ShootingScript.bulletPrefab = null;
        LaserGunEquipped = true;
        MachineGunEquipped = false;
        RocketLauncherEquipped = false;
        ActivateLaserSprite();

        LaserScript.ammoUI.SetActive(false);
        LaserScript.laserTimerUI.SetActive(true);
    }

    void PrefabWeaponPickUp()
    {
        ShootingScript.canShoot = true;
        LaserScript.enabled = false;
        ShootingScript.bulletPrefab = WeaponType;
        if (isMachineGun)
        {
            MachineGunEquipped = true;
            LaserGunEquipped = false;
            RocketLauncherEquipped = false;
            ActivateMachineGunSprite();
        }
        else
        {
            RocketLauncherEquipped = true;
            LaserGunEquipped = false;
            MachineGunEquipped = false;
            ActivateRocketLauncherSprite();
        }
        LaserScript.ammoUI.SetActive(true);
        LaserScript.laserTimerUI.SetActive(false);
    }

    void ActivateLaserSprite()
    {
        WeaponUI.GetComponent<RectTransform>().sizeDelta = new Vector2(134.4706f, 74.034f);
        WeaponUI.GetComponent<Image>().sprite = WeaponSprites[2]; 
    }

    void ActivateMachineGunSprite()
    {
        WeaponUI.GetComponent<RectTransform>().sizeDelta = new Vector2(248.0147f, 89.5598f);
        WeaponUI.GetComponent<Image>().sprite = WeaponSprites[0];
    }

    void ActivateRocketLauncherSprite()
    {
        WeaponUI.GetComponent<RectTransform>().sizeDelta = new Vector2(268.1662f, 70.9835f);
        WeaponUI.GetComponent<Image>().sprite = WeaponSprites[1];
    }
}