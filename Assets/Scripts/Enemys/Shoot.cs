using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack
{
    private bool canShoot;
    private float timeBtwShots;
    public Transform enemyFirepoint;
    public GameObject projectile;
    public Animator Animator;
    public float fireRate;

    private void Start()
    {
        canShoot = true;
    }

    public override void Attacking()
    {
        if (canShoot)
            StartCoroutine(Fire());
        /*
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
        */
    }

    IEnumerator Fire()
    {
        canShoot = false;
        Instantiate(projectile, enemyFirepoint.position, Quaternion.identity);
        Animator.Play("EnemyRangedShooting");
        SoundManager.PlaySounds(SoundManager.Sound.ShootingSound);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
