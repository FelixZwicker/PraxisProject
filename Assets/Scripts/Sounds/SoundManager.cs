using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        ShootingSound,
        WalkingSound,
        SlashingSound,
        UIClickSound,
        DashSound,
    }

    //can be used in every script to play a sound predefined by the sound enum
    public static void PlaySounds(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        soundGameObject.tag = "Sound";
        audioSource.PlayOneShot(GetAudioClip(sound));
        audioSource.outputAudioMixerGroup = GameSounds.i.Mixer;
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameSounds.SoundAudioClip soundAudioClip in GameSounds.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}