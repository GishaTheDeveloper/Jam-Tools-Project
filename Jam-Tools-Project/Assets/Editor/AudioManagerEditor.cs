using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    private SerializedProperty sfxProp;
    private SerializedProperty musicProp;

    private void OnEnable()
    {
        sfxProp = serializedObject.FindProperty("sfxCollection");
        musicProp = serializedObject.FindProperty("musicCollection");
    }

    public override void OnInspectorGUI()
    {
        var manager = target as AudioManager;

        EditorGUILayout.LabelField("Collections");

        serializedObject.Update();
        MimicArray(sfxProp);
        MimicArray(musicProp);
        serializedObject.ApplyModifiedProperties();

        //base.OnInspectorGUI();
    }

    private void MimicArray(SerializedProperty arrayProp)
    {
        EditorGUILayout.BeginVertical("box");
        arrayProp.isExpanded = EditorGUILayout.Foldout(arrayProp.isExpanded, arrayProp.name);

        if (arrayProp.isExpanded)
        {
            arrayProp.arraySize = EditorGUILayout.IntField("Size", arrayProp.arraySize);
            for (int i = 0; i < arrayProp.arraySize; i++)
            {
                SerializedProperty prop = arrayProp.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(prop, new GUIContent("Element " + i));
            }
        }
        EditorGUILayout.EndVertical();
    }
}
