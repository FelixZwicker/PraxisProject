using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : Attack
{
    public int damage;

    public override void Attacking()
    {
        gameObject.GetComponent<HandleExplosion>().CastSurrounding(damage);
        StartCoroutine(gameObject.GetComponent<HandleExplosion>().PlayExplosion());

        Destroy(gameObject);
    }
}
