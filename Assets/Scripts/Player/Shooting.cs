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

    public float bulletForce = 20f;
    public int maxAmmo = 25;
    public float reloadSpeed = 5f;
    public bool canShoot = true;
    public int currentAmmo;

    private float reloadTimer;
    private bool reloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        reloadTimer = 0;
        ammoDisplay.SetText(currentAmmo.ToString() + " / " + maxAmmo.ToString());
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.R) && canShoot)
        {
            Reload();
        }

        ammoDisplay.SetText(currentAmmo.ToString() + " / " + maxAmmo.ToString());

        if(reloading)
        {
            reloadTimer += 1 / reloadSpeed * Time.deltaTime;
            ReloadIndicator.fillAmount = reloadTimer;
        }
        else
        {
            reloadTimer = 0;
        }
    }

    void Shoot()
    {
        if(currentAmmo > 0 && reloading == false)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            currentAmmo--;
        }
    }

    void Reload()
    {
        if(reloading == false)
        {
            reloading = true;
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = maxAmmo;
        reloading = false;
    }
}
