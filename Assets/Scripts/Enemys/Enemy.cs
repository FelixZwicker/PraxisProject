using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public int health = 2;

    private Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.transform.gameObject.tag == "Bullet")
        {
            if(health > 1)
            {
                health--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if(collider.transform.gameObject.tag == "Player")
        {

        }
    }
}
