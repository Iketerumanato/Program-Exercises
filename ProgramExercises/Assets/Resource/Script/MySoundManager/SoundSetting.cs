using System.Collections.Generic;
using UnityEngine;

public class SoundSetting : ScriptableObject
{
    public List<AudioClip> audioClips = new();
    public List<float> volumes = new();
}