using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Ranged_Positioning : MonoBehaviour
{
    public float rotationModifier;
    public float speed;
    
    public float stoppingDistance;
    private Transform player;
    
    
    public int health;
    public Animator Animator;
    private Vector3 target;
    NavMeshAgent agent;
    private int framesToWait;
    private bool canMove;
    private bool shoot;

    private bool ranged;

    public float BaseFireRate;
    public float HighFireRate;
    public Transform enemyFirepoint;
    private float timeBtwShots;
    public GameObject projectile;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        player = GameObject.Find("Player").transform;
        framesToWait = 75;
        canMove = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsPlayer();

        HandleMovement();
    }

    private void Shoot(float fireRate)
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

    private void HandleMovement()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            Shoot(HighFireRate);
            if (canMove == false)
            {
                framesToWait--;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                if (framesToWait == 0)
                    canMove = true;
            }
            else
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                MoveTowardsPlayer(player.position.x, player.position.y);
                Animator.Play("EnemyRangedRunning");
            }
        }
        else
        {
            Shoot(BaseFireRate);
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            canMove = false;
            framesToWait = 75;
            transform.position = this.transform.position;
            Animator.Play("EnemyRangedRunning");
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, new Vector3(0, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
    }

    void MoveTowardsPlayer(float positionX, float positionY)
    {
        agent.SetDestination(new Vector3(positionX, positionY, 0));
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.transform.gameObject.tag == "Bullet")
        {
            if (health > 1)
            {
                health--;
                Animator.Play("EnemyRangedHit");
            }
            else
            {
                Destroy(gameObject);
                PlayerController.money += 20;
            }
        }
    }
}

