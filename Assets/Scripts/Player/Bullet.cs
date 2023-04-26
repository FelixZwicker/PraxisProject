using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public int damage;

    protected abstract void HandleCollision();

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision();
    }


}
