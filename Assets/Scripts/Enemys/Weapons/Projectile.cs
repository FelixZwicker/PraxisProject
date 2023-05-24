using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private GameObject player;

    private Vector2 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + (speed * Time.deltaTime * direction));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DestroyProjectile();
            StartCoroutine(player.GetComponent<PlayerController>().TakeDamage(1));
        }
        else if (collision.CompareTag("Enviroment"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
