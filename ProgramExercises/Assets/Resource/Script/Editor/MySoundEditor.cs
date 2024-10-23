using UnityEngine;
using UnityEditor;

public class MySoundEditor : EditorWindow
{
    [MenuItem("Window/My Sound Manager")]
    public static void ShowWindow()
    {
        var window = GetWindow<MySoundManagerEditor>("My Sound Manager");
        window.minSize = new Vector2(700, 300);
    }
    private void OnGUI()
    {
        GUILayout.Label("Sound Settiong", EditorStyles.boldLabel);
    }
}