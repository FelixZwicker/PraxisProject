using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementE : MonoBehaviour
{
    public float stoppingDistance;
    public float rotationModifier;
    public Animator Animator;
    public Attack attack;
    public float timeBtwPunches;

    NavMeshAgent agent;
    private Transform player;
    private bool canMove;
    private int framesToWait;

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

    private void HandleMovement()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
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
                if(gameObject.tag == "Ranged")
                {
                    Animator.Play("EnemyRangedRunning");
                }
                else
                {
                    Animator.Play("EnemyCloseRunnging");
                }               
            }
        }
        else
        {
            attack.Attacking(.5f, true, timeBtwPunches);
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            canMove = false;
            framesToWait = 75;
            transform.position = this.transform.position;
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
}
