using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack
{
    private float timeBtwShots;
    public Transform enemyFirepoint;
    public GameObject projectile;
    public Animator Animator;
    public float fireRate;

    public override void Attacking()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, enemyFirepoint.position, Quaternion.identity);
            timeBtwShots = fireRate;
            Animator.Play("EnemyRangedShooting");
            SoundManager.PlaySounds(SoundManager.Sound.ShootingSound);
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            Animator.Play("EnemyRangedRunning");
        }
    }
}
