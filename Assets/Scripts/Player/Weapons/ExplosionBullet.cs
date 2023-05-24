using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public LayerMask layerMask;
    public float radius;
    public float enviromentalDamage;

    public GameObject explosionPrefab;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected override void HandleCollision()
    {
        StartCoroutine(PlayExplosion());
        CastPlayerBulletSurrounding(enviromentalDamage);
        Destroy(gameObject);
    }

    IEnumerator PlayExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Debug.Log(explosion.GetComponent<ParticleSystem>().main.duration);
        explosion.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(explosion.GetComponent<ParticleSystem>().main.duration);
        Destroy(explosion);
    }

    public void CastPlayerBulletSurrounding(float _damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(col.GetComponent<EnemyHealth>().EnemyTakeDamage(_damage));

                col.gameObject.GetComponent<KnockBack>().HandleKnockBack(transform, knockBackStrength, knockBackTime);
            }
        }
    }

   
}
