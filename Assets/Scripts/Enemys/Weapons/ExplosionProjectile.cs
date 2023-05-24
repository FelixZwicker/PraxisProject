using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : MonoBehaviour
{
    public float speed;
    public int environmentalDamage;
    public int directDamage;

    private GameObject player;
    private Vector2 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + (speed * Time.deltaTime * direction));
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(gameObject.GetComponent<HandleExplosion>().PlayExplosion());
            StartCoroutine(player.GetComponent<PlayerController>().TakeDamage(directDamage));
            DestroyProjectile();
        }
        else if (_collision.gameObject.CompareTag("Enviroment"))
        {
            StartCoroutine(gameObject.GetComponent<HandleExplosion>().PlayExplosion());
            gameObject.GetComponent<HandleExplosion>().CastSurrounding(environmentalDamage);
            DestroyProjectile();

        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
