using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : MonoBehaviour
{
    public float speed;
    public int environmentalDamage;
    public int directDamage;

    private GameObject player;
    private Vector2 Target;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Target = new Vector2(player.transform.position.x, player.transform.position.y);
        direction = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + (speed * Time.deltaTime * direction));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<HandleExplosion>().PlayExplosion();
            StartCoroutine(player.GetComponent<PlayerController>().TakeDamage(directDamage));
            DestroyProjectile();
        }
        else if (collision.gameObject.CompareTag("Enviroment"))
        {
            gameObject.GetComponent<HandleExplosion>().PlayExplosion();
            gameObject.GetComponent<HandleExplosion>().CastSurrounding(environmentalDamage);
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
