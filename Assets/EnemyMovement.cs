using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Movement Movement;
    
    void Update()
    {
        Movement.Move(this);   
    }
}
