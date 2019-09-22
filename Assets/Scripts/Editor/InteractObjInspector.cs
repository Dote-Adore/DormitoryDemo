using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractObj))]

public class InteractObjInspector : Editor
{
    private SerializedObject serializedObject = null;  // 序列化
    private SerializedProperty type, shouldDestroy, canInteract, interactTime;
    void OnEnable()
    {
        serializedObject = new SerializedObject(target);
        type = serializedObject.FindProperty("type");
        shouldDestroy = serializedObject.FindProperty("shouldDestroy");
        canInteract = serializedObject.FindProperty("canInteract");
        interactTime = serializedObject.FindProperty("interactTime");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(type);
        if(type.enumValueIndex == (int)InteractObjType.Once)
        {
            EditorGUILayout.PropertyField(shouldDestroy);
        }
        EditorGUILayout.PropertyField(canInteract);
        EditorGUILayout.PropertyField(interactTime);
        serializedObject.ApplyModifiedProperties();//应用
    }
}
