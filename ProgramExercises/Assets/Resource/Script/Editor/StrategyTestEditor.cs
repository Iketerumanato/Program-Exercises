using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StrategyTest))]
public class StrategyTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        StrategyTest strategyTest = (StrategyTest)target;

        if (GUILayout.Button("バナナ銃を装備"))
        {
            strategyTest.EquipBananaGun();
        }

        if (GUILayout.Button("大きなカジキを装備"))
        {
            strategyTest.EquipBigMarlin();
        }
    }
}