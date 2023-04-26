using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleExplosion : MonoBehaviour
{
    public LayerMask layerMask;
    public float radius;

    private ParticleSystem explosion;
    private GameObject player;

    void Start()
    {
        explosion = GameObject.Find("ExplosionEnemy").GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayExplosion()
    {
        explosion.transform.position = transform.position;
        explosion.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void CastSurrounding(int damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        foreach (Collider2D col in colliders)
        {
            if (col.name == "Player")
            {
                StartCoroutine(player.GetComponent<PlayerController>().TakeDamage(damage));
            }
        }
    }
}
