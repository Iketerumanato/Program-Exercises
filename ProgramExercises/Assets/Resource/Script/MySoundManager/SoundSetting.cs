using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SoundSetting",menuName = "My Sound Manager/Sound Setting")]
public class SoundSetting : ScriptableObject
{
    public List<AudioClip> audioClips;
    public List<float> volumes;
}