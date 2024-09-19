using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class EditorWindowTest : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("EditorWindowTest/オブジェクトをまとめてコピー")]
    private static void ShowWindow()
    {
        var window = GetWindow<EditorWindowTest>("UIElements");
        window.titleContent = new GUIContent("EditorWindowTest");
        window.Show();
    }

    [SerializeField] private VisualTreeAsset _rootVisualTreeAsset;
    [SerializeField] private StyleSheet _rootStyleSheet;

    private void CreateGUI()
    {
        _rootVisualTreeAsset.CloneTree(rootVisualElement);
        rootVisualElement.styleSheets.Add(_rootStyleSheet);

        var copyButton = rootVisualElement.Q<Button>("copy_btn");
        copyButton.clicked += () =>
        {
            var moto1 = (GameObject)rootVisualElement.Q<ObjectField>("moto1_obj").value;
            var saki1 = (GameObject)rootVisualElement.Q<ObjectField>("saki1_obj").value;
            var saki2 = (GameObject)rootVisualElement.Q<ObjectField>("saki2_obj").value;
            var saki3 = (GameObject)rootVisualElement.Q<ObjectField>("saki3_obj").value;
            saki1.GetComponent<Renderer>().sharedMaterial = moto1.GetComponent<Renderer>().sharedMaterial;
            saki2.GetComponent<Renderer>().sharedMaterial = moto1.GetComponent<Renderer>().sharedMaterial;
            saki3.GetComponent<Renderer>().sharedMaterial = moto1.GetComponent<Renderer>().sharedMaterial;
        };
    }
#endif
}