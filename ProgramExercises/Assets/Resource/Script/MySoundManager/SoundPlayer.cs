using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] SoundSetting soundSetting;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int clipindex)
    {
        if (soundSetting != null && clipindex >= 0 && clipindex < soundSetting.audioClips.Count)
        {
            AudioClip clip = soundSetting.audioClips[clipindex];
            float volume = soundSetting.volumes[clipindex];

            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid AudioClip index or SoundSettings not assigned.");
        }
    }
}
