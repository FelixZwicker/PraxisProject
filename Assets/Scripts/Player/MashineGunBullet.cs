using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashineGunBullet : Bullet
{
    protected override void HandleCollision()
    {
        Destroy(gameObject);
    }
}
