using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerDamageIndicator PlayerDamageIndicatorScript;
    public ShopController shopControllerScript;
    public PauseMenu pauseScript;
    public HealthBar HealthBarScript;
    public Animator animator;

    public TextMeshProUGUI moneyDisplayGameUI;
    public GameObject GameOverUI;
    public GameObject leaderBoardUI;
    public GameObject restartUI;
    public GameObject InGameUI;
    public Image DashIndicator;

    public Rigidbody2D rb;
    public Camera cam;
    public TrailRenderer tr;

    public Texture2D cursorTexture;
    public TextMeshProUGUI inGameScore;
    public TextMeshProUGUI leaderBoardScore;

    //gloabal eccessable variables
    public int maxHealth = 10;
    public int currentHealth;
    public float money = 0;
    public float moveSpeed = 10f;
    public float actualMoveSpeed;
    public int score = 0;
    public bool extraLife = false;
    public bool canUseHeal = false;

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

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
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
        if (Input.GetKeyDown(KeyCode.Space) && canUseHeal && currentHealth < maxHealth / 2)
        {
            canUseHeal = false;
            currentHealth = maxHealth / 2;
            shopControllerScript.healEquipped = false;
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
        UpdateScore();
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
            GameObject extraLifeIndicator = GameObject.Find("ExtraLifeUI");
            extraLifeIndicator.SetActive(false);
        }
    }

    public void SwitchToGameOverUI()
    {
        leaderBoardUI.SetActive(false);
        restartUI.SetActive(true);
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

    void UpdateScore()
    {
        inGameScore.text = score.ToString();
        leaderBoardScore.text = score.ToString();
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
