using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public TextMeshProUGUI ammoDisplay;
    public Image ReloadIndicator;
    public Animator animator;

    public int maxAmmo = 25;
    public float machineGunReloadSpeed = 10f;
    public float rocketLaucnherReloadSpeed = 15f;  
    public int currentAmmo;
    public bool canShoot = true;
    public bool permanendFire = false;

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
        currentAmmo = maxAmmo;
        reloadTimer = 0;
        ammoDisplay.SetText(currentAmmo.ToString() + " / " + maxAmmo.ToString());
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot && (!permanendFire || CollectWeapon.RocketLauncherEquipped))
        {
            Shoot();
        }
        else if (Input.GetButton("Fire1") && canShoot && permanendFire && !startedPermanentFireCoroutine && CollectWeapon.MachineGunEquipped)
        {
            StartCoroutine(PermanetFire());
        }

        if (Input.GetKeyDown(KeyCode.R) && canShoot && CollectWeapon.MachineGunEquipped)
        {
            ReloadMachineGun();
        }
        if (Input.GetKeyDown(KeyCode.R) && canShoot && CollectWeapon.RocketLauncherEquipped)
        {
            ReloadRocketLauncher();
        }

        ammoDisplay.SetText(currentAmmo.ToString() + " / " + maxAmmo.ToString());

        if(reloadingMachineGun)
        {
            reloadTimer += 1 / machineGunReloadSpeed * Time.deltaTime;
            ReloadIndicator.fillAmount = reloadTimer;
        }
        else if(reloadingRocketLauncher)
        {
            reloadTimer += 1 / rocketLaucnherReloadSpeed * Time.deltaTime;
            ReloadIndicator.fillAmount = reloadTimer;
        }
        else
        {
            reloadTimer = 0;
        }

        mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Shoot()
    {
        if(currentAmmo > 0 && reloading == false)
        {
            Vector2 direction = (mouseScreenPosition - (Vector2)firePoint.transform.position).normalized;
            firePoint.up = direction;
            animator.Play("PlayerRifleShooting");
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if(CollectWeapon.MachineGunEquipped)
            {
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(firePoint.up * rocketBulletForce, ForceMode2D.Impulse);
            }
            currentAmmo--;
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
        currentAmmo = maxAmmo;
        reloading = false;
        reloadingMachineGun = false;
    }

    IEnumerator ReloadingRocketLauncher()
    {
        animator.Play("PlayerRifleReload");
        yield return new WaitForSeconds(rocketLaucnherReloadSpeed);
        currentAmmo = maxAmmo;
        reloading = false;
        reloadingRocketLauncher = false;
    }
}
