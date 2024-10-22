using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MySoundManagerEditor : EditorWindow
{
    private List<AudioClip> audioClips = new();
    private Dictionary<AudioClip, float> clipVolumes = new();

    private SoundSetting soundSetting;

    [MenuItem("Window/Sound Manager")]
    public static void ShowWindow()
    {
        GetWindow<MySoundManagerEditor>("My Sound Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("Sound Settings", EditorStyles.boldLabel);

        soundSetting = (SoundSetting)EditorGUILayout.ObjectField("Sound Settings", soundSetting, typeof(SoundSetting), false);

        if (soundSetting != null)
        {
            if (GUILayout.Button("Load Settings"))
            {
                LoadSettings();
            }

            if (GUILayout.Button("Add Audio Clip"))
            {
                audioClips.Add(null);
            }

            for (int i = 0; i < audioClips.Count; i++)
            {
                audioClips[i] = (AudioClip)EditorGUILayout.ObjectField("Audio Clip " + (i + 1), audioClips[i], typeof(AudioClip), false);

                if (audioClips[i] != null)
                {
                    if (!clipVolumes.ContainsKey(audioClips[i]))
                    {
                        clipVolumes[audioClips[i]] = 1.0f; // デフォルト音量
                    }

                    clipVolumes[audioClips[i]] = EditorGUILayout.Slider("Volume", clipVolumes[audioClips[i]], 0.0f, 1.0f);
                }
            }

            if (GUILayout.Button("Save Settings"))
            {
                SaveSettings();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Please assign a SoundSettings asset.", MessageType.Warning);
        }
    }

    private void LoadSettings()
    {
        if (soundSetting == null) return;

        audioClips = new List<AudioClip>(soundSetting.audioClips);
        clipVolumes.Clear();

        for (int i = 0; i < soundSetting.audioClips.Count; i++)
        {
            clipVolumes[soundSetting.audioClips[i]] = soundSetting.volumes[i];
        }
    }

    private void SaveSettings()
    {
        if (soundSetting == null) return;

        // AudioClipリストと音量リストをクリア
        soundSetting.audioClips.Clear();
        soundSetting.volumes.Clear();

        // 現在の設定をScriptableObjectに保存
        foreach (var clip in audioClips)
        {
            soundSetting.audioClips.Add(clip);
            soundSetting.volumes.Add(clipVolumes.ContainsKey(clip) ? clipVolumes[clip] : 1.0f);
        }

        // ScriptableObjectの変更を保存
        EditorUtility.SetDirty(soundSetting);
        AssetDatabase.SaveAssets();

        Debug.Log("Settings Saved");
    }
}
