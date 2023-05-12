using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWeapon : MonoBehaviour
{
    private Shooting shootingScript;
    private ShopInteraction shopInteractionScript;
    private GameObject WeaponType;

    private float distanceToPlayer;

    private void Start()
    {
        if (gameObject.name == "ExplosionWeapon(Clone)")
        {
            WeaponType = Resources.Load("Prefab/ExplosionBullet") as GameObject;
        }
        else if(gameObject.name == "MashineGunWeapon(Clone)")
        {
            WeaponType = Resources.Load("Prefab/MashineGunBullet") as GameObject;
        }

        shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        shopInteractionScript = GameObject.Find("Shop").GetComponent<ShopInteraction>();
    }

    private void Update()
    {
        if(ShopInteraction.openedShop)
        {
            Destroy(gameObject);
        }

        distanceToPlayer = Vector2.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if(distanceToPlayer < 1.5f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(gameObject);
                shootingScript.bulletPrefab = WeaponType;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.transform.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            shootingScript.bulletPrefab = WeaponType;
        }*/
    }
}