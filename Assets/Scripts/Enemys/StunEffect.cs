using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        yield return new WaitForSeconds(stunTime);
        gameObject.GetComponent<MovementE>().enabled = true;
    }
}
