using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Ranged_Positioning : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    private Transform player;
    public GameObject Enemy;
    private float startTimeBtwShots;
    public GameObject projectile;

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
        player = GameObject.Find("Player").transform;
        startTimeBtwShots = 2;
        framesToWait = 75;
        canMove = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(framesToWait);
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
                Debug.Log("move");
            }
        }
        else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
           
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            Debug.Log("back up");
            
        }
        else
        {
            startTimeBtwShots = 0.1f;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            canMove = false;
            framesToWait = 75;
            transform.position = this.transform.position;
            Debug.Log("stop");
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
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
}

