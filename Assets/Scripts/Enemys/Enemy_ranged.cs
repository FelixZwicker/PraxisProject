using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_ranged : MonoBehaviour
{
    public float moveSpeed;
    public int health;
    public float range;
    public GameObject Enemy;

    NavMeshAgent agent;
    private int framesToWait;
    private bool canMove;
    private float time;
    private Transform player;
    private Transform enemy;
    private Rigidbody2D rb;
    private Vector2 movement;
    
    

    void Start()
    {
        framesToWait = 75;
        canMove = false;
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        Vector3 direction = player.position - transform.position;
        float ab = (Mathf.Abs(direction.x) * Mathf.Abs(direction.x)) + (Mathf.Abs(direction.y) * Mathf.Abs(direction.y));
        double c = Mathf.Sqrt(ab);

        Debug.Log(Enemy.transform.position);

        if (c > range)
        { 
            if (canMove == false)
            {
                framesToWait--;
                if (framesToWait == 0)
                    canMove = true;
            }
            else
            {
                moveCharacter();
                Debug.Log("move");
            }
        }
        else
        {
            canMove = false;
            framesToWait = 75;
        }
    }

    

    void moveCharacter()
    {
        agent.SetDestination(new Vector3(player.position.x, player.position.y, 0));
    }
   
}
