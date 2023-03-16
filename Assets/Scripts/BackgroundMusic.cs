using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip MusicOne;
    public AudioClip MusicTwo;
    public AudioClip MusicThree;
    public AudioClip MusicFour;
    public AudioClip MusicFive;
    public AudioClip MusicSix;

    private AudioSource audio;
    private int musicCounter = 0;
    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        selectAudio();
    }

    private void selectAudio()
    {
        AudioClip clip;
        switch (musicCounter)
        {
            case 0:
                clip = MusicOne;
                break;
            case 1:
                clip = MusicTwo;
                break;
            case 2:
                clip = MusicThree;
                break;
            case 3:
                clip = MusicFour;
                break;
            case 4:
                clip = MusicFive;
                break;
            case 5:
                clip = MusicSix;
                break;
            default:
                musicCounter = 0;
                clip = MusicOne;
                break;
        }

        StartCoroutine(playBackgroundMusic(clip));
    }

    IEnumerator playBackgroundMusic(AudioClip clip)
    {
        audio.PlayOneShot(clip);
        Debug.Log(clip.length + " in s");
        yield return new WaitForSeconds(clip.length);
        audio.Stop();
        musicCounter++;
        selectAudio();
    }
}
