using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameSounds : MonoBehaviour
{
    private static GameSounds _i;

    public static GameSounds i
    {
        get
        {
            if(_i == null)
            {
                _i = (Instantiate(Resources.Load("GameSounds")) as GameObject).GetComponent<GameSounds>();
            }
            return _i;
        }
    }

    public AudioMixerGroup Mixer;
    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
