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
    public TrailRenderer tr;
    public GameObject[] obstacles;

    //gloabal eccessable variables
    public static int maxHealth = 10;
    public static float money = 0;
    public float moveSpeed = 0.5f;
    private float actualMoveSpeed;

    private int currentHealth;
    private Vector2 movement;
    private Vector2 mousePos;
    private bool hit = true;

    public float dashSpeed = 2f;

    private bool canDash;
    private float dashTime = .05f;
    private float dashCoolDown = 3f;

    void Start()
    {
        canDash = true;
        actualMoveSpeed = moveSpeed;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        healthBar.SetHealth(currentHealth, maxHealth);
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
        {
            StartCoroutine(Dash());
        }
        CheckHealth();
        DisplayMoney();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * actualMoveSpeed * Time.fixedDeltaTime);

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

    public IEnumerator TakeDamage()
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

    private IEnumerator Dash()
    {
        canDash = false;
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        actualMoveSpeed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        actualMoveSpeed = moveSpeed;
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        yield return new WaitForSeconds(dashCoolDown);
        Debug.Log("Finished");
        canDash = true;
    }
}
