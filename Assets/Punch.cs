using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : Attack
{
    private float timeBtwPunchses;
    public Animator Animator;
    public HitBox HitBox;
    private static float punchRate = 10f;
    private bool canPunchEnum;
    private GameObject player;
    public Collider2D HitBoxCol;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Attacking(float fireRate, bool canPunch, float punchRate)
    {
        if (timeBtwPunchses <= 0)
        {
            Debug.Log("punch!");
            Animator.Play("EnemyCloseSlashing");
            timeBtwPunchses = punchRate;
            StartCoroutine(player.GetComponent<PlayerController>().TakeDamage());
            CheckHitBox(HitBoxCol);
        }
        else
        {
            Debug.Log("i cant!");
            timeBtwPunchses -= Time.deltaTime;
        }
    }

    private void CheckHitBox(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("hit");
            StartCoroutine(player.GetComponent<PlayerController>().TakeDamage());
        }
    }

}
