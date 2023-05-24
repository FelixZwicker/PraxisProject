using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip[] musicArry;

    private AudioSource audio;
    private int musicCounter = 0;

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        selectAudio();
    }

    private void selectAudio()
    {
        //playes through all sound in array
        if(musicCounter == musicArry.Length)
        {
            musicCounter = 0;
        }
        StartCoroutine(playBackgroundMusic(musicArry[musicCounter]));
    }

    IEnumerator playBackgroundMusic(AudioClip clip)
    {
        audio.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        audio.Stop();
        musicCounter++;
        selectAudio();
    }
}
