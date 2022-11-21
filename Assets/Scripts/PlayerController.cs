using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public Rigidbody2D rb;
    public Camera cam;
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;

    private Vector2 movement;
    private Vector2 mousePos;
    private bool hit = true;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        CheckHealth();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
    }

    void CheckHealth()
    {
        if(currentHealth == 0)
        {
            //Deaths
            Debug.Log("You Died");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.tag == "Enemy")
        {
            if (hit)
            {
                StartCoroutine(TageDamage());
            }
        }
    }

    IEnumerator TageDamage()
    {
        hit = false;
        currentHealth--;
        healthBar.SetHealth(currentHealth);
        yield return new WaitForSeconds(0.5f);
        hit = true;
    }
}
