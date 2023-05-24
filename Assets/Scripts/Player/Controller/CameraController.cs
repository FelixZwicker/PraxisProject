using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        //adjusts camera position to player position
        Vector3 position = new Vector3(player.position.x, player.position.y, -10);
        gameObject.transform.position = position;
    }
}
