using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public Animator creditsAnimator;
    public AnimationClip creditsClip;

    private void Start()
    {
        creditsAnimator.Play("Credits");
        StartCoroutine(WaitForCredits());
    }

    IEnumerator WaitForCredits()
    {
        yield return new WaitForSeconds(creditsClip.length);
        SceneManager.LoadScene("StartMenu");

    }
}
