using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : MonoBehaviour
{
    public LayerMask layerMask;
    public float radius;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayExplosion();
        CastPlayerStunBulletSurrounding();
        Destroy(gameObject);
    }

    private void PlayExplosion()
    {
        explosion.transform.position = transform.position;
        explosion.Play();
    }

    private void CastPlayerStunBulletSurrounding()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                col.GetComponent<StunEffect>().HandleStunEffect();
            }
        }
    }
}
