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

    public static void PlaySounds(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
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