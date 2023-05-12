using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float damage;

    protected abstract void HandleCollision();

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision();
        if(collision.transform.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(collision.transform.gameObject.GetComponent<Enemy_Health>().EnemyTakeDamage(damage));
            collision.transform.GetComponent<KnockBack>().HandleKnockBack(transform);
        }
    }


}
