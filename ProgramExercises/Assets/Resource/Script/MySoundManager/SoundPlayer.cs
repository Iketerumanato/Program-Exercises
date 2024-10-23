using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] SoundSetting soundSetting;
    [SerializeField] AudioSource audioSource;

    public void PlaySound(int clipNum)
    {
        if (soundSetting != null && clipNum < soundSetting.audioClips.Count)
        {
            AudioClip clip = soundSetting.audioClips[clipNum];
            float volume = soundSetting.volumes[clipNum];
            float pitch = soundSetting.pitchs[clipNum];

            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.Play();
        }
        else Debug.LogWarning("Invalid AudioClip index or SoundSettings not assigned.");
    }

    public string GetSoundName(int clipNum)
    {
        if (clipNum >= 0 && clipNum < soundSetting.clipNames.Count)
        {
            string SoundName = soundSetting.clipNames[clipNum];
            return SoundName;
        }
        else return null;
    }
}