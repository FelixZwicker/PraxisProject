using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : Attack
{
    public Animator animator;
    public float damageDelay;

    private GameObject player;
    private bool canAttack;

    void Start()
    {
        canAttack = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Attacking()
    {
        if (canAttack)
            StartCoroutine(Slash());
    }

    IEnumerator Slash()
    {
        canAttack = false;
        animator.Play("EnemyCloseSlashing");
        SoundManager.PlaySounds(SoundManager.Sound.SlashingSound);
        yield return new WaitForSeconds(damageDelay);
        Debug.Log(Vector2.Distance(transform.position, player.transform.position));
        if(Vector2.Distance(transform.position, player.transform.position) < 3)
            StartCoroutine(player.GetComponent<PlayerController>().TakeDamage(1));
        canAttack = true;
    }
}
