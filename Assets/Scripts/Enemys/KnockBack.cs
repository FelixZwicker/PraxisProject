using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float knockBackStrength;
    public float knockBackTime;
    
    public void HandleKnockBack(Transform bulletPos)
    {
        StartCoroutine(Push(bulletPos));
    }

    private IEnumerator Push(Transform bulletPos)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector2 dir = bulletPos.position - transform.position;
        gameObject.GetComponent<Rigidbody2D>().AddForce(-dir.normalized * knockBackStrength, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockBackTime);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
