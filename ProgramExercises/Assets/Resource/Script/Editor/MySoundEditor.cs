using UnityEngine;
using UnityEditor;

public class MySoundEditor : EditorWindow
{
    [MenuItem("Window/MySoundManager")]
    public static void ShowWindow()
    {
        GetWindow<MySoundManagerEditor>("MySoundManager");
    }
    private void OnGUI()
    {
        GUILayout.Label("Sound Settiong", EditorStyles.boldLabel);
    }
}