using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 direction;

    public float speed;
    private GameObject player;
    private Vector2 Target;
    private Rigidbody2D rb;

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
        gameObject.GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + (5f * Time.deltaTime * direction));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DestroyProjectile();  
            Debug.Log("hit");
            StartCoroutine(player.GetComponent<PlayerController>().TakeDamage());
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
