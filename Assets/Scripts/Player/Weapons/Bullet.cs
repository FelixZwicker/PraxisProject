using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float damage;
    public float knockBackStrength;
    public float knockBackTime;

    protected abstract void HandleCollision();

    void OnCollisionEnter2D(Collision2D _collision)
    {
        HandleCollision();
        if(_collision.transform.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(_collision.transform.gameObject.GetComponent<EnemyHealth>().EnemyTakeDamage(damage));
            _collision.transform.GetComponent<KnockBack>().HandleKnockBack(transform,knockBackStrength, knockBackTime);
        }
    }
}
