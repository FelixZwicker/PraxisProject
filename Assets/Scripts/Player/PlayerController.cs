﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerDamageIndicator PlayerDamageIndicatorScript;
    public PauseMenu pauseScript;
    public HealthBar HealthBarScript;
    public Animator animator;

    public TextMeshProUGUI moneyDisplayGameUI;
    public GameObject GameOverUI;
    public GameObject InGameUI;
    public Image DashIndicator;

    public Rigidbody2D rb;
    public Camera cam;
    public TrailRenderer tr;

    //gloabal eccessable variables
    public static int maxHealth = 10;
    public static float money = 0;
    public float moveSpeed = 10f;
    public static float actualMoveSpeed;
    public static int score = 0;
    public static bool extraLife = false;

    private int currentHealth;
    private Vector2 movement;
    private Vector2 mousePos;

    public float dashSpeed;

    private bool canDash;
    public float dashTime;
    public float dashCoolDown;
    private float dashTimer = 0;

    void Start()
    {
        tr.emitting = false;
        canDash = true;
        actualMoveSpeed = moveSpeed;
        currentHealth = maxHealth;
        HealthBarScript.SetHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        HealthBarScript.SetHealth(currentHealth, maxHealth);
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
        {
            StartCoroutine(Dash());
        }

        //Health Kit
        if (Input.GetKeyDown(KeyCode.Space) && WaveController.canUseHeal)
        {
            WaveController.canUseHeal = false;
            currentHealth = maxHealth;
        }

        //Debug add money
        if (Input.GetKeyDown(KeyCode.M))
        {
            money += 100;
        }

        //Desh Indicator
        if(!canDash)
        {
            dashTimer += 1 / dashCoolDown * Time.deltaTime;
            DashIndicator.fillAmount = dashTimer;
        }
        else
        {
            dashTimer = 0;
        }

        CheckHealth();
        DisplayMoney();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * actualMoveSpeed * Time.fixedDeltaTime);
        //SoundManager.PlaySounds(SoundManager.Sound.WalkingSound);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
    }

    void CheckHealth()
    {
        if (currentHealth <= 0 && extraLife == false)
        {
            pauseScript.FreezeGame();
            GameOverUI.SetActive(true);
            InGameUI.SetActive(false);
        }
        else if(currentHealth <= 0 && extraLife == true)
        {
            currentHealth = maxHealth;
            extraLife = false;
            //Wiederbelebungs indikator
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<KnockBack>().HandleKnockBack(transform, 400, .2f);
        }
    }

    public IEnumerator TakeDamage(int Damage)
    {
        currentHealth -= Damage;
        PlayerDamageIndicatorScript.TakenDamage();
        yield return new WaitForSeconds(0.5f);
    }

    void DisplayMoney()
    {
        moneyDisplayGameUI.text = money.ToString();
    }

    private IEnumerator Dash()
    {
        canDash = false;
        SoundManager.PlaySounds(SoundManager.Sound.DashSound);
        actualMoveSpeed = dashSpeed;
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        actualMoveSpeed = moveSpeed;
        tr.emitting = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
}
