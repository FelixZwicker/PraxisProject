using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainingEnemies : MonoBehaviour
{
    public WaveController waveControllerScript;
    public Animator animator;
    public GameObject PopUpWindow;
    public TextMeshProUGUI PopUpText;

    private GameObject[] enemiesToCount;

    private float popUpCooldown = 5f;
    private bool startCounter = false;

    private void Update()
    {
        if(waveControllerScript.currentWaveDuration < 5 && !waveControllerScript.finishedWave && !startCounter)
        {
            startCounter = true;
            StartCoroutine(PopUpMessage());
        }

        enemiesToCount = GameObject.FindGameObjectsWithTag("Enemy");
        PopUpText.text = enemiesToCount.Length.ToString();
    }

    IEnumerator PlayAnimation()
    {
        Debug.Log("Here");
        PopUpWindow.SetActive(true);
        animator.Play("PopUp");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        PopUpWindow.SetActive(false);
    }

    IEnumerator PopUpMessage()
    {
        StartCoroutine(PlayAnimation());
        yield return new WaitForSeconds(popUpCooldown);
        if(enemiesToCount.Length != 0)
        {
            StartCoroutine(PopUpMessage());
        }
        else
        {
            startCounter = false;
        }
    }
}
