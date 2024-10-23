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

    public void PlaySound(int clipNum)
    {
        if (soundSetting != null && clipNum >= 0 && clipNum < soundSetting.audioClips.Count)
        {
            AudioClip clip = soundSetting.audioClips[clipNum];
            float volume = soundSetting.volumes[clipNum];

            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid AudioClip index or SoundSettings not assigned.");
        }
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