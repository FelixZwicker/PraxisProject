using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet hit effects 

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
