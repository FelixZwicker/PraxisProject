using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack
{
    public Transform enemyFirepoint;
    public GameObject projectile;
    public Animator animator;
    public float fireRate;

    private bool canShoot;

    private void Start()
    {
        canShoot = true;
    }

    public override void Attacking()
    {
        if (canShoot)
            StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        canShoot = false;
        Instantiate(projectile, enemyFirepoint.position, Quaternion.identity);
        animator.Play("EnemyRangedShooting");
        SoundManager.PlaySounds(SoundManager.Sound.ShootingSound);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
