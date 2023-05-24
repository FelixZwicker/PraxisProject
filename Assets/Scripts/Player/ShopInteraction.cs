using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public WaveController waveControllerScript;
    public GameObject interactionIndicator;
    public GameObject player;
    public LineRenderer line;

    public bool openedShop = false;
    
    private float distanceToPlayer;
    private bool showLine = false;

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);

        //checks if shop can be opened
        if (distanceToPlayer < 3 && !openedShop && waveControllerScript.finishedWave)
        {
            interactionIndicator.SetActive(true);
            OpenShop();
        }
        else
        {
            interactionIndicator.SetActive(false);
        }

        if(waveControllerScript.finishedWave && !openedShop)
        {
            showLine = true;
        }
        else
        {
            showLine = false;
        }

        //linerender to help player find the shop on round end
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
            interactionIndicator.SetActive(false);
            waveControllerScript.WaveOver();
        }
    }
}
