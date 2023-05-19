using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeHandler : MonoBehaviour
{
    public GameObject grenadePrefab;
    public GameObject aBombPrefab;
    public Transform firePoint;

    public int maxGrenades = 5;
    public float grenadeForce;
    public float airTime;
    public float grenadeCoolDown;
    public float detonationTime;
    public int currentGrenades = 0;
    public bool aBombisCurrentlyEquipped = false;

    private bool canThrow;
    

    // Start is called before the first frame update
    void Start()
    {
        canThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(ThrowGrenade());
        }

        if (Input.GetKeyDown(KeyCode.X) &&  aBombisCurrentlyEquipped)
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
