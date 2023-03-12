using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Ranged_Positioning : MonoBehaviour
{
    public float rotationModifier;
    public float speed;
    public Transform enemyFirepoint;
    public float stoppingDistance;
    public float retreatDistance;
    private Transform player;
    private Transform Enemy;
    private float startTimeBtwShots;
    public GameObject projectile;
    public int health;

    private float timeBtwShots;
    private Vector3 target;
    NavMeshAgent agent;
    private int framesToWait;
    private bool canMove;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        timeBtwShots = startTimeBtwShots;
    }

    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject.transform;
        player = GameObject.Find("Player").transform;
        startTimeBtwShots = 2;
        framesToWait = 75;
        canMove = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            startTimeBtwShots = 2;
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
            }
        }
        else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
           
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            
        }
        else
        {
            startTimeBtwShots = 0.1f;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            canMove = false;
            framesToWait = 75;
            transform.position = this.transform.position;
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, enemyFirepoint.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
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
            }
            else
            {
                Destroy(gameObject);
                PlayerController.money += 20;
            }
        }
    }
}

