using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public WaveController waveControllerScript;
    public GameObject InteractionIndicator;
    public LineRenderer line;

    public static bool openedShop = false;
    
    private float distanceToPlayer;
    private bool showLine = false;

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

        if(waveControllerScript.finishedWave && !openedShop)
        {
            showLine = true;
        }
        else
        {
            showLine = false;
        }

        if(showLine)
        {
            line.enabled = true;
            line.SetPosition(0, GameObject.FindGameObjectWithTag("Player").transform.position); // Positions des Spielercharakters
            line.SetPosition(1, transform.position); // Postion des Ziels
        }
        else
        {
            line.enabled = false;
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
