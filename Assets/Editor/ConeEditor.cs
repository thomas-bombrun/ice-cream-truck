using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Cone))]
public class ConeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Cone cone = (Cone)target;
        if (GUILayout.Button("Random fill"))
        {
            cone.RandomFill();
        }
        if (GUILayout.Button("Reset"))
        {
            cone.Reset();
        }
    }
}
