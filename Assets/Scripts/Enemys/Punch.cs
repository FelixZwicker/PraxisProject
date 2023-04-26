using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : Attack
{
    public Animator Animator;
    public float punchRate;

    private float timeBtwPunchses;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Attacking()
    {
        if (timeBtwPunchses <= 0)
        {
            Animator.Play("EnemyCloseSlashing");
            timeBtwPunchses = punchRate;
            StartCoroutine(player.GetComponent<PlayerController>().TakeDamage(1));
            SoundManager.PlaySounds(SoundManager.Sound.SlashingSound);
        }
        else
        {
            timeBtwPunchses -= Time.deltaTime;
        }
    }
}
