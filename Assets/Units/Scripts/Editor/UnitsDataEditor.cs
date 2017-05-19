using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitsData))]
public class UnitsDataEditor : Editor
{
    private UnitsData script;

    private void OnEnable()
    {
        script = target as UnitsData;
    }

    public override void OnInspectorGUI()
    {
     //   EditorGUILayout.BeginHorizontal();
        
        script.maxUnits = EditorGUILayout.IntField("Maxim units",script.maxUnits);
        script.unitTypes = EditorGUILayout.IntField("Unit types", script.unitTypes);

     //   EditorGUILayout.EndHorizontal();

        for (int i = 0; i < script.units.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            script.units[i].prefab = EditorGUILayout.ObjectField(script.units[i].prefab, typeof(GameObject)) as GameObject;
            EditorGUILayout.EndHorizontal();
        }
    }

}
