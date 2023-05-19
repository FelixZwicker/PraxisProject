using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public void HandleKnockBack(Transform bulletPos, float knockBackStrength, float knockBackTime)
    {
        
        StartCoroutine(Push(bulletPos, knockBackStrength, knockBackTime));
    }

    private IEnumerator Push(Transform bulletPos, float knockBackStrength, float knockBackTime)
    {
        if(gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.enabled = false;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector2 dir = bulletPos.position - transform.position;
        gameObject.GetComponent<Rigidbody2D>().AddForce(-dir.normalized * knockBackStrength, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockBackTime);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(playerController != null)
            playerController.enabled = true;
    }
}
