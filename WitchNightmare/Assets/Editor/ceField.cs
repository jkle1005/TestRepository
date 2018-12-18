using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

using Witch;

[CustomEditor(typeof(Field))]
public class ceField : Editor
{
    ReorderableList reorderableList;

    SerializedProperty type;
    SerializedProperty number;
    SerializedProperty size;

    SerializedProperty boundary_top;
    SerializedProperty boundary_bottom;
    SerializedProperty boundary_left;
    SerializedProperty boundary_right;
       
    SerializedProperty load;

    private void OnEnable()
    {
        type = serializedObject.FindProperty("type");
        number = serializedObject.FindProperty("number");
        size = serializedObject.FindProperty("size");
        load = serializedObject.FindProperty("load_id");
        boundary_top = serializedObject.FindProperty("boundary_top");
        boundary_bottom = serializedObject.FindProperty("boundary_bottom");
        boundary_left = serializedObject.FindProperty("boundary_left");
        boundary_right = serializedObject.FindProperty("boundary_right");

        var prop = serializedObject.FindProperty("gates");
        reorderableList = new ReorderableList(serializedObject, prop);
        reorderableList.elementHeight = 78;
        reorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = prop.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, element);
        };

        var defaultColor = GUI.backgroundColor;

        reorderableList.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, prop.displayName);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(type, new GUIContent("Type"));
        EditorGUILayout.PropertyField(number, new GUIContent("Number"));
        EditorGUILayout.PropertyField(size, new GUIContent("Size"));

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Boundary Size");
        EditorGUILayout.PropertyField(boundary_top, new GUIContent("Top"));
        EditorGUILayout.PropertyField(boundary_bottom, new GUIContent("Bottom"));
        EditorGUILayout.PropertyField(boundary_left, new GUIContent("Left"));
        EditorGUILayout.PropertyField(boundary_right, new GUIContent("Right"));

        EditorGUILayout.Space();

        reorderableList.DoLayoutList();

        EditorGUILayout.Space();

        if (GUILayout.Button("Save"))
        {
            Field field = target as Field;
            if (field)
            {
                field.Save();
            }
        }

        EditorGUILayout.PropertyField(load, new GUIContent("Load ID"));

        if (GUILayout.Button("Load"))
        {
            Field field = target as Field;
            if (field)
            {
                field.Load();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}

[CustomPropertyDrawer(typeof(Gate))]
public class GateDrawer : PropertyDrawer
{
    private Gate gate;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {        
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            EditorGUIUtility.labelWidth = 100;

            position.height = EditorGUIUtility.singleLineHeight;

            var halfWidth = position.width * 0.5f;
            
            var egoRect = new Rect(position)
            {
                
            };

            var xftRect = new Rect(egoRect)
            {
                y = egoRect.y + EditorGUIUtility.singleLineHeight + 2
            };

            var xfnRect = new Rect(xftRect)
            {
                y = xftRect.y + EditorGUIUtility.singleLineHeight + 2
            };

            var xgnRect = new Rect(xfnRect)
            {
                y = xfnRect.y + EditorGUIUtility.singleLineHeight + 2
            };

            var egoProperty = property.FindPropertyRelative("enter_gate");
            var xftProperty = property.FindPropertyRelative("exit_field_type");
            var xfnProperty = property.FindPropertyRelative("exit_field_number");
            var xgnProperty = property.FindPropertyRelative("exit_gate_number");

            egoProperty.objectReferenceValue =
                EditorGUI.ObjectField(egoRect, 
                "Gate Object", egoProperty.objectReferenceValue, typeof(GameObject), true);
            
            xftProperty.enumValueIndex =
                (int)(FIELD_TYPE)(EditorGUI.EnumPopup(xftRect,
                "Field Type", (FIELD_TYPE)xftProperty.intValue));
            

            xfnProperty.intValue =
                EditorGUI.IntField(xfnRect,
                "Field number", xfnProperty.intValue);

            xgnProperty.intValue =
                EditorGUI.IntField(xgnRect,
                "Gate number", xgnProperty.intValue);
        }
    }
}

