using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.AI;

public class StunEffect : MonoBehaviour
{
    public float stunTime;

    public void HandleStunEffect()
    {
        StartCoroutine(Stun());
    }

    private IEnumerator Stun()
    {
        gameObject.GetComponent<MovementE>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        Color baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1, 1);
        yield return new WaitForSeconds(stunTime);
        gameObject.GetComponent<MovementE>().enabled = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = baseColor;
    }
}
