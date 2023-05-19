using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public LayerMask layerMask;
    public float radius;
    public float enviromentalDamage;

    private ParticleSystem explosion;

    void Start()
    {
        explosion = GameObject.Find("ExplosionPlayerBullet").GetComponent<ParticleSystem>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected override void HandleCollision()
    {
        PlayExplosion();
        CastPlayerBulletSurrounding(enviromentalDamage);
        Destroy(gameObject);
    }

    private void PlayExplosion()
    {
        explosion.transform.position = transform.position;
        explosion.Play();
    }

    public void CastPlayerBulletSurrounding(float damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(col.GetComponent<Enemy_Health>().EnemyTakeDamage(damage));

                col.gameObject.GetComponent<KnockBack>().HandleKnockBack(transform, knockBackStrength, knockBackTime);
            }
        }
    }

   
}
