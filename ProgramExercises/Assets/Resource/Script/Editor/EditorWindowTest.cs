using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class EditorWindowTest : EditorWindow
{
#if UNITY_EDITOR
    private List<GameObject> copyTargets = new();

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
        var root = rootVisualElement;

        _rootVisualTreeAsset.CloneTree(root);
        root.styleSheets.Add(_rootStyleSheet);

        var listView = root.Q<ListView>("ObjectList");
        listView.itemsSource = copyTargets;

        var addButton = root.Q<Button>("unity-list-vie__add-button");
        addButton.clicked += () =>
        {
            GameObject newObject = new();
            copyTargets.Add(newObject);
            listView.Rebuild();
        };

        var copyButton = root.Q<Button>("copy_btn");
        copyButton.clicked += () =>
        {
            var moto = (GameObject)root.Q<ObjectField>("moto_obj").value;

            foreach (var target in copyTargets)
            {
                if (target != null)
                {
                    target.GetComponent<Renderer>().sharedMaterial = moto.GetComponent<Renderer>().sharedMaterial;
                }
            }
        };
    }
#endif
}