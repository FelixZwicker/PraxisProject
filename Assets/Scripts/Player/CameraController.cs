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
        Vector3 position = new Vector3(player.position.x, player.position.y, -10);
        gameObject.transform.position = position;
    }
}
