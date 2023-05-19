using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeHandler : MonoBehaviour
{
    public static int maxGrenades = 5;
    public static bool aBombisEquipped = true;

    public GameObject grenadePrefab;
    public GameObject aBombPrefab;
    public Transform firePoint;

    public float grenadeForce;
    public float airTime;
    public float grenadeCoolDown;
    public float detonationTime;

    private int currentGrenades;
    private bool aBombisCurrentlyEquipped;
    private bool canThrow;
    

    // Start is called before the first frame update
    void Start()
    {
        canThrow = true;
        aBombisCurrentlyEquipped = aBombisEquipped;
        currentGrenades = maxGrenades;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(ThrowGrenade());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Ignate());
        }
    }

    IEnumerator ThrowGrenade()
    {
        if(currentGrenades > 0 && canThrow)
        {
            canThrow = false;
            GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * grenadeForce, ForceMode2D.Impulse);
            currentGrenades--;
            yield return new WaitForSeconds(airTime);
            if(grenade != null)
            {
                rb.velocity = Vector2.zero;
                grenade.GetComponent<StunGrenade>().CastPlayerStunBulletSurrounding();
                grenade.GetComponent<StunGrenade>().PlayExplosion();
                Destroy(grenade);
            }
            yield return new WaitForSeconds(grenadeCoolDown);
            canThrow = true;
        }
    }

    IEnumerator Ignate()
    {
        GameObject aBomb = Instantiate(aBombPrefab, gameObject.transform);
        aBombisCurrentlyEquipped = false;
        yield return new WaitForSeconds(detonationTime);
        aBomb.GetComponent<ABomb>().Detonate();
    }
}
