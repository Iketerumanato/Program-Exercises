using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundSetting))]
public class SoundSettiongEditor : Editor
{
    const float DefaultVolumes = 1.0f;
    const float minVolumes = 0.0f;

    public override void OnInspectorGUI()
    {
        SoundSetting soundSetting= (SoundSetting)target;

        for (int clipCount = 0; clipCount < soundSetting.audioClips.Count; clipCount++)
        {
            soundSetting.clipNames[clipCount] = EditorGUILayout.TextField("Clip Name", soundSetting.clipNames[clipCount]);
            EditorGUILayout.ObjectField("Audio Clip", soundSetting.audioClips[clipCount], typeof(AudioClip), false);
            EditorGUILayout.Slider("Volume", soundSetting.volumes[clipCount], minVolumes, DefaultVolumes);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(soundSetting);
        }
    }
}