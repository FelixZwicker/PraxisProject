using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageIndicator : MonoBehaviour
{
    public Image bloodSplatter;

    private float splatterCooldown = 5.2f; // how fast alpha reaches 0 again

    void Update()
    {
        //permanently decreases alpha of BloodSplatter image
        Color splatterAlpha = bloodSplatter.color;
        if(bloodSplatter.color.a > 0)
        {
            splatterAlpha.a -= 1 / splatterCooldown * Time.deltaTime;
            bloodSplatter.color = splatterAlpha;
        }
    }

    public void TakenDamage()
    {
        //increases alpha of BloodSplatter image on damage impact
        Color splatterAlpha = bloodSplatter.color;
        if(splatterAlpha.a <= 1)
        {
            splatterAlpha.a += 0.2f;
        }
        bloodSplatter.color = splatterAlpha;
    }
}
