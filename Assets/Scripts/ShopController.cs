using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public WaveController waveControllerScript;

    public void ExitShop()
    {
        waveControllerScript.StartWave();
    }

    void SetShopItems()
    {

    }

    void GetSelectedItem()
    {

    }
}
