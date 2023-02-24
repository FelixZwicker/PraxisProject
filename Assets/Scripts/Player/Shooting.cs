using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public TextMeshProUGUI ammoDisplay;

    public static float bulletForce = 4.5f;
    public static int maxAmmo = 25;

    private int currentAmmo;
    private bool reloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        ammoDisplay.SetText(currentAmmo.ToString() + " / " + maxAmmo.ToString());
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        ammoDisplay.SetText(currentAmmo.ToString() + " / " + maxAmmo.ToString());
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
        yield return new WaitForSeconds(2);
        currentAmmo = maxAmmo;
        reloading = false;
    }
}
