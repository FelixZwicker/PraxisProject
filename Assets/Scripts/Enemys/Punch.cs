using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : Attack
{
    private float timeBtwPunchses;
    public Animator Animator;
    private GameObject player;


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
            SoundManager.PlaySounds(SoundManager.Sound.SlashingSound);
        }
        else
        {
            Debug.Log("i cant!");
            timeBtwPunchses -= Time.deltaTime;
        }
    }
}
