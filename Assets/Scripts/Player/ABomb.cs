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

    private ParticleSystem explosion;

    private void CastABombSurrounding()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(col.GetComponent<Enemy_Health>().EnemyTakeDamage(damage));
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
        PlayExplosion();
        CastABombSurrounding();
        Destroy(gameObject);
    }

    private void PlayExplosion()
    {
        explosion = GameObject.Find("ExplosionABomb").GetComponent<ParticleSystem>();
        explosion.transform.position = transform.position;
        explosion.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
