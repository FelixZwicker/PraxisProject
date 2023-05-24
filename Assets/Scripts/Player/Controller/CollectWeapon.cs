using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectWeapon : MonoBehaviour
{
    public Sprite[] weaponSprites;
    public static bool machineGunEquipped;
    public static bool laserGunEquipped;
    public static bool rocketLauncherEquipped;

    private GameObject weaponUI;
    private Shooting shootingScript;
    private Laser laserScript;
    private ShopInteraction shopInteractionScript;
    private GameObject weaponType;
    private GameObject player;

    private float distanceToPlayer;
    private bool isLaserWeapon = false;
    private bool isMachineGun = true;

    private void Start()
    {
        shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        shopInteractionScript = GameObject.FindGameObjectWithTag("Shop").GetComponent<ShopInteraction>();
        player = GameObject.FindGameObjectWithTag("Player");

        //check on wich prefab the script is
        //and wich weapon should be installed on pickup
        if (gameObject.name == "ExplosionWeapon(Clone)")
        {
            weaponType = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
            isMachineGun = false;
        }
        else if(gameObject.name == "MashineGunWeapon(Clone)")
        {
            weaponType = Resources.Load("Prefab/Player/Weapon/MashineGunBullet") as GameObject;
            isMachineGun = true;
        }
        else if(gameObject.name == "LaserWeapon(Clone)")
        {
            isLaserWeapon = true;
        }

        weaponUI = GameObject.Find("WeaponUI");
    }

    private void Update()
    {
        if(shopInteractionScript.openedShop)
        {
            Destroy(gameObject);
        }

        distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);

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
                shootingScript.currentMachineGunAmmo = shootingScript.maxMachineGunAmmo;
                shootingScript.currentRocketLauncherAmmo = shootingScript.maxRocketLauncherAmmo;
                Destroy(gameObject);
            }
        }
    }

    void LaserPickUp()
    {
        laserScript.enabled = true;
        shootingScript.canShoot = false;
        shootingScript.bulletPrefab = null;
        laserGunEquipped = true;
        machineGunEquipped = false;
        rocketLauncherEquipped = false;
        ActivateLaserSprite();

        shootingScript.machineGunAmmoUI.SetActive(false);
        shootingScript.rocketLauncherAmmoUI.SetActive(false);
        laserScript.laserTimerUI.SetActive(true);
    }

    void PrefabWeaponPickUp()
    {
        shootingScript.canShoot = true;
        laserScript.enabled = false;
        shootingScript.bulletPrefab = weaponType;
        if (isMachineGun) //machingun pickup
        {
            machineGunEquipped = true;
            laserGunEquipped = false;
            rocketLauncherEquipped = false;
            shootingScript.ChangeAmmoDisplay();
            ActivateMachineGunSprite();
        }
        else            // rocketlauncher pickup
        {
            rocketLauncherEquipped = true;
            laserGunEquipped = false;
            machineGunEquipped = false;
            shootingScript.ChangeAmmoDisplay();
            ActivateRocketLauncherSprite();
        }
        laserScript.laserTimerUI.SetActive(false);
    }

    void ActivateLaserSprite()
    {
        weaponUI.GetComponent<RectTransform>().sizeDelta = new Vector2(134.4706f, 74.034f);
        weaponUI.GetComponent<Image>().sprite = weaponSprites[2]; 
    }

    void ActivateMachineGunSprite()
    {
        weaponUI.GetComponent<RectTransform>().sizeDelta = new Vector2(248.0147f, 89.5598f);
        weaponUI.GetComponent<Image>().sprite = weaponSprites[0];
    }

    void ActivateRocketLauncherSprite()
    {
        weaponUI.GetComponent<RectTransform>().sizeDelta = new Vector2(268.1662f, 70.9835f);
        weaponUI.GetComponent<Image>().sprite = weaponSprites[1];
    }
}