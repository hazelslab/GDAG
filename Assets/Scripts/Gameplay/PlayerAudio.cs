using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private List<SoundData> _clips = new List<SoundData>();

    public void PlaySound(string soundName)
    {
        foreach (var clip in _clips)
        {
            if (soundName == clip._name)
            {
                _audioSource.clip = clip._sound;
                _audioSource.Play();
            }
        }
    }
}

[Serializable]
public class SoundData
{
    [SerializeField]
    public string _name;
    [SerializeField]
    public AudioClip _sound;
}