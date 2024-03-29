using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGameObjects : MonoBehaviour
{
    private bool currentlyDestroying = false;

    private void Update()
    {
        if(!currentlyDestroying)
        {
            currentlyDestroying = true;
            StartCoroutine(DestroySoundGameObjects());
        }
    }

    //destroyes all sound objects in scene after 2min.
    IEnumerator DestroySoundGameObjects()
    {
        yield return new WaitForSeconds(120f);
        GameObject[] instaniatedSounds = GameObject.FindGameObjectsWithTag("Sound");
        foreach (GameObject sound in instaniatedSounds)
        {
            Destroy(sound);
        }
        currentlyDestroying = false;
    }
}
