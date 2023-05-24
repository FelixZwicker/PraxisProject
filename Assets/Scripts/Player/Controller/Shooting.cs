using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject machineGunAmmoUI;
    public GameObject rocketLauncherAmmoUI;
    public TextMeshProUGUI machineGunAmmoDisplay;
    public TextMeshProUGUI rocketLauncherAmmoDisplay;
    public Image reloadIndicator;
    public Animator animator;

    public int maxMachineGunAmmo = 25;
    public int maxRocketLauncherAmmo = 5;
    public float machineGunReloadSpeed = 6f;
    public float rocketLaucnherReloadSpeed = 2.5f;  
    public int currentMachineGunAmmo;
    public int currentRocketLauncherAmmo;
    public bool canShoot = true;
    public bool permanendFire = false;
    public bool stopReloading = false;

    private Vector2 mouseScreenPosition;
    private float bulletForce = 45f;
    private float rocketBulletForce = 20f;
    private float fireRate = 0.1f;
    private bool startedPermanentFireCoroutine = false;
    private float reloadTimer;
    private bool reloadingMachineGun = false;
    private bool reloadingRocketLauncher = false;
    private bool reloading = false;

    void Start()
    {
        currentMachineGunAmmo = maxMachineGunAmmo;
        currentRocketLauncherAmmo = maxRocketLauncherAmmo;
        reloadTimer = 0;
        machineGunAmmoDisplay.SetText(currentMachineGunAmmo.ToString() + " / " + maxMachineGunAmmo.ToString());
        rocketLauncherAmmoDisplay.SetText(currentMachineGunAmmo.ToString() + " / " + maxRocketLauncherAmmo.ToString());
    }

    void Update()
    {
        //shooting
        if (Input.GetButtonDown("Fire1") && canShoot && (!permanendFire || CollectWeapon.rocketLauncherEquipped))
        {
            Shoot();
        }
        else if (Input.GetButton("Fire1") && canShoot && permanendFire && !startedPermanentFireCoroutine && CollectWeapon.machineGunEquipped) //used when permanent fire was bought for machinegun
        {
            StartCoroutine(PermanetFire());
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R) && canShoot && CollectWeapon.machineGunEquipped)
        {
            ReloadMachineGun();
        }
        if (Input.GetKeyDown(KeyCode.R) && canShoot && CollectWeapon.rocketLauncherEquipped)
        {
            ReloadRocketLauncher();
        }

        machineGunAmmoDisplay.SetText(currentMachineGunAmmo.ToString() + " / " + maxMachineGunAmmo.ToString());
        rocketLauncherAmmoDisplay.SetText(currentRocketLauncherAmmo.ToString() + " / " + maxRocketLauncherAmmo.ToString());

        ReloadIndicatorDisplay();

        StopReloading();

        mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Shoot()
    {
        if(currentMachineGunAmmo > 0 && currentRocketLauncherAmmo > 0 && reloading == false)
        {
            //shoot directly at mouse position when its not to close to player
            //else shoot directly forward
            if(Vector2.Distance(mouseScreenPosition, firePoint.transform.position) > 2)
            {
                Vector2 direction = (mouseScreenPosition - (Vector2)firePoint.transform.position).normalized;
                firePoint.up = direction;
            }
            animator.Play("PlayerRifleShooting");
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            //diffrent forces for weapontypes
            if(CollectWeapon.machineGunEquipped)
            {
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(firePoint.up * rocketBulletForce, ForceMode2D.Impulse);
            }

            if(CollectWeapon.machineGunEquipped)
            {
                --currentMachineGunAmmo;
            }
            else if(CollectWeapon.rocketLauncherEquipped)
            {
                --currentRocketLauncherAmmo;
            }
        }
    }

    //show reload time in ui
    void ReloadIndicatorDisplay()
    {
        if (reloadingMachineGun)
        {
            reloadTimer += 1 / machineGunReloadSpeed * Time.deltaTime;
            reloadIndicator.fillAmount = reloadTimer;
        }
        else if (reloadingRocketLauncher)
        {
            reloadTimer += 1 / rocketLaucnherReloadSpeed * Time.deltaTime;
            reloadIndicator.fillAmount = reloadTimer;
        }
        else
        {
            reloadTimer = 0;
        }
    }

    //when reload time needs to be resetted 
    void StopReloading()
    {
        if(stopReloading)
        {
            stopReloading = false;
            reloading = false;
            reloadingMachineGun = false;
            reloadingRocketLauncher = false;
            currentMachineGunAmmo = maxMachineGunAmmo;
            currentRocketLauncherAmmo = maxRocketLauncherAmmo;
            reloadIndicator.fillAmount = 1;
        }
    }

    IEnumerator PermanetFire()
    {
        startedPermanentFireCoroutine = true;
        Shoot();
        yield return new WaitForSeconds(fireRate);
        startedPermanentFireCoroutine = false;
    }

    void ReloadMachineGun()
    {
        if(reloadingMachineGun == false)
        {
            reloadingMachineGun = true;
            reloading = true;
            StartCoroutine(ReloadingMachineGun()); 
        }
    }

    void ReloadRocketLauncher()
    {
        if (reloadingRocketLauncher == false)
        {
            reloadingRocketLauncher = true;
            reloading = true;
            StartCoroutine(ReloadingRocketLauncher());
        }
    }

    IEnumerator ReloadingMachineGun()
    {
        animator.Play("PlayerRifleReload");
        yield return new WaitForSeconds(machineGunReloadSpeed);
        currentMachineGunAmmo = maxMachineGunAmmo;
        reloading = false;
        reloadingMachineGun = false;
    }

    IEnumerator ReloadingRocketLauncher()
    {
        animator.Play("PlayerRifleReload");
        yield return new WaitForSeconds(rocketLaucnherReloadSpeed);
        currentRocketLauncherAmmo = maxRocketLauncherAmmo;
        reloading = false;
        reloadingRocketLauncher = false;
    }

    public void ChangeAmmoDisplay()
    {
        if(CollectWeapon.machineGunEquipped)
        {
            machineGunAmmoUI.SetActive(true);
            rocketLauncherAmmoUI.SetActive(false);
        }
        else if(CollectWeapon.rocketLauncherEquipped)
        {
            rocketLauncherAmmoUI.SetActive(true);
            machineGunAmmoUI.SetActive(false);
        }
    }
}
