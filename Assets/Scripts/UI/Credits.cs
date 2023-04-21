using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public Animator CreditsAnimator;
    public AnimationClip CreditsClip;

    private void Start()
    {
        CreditsAnimator.Play("Credits");
        StartCoroutine(WaitForCredits());
    }

    IEnumerator WaitForCredits()
    {
        yield return new WaitForSeconds(CreditsClip.length);
        SceneManager.LoadScene("StartMenu");

    }
}
