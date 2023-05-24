using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABomb : MonoBehaviour
{
    public float radius;
    public LayerMask layerMask;
    public float damage;
    public float knockBackStrength;
    public float knockBackTime;
    public GameObject explosionPrefab;

    private void CastABombSurrounding()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(col.GetComponent<EnemyHealth>().EnemyTakeDamage(damage));
            }
            else if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log(col.gameObject.name);
                col.GetComponent<KnockBack>().HandleKnockBack(transform, knockBackStrength, knockBackTime);
            }
        }
    }

    public void Detonate()
    {
        StartCoroutine(PlayExplosion());
        CastABombSurrounding();
        Destroy(gameObject);
    }

    private IEnumerator PlayExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Debug.Log(explosion.GetComponent<ParticleSystem>().main.duration);
        explosion.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(explosion.GetComponent<ParticleSystem>().main.duration);
        Destroy(explosion);
    }
}
