using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public WaveController waveControllerScript;
    public GameObject InteractionIndicator;

    public static bool openedShop = false;

    private float distanceToPlayer;

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (distanceToPlayer < 3 && !openedShop && waveControllerScript.finishedWave)
        {
            InteractionIndicator.SetActive(true);
            OpenShop();
        }
        else
        {
            InteractionIndicator.SetActive(false);
        }
    }

    void OpenShop()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            openedShop = true;
            InteractionIndicator.SetActive(false);
            waveControllerScript.WaveOver();
        }
    }
}
