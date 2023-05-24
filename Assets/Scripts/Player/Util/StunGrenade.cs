using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : MonoBehaviour
{
    public LayerMask layerMask;
    public float radius;
    public GameObject stunGrenadeEffectPrefab;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(PlayStunExplosion());
        CastPlayerStunBulletSurrounding();
        Destroy(gameObject);
    }

    public IEnumerator PlayStunExplosion()
    {
        GameObject stunEffect = Instantiate(stunGrenadeEffectPrefab, transform.position, Quaternion.identity);
        Debug.Log(stunEffect.GetComponent<ParticleSystem>().main.duration);
        stunEffect.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(stunEffect.GetComponent<ParticleSystem>().main.duration);
        Destroy(stunEffect);
    }

    public void CastPlayerStunBulletSurrounding()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(col.GetComponent<StunEffect>().Stun());
            }
        }
    }
}
