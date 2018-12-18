using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LayerControl))]
public class ceLayerControl : Editor
{
    
    SerializedProperty Adjust;
    SerializedProperty Static;
    SerializedProperty Type;
    SerializedProperty Renderer;
    SerializedProperty Value;

    private void OnEnable()
    {
        Adjust = serializedObject.FindProperty("adjust_y");
        Static = serializedObject.FindProperty("static_layer");
        Type = serializedObject.FindProperty("static_layer_type");
        Renderer = serializedObject.FindProperty("static_layer_srr");
        Value = serializedObject.FindProperty("static_layer_val");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();        
        EditorGUILayout.PropertyField(Adjust, new GUIContent("Adjust"));
        EditorGUILayout.PropertyField(Static, new GUIContent("isStatic"));        

        if (Static.boolValue)
        {            
            EditorGUILayout.PropertyField(Type, new GUIContent("Type"));
            EditorGUILayout.PropertyField(Renderer, new GUIContent("Renderer"));
            EditorGUILayout.PropertyField(Value, new GUIContent("Value"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
