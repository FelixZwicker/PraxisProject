using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HitBox : MonoBehaviour
{ 
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision, bool hit)
    {
        if (collision.CompareTag("Player") && hit == true)
        {
            Debug.Log("hit");
            StartCoroutine(player.GetComponent<PlayerController>().TakeDamage());
        }
    }
}
