using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class MySoundManagerEditor : EditorWindow
{
    private List<AudioClip> audioClips = new();
    private Dictionary<AudioClip, float> clipVolumes = new();

    private string outputPath = "Assets/Resource/Data/Sound/SoundSettings.asset";  // デフォルトの保存パス

    const float DefaultVolume = 1.0f;
    const float minVolume = 0.0f;

    public static void ShowWindow()
    {
        GetWindow<MySoundManagerEditor>("Sound Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("Sound Settings", EditorStyles.boldLabel);

        // 出力パスの設定
        outputPath = EditorGUILayout.TextField("Save Path", outputPath);

        if (GUILayout.Button("Add Audio Clip"))
        {
            audioClips.Add(null);
        }

        // サウンドクリップリストの描画
        for (int clipCount = 0; clipCount < audioClips.Count; clipCount++)
        {
            GUILayout.BeginHorizontal();

            audioClips[clipCount] = (AudioClip)EditorGUILayout.ObjectField("Audio Clip " + (clipCount + 1), audioClips[clipCount], typeof(AudioClip), false);

            if (audioClips[clipCount] != null)
            {
                if (!clipVolumes.ContainsKey(audioClips[clipCount]))
                {
                    clipVolumes[audioClips[clipCount]] = DefaultVolume;
                }

                clipVolumes[audioClips[clipCount]] = EditorGUILayout.Slider("Volume", clipVolumes[audioClips[clipCount]], minVolume, DefaultVolume);
            }

            // 削除ボタン
            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                RemoveClip(clipCount);
            }

            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Save Settings"))
        {
            SaveSettings();
        }
    }

    // 設定の保存とScriptableObjectの生成
    private void SaveSettings()
    {
        // ScriptableObjectのインスタンスを作成
        SoundSetting newSettings = ScriptableObject.CreateInstance<SoundSetting>();

        // AudioClipとその音量をScriptableObjectに保存
        if (audioClips != null && clipVolumes != null)
        {
            foreach (var clip in audioClips)
            {
                newSettings.audioClips.Add(clip);
                newSettings.volumes.Add(clipVolumes.ContainsKey(clip) ? clipVolumes[clip] : DefaultVolume);
            }
        }

        // アセットとして保存
        if (File.Exists(outputPath))
        {
            Debug.LogWarning("File already exists at path: " + outputPath + ". Overwriting existing file.");
        }

        // ScriptableObjectをアセットとして指定のパスに保存
        AssetDatabase.CreateAsset(newSettings, outputPath);
        AssetDatabase.SaveAssets();

        Debug.Log("Settings saved to ScriptableObject at: " + outputPath);
    }

    // クリップの削除処理
    private void RemoveClip(int clipindex)
    {
        if (clipindex >= 0 && clipindex < audioClips.Count)
        {
            AudioClip clip = audioClips[clipindex];

            if (clip != null && clipVolumes.ContainsKey(clip))
            {
                clipVolumes.Remove(clip);
            }

            audioClips.RemoveAt(clipindex);
        }
    }
}