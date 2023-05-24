using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GrenadeHandler : MonoBehaviour
{
    public GameObject grenadePrefab;
    public GameObject aBombPrefab;
    public Image aBombUI;
    public Transform firePoint;
    public TextMeshProUGUI grenadeCounter;
    public TextMeshProUGUI grenadeCounterShopUI;

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
            Color aBombAlpha = aBombUI.color;
            aBombAlpha.a = 0.1f;
            aBombUI.color = aBombAlpha;
            StartCoroutine(Ignate());
        }

        grenadeCounter.text = currentGrenades.ToString();
        grenadeCounterShopUI.text = currentGrenades.ToString();
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
                StartCoroutine(grenade.GetComponent<StunGrenade>().PlayStunExplosion());
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
