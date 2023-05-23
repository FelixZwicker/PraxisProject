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

    public bool wasPlayed = false;

    private GameObject[] enemiesToCount;

    private void Update()
    {
        if (waveControllerScript.currentWaveDuration < 0 && !wasPlayed)
        {
            enemiesToCount = GameObject.FindGameObjectsWithTag("Enemy");
            PopUpText.text = enemiesToCount.Length.ToString();
            wasPlayed = true;
            StartCoroutine(PlayAnimation());
        }
    }

    IEnumerator PlayAnimation()
    {
        PopUpWindow.SetActive(true);
        animator.Play("PopUp");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        PopUpWindow.SetActive(false);
    }
}
