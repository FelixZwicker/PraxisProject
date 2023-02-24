using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    public HealthBar healthBar;
    public TextMeshProUGUI moneyDisplayGameUI;

    //gloabal eccessable variables
    public static int maxHealth = 10;
    public static float money = 0;
    public float moveSpeed = 0.5f;

    private int currentHealth;
    private Vector2 movement;
    private Vector2 mousePos;
    private bool hit = true;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        healthBar.SetHealth(currentHealth, maxHealth);

        CheckHealth();
        DisplayMoney();
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
            //Death
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.tag == "Enemy")
        {
            if(hit)
            {
                StartCoroutine(TakeDamage());
            }
        }
    }

    IEnumerator TakeDamage()
    {
        hit = false;
        currentHealth--;
        yield return new WaitForSeconds(0.5f);
        hit = true;
    }

    void DisplayMoney()
    {
        moneyDisplayGameUI.text = money.ToString() + " €";
    }
}
