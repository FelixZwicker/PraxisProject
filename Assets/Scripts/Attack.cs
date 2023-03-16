using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Attacking(float fireRate, bool canPunch, float punchRate);
}
