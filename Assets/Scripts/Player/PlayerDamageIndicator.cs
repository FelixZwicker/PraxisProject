using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageIndicator : MonoBehaviour
{
    public Image BloodSplatter;
    public float SplatterCooldown;

    void Update()
    {
        Color splatterAlpha = BloodSplatter.color;
        if(BloodSplatter.color.a > 0)
        splatterAlpha.a -= 1 / SplatterCooldown * Time.deltaTime;
        BloodSplatter.color = splatterAlpha;
    }

    public void TakenDamage()
    {
        Color splatterAlpha = BloodSplatter.color;
        if(splatterAlpha.a <= 1)
        {
            splatterAlpha.a += 0.2f;
        }
        BloodSplatter.color = splatterAlpha;
    }
}
