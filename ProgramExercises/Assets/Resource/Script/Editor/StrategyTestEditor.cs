using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StrategyTest))]
public class StrategyTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        StrategyTest strategyTest = (StrategyTest)target;

        if (GUILayout.Button("�o�i�i�e�𑕔�"))
        {
            strategyTest.EquipBananaGun();
        }

        if (GUILayout.Button("�傫�ȃJ�W�L�𑕔�"))
        {
            strategyTest.EquipBigMarlin();
        }
    }
}