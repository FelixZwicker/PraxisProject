using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    private float timeBtwShots;
    public GameObject projectile; 
    public Transform enemyFirepoint;
    public float fireRate;
    public Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, enemyFirepoint.position, Quaternion.identity);
            timeBtwShots = fireRate;
            Animator.Play("EnemyRangedShooting");
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            Animator.Play("EnemyRangedRunning");
        }
    }
}
