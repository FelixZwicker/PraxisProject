using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public TextMeshProUGUI ammoDisplay;

    public float bulletForce = 20f;

    public int maxAmmo = 25;
    public int currentAmmo;

    private bool reloding = false;

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
            Relode();
        }

        ammoDisplay.SetText(currentAmmo.ToString() + " / " + maxAmmo.ToString());
    }

    void Shoot()
    {
        if(currentAmmo > 0 && reloding == false)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            currentAmmo--;
        }
    }

    void Relode()
    {
        if(reloding == false)
        {
            reloding = true;
            StartCoroutine(Reloding());
        }
    }

    IEnumerator Reloding()
    {
        yield return new WaitForSeconds(2);
        currentAmmo = maxAmmo;
        reloding = false;
    }
}
