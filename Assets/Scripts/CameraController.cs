using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    void Start()
    {

    }

    void Update()
    {
        Vector2 position = player.position;
        gameObject.transform.position = position;
    }
}
