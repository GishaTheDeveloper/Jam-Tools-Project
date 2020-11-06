using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    private SerializedProperty sfxProp;
    private SerializedProperty musicProp;

    #region Styles
    GUIStyle bold;
    GUIStyle collectionBlock;
    GUIStyle buttonsBlock;
    #endregion

    private void OnEnable()
    {
        sfxProp = serializedObject.FindProperty("sfxCollection");
        musicProp = serializedObject.FindProperty("musicCollection");
    }

    public override void OnInspectorGUI()
    {
        #region Styles
        bold = new GUIStyle();
        bold.fontSize = 15;
        bold.fontStyle = FontStyle.Bold;
        bold.normal.textColor = Color.white;

        collectionBlock = new GUIStyle("box");
        collectionBlock.margin = new RectOffset(0, 0, 0, 15);
        collectionBlock.padding = new RectOffset(0, 0, 10, 10);

        buttonsBlock = new GUIStyle();
        buttonsBlock.margin = new RectOffset(0, 0, 10, 0);
        #endregion

        var manager = target as AudioManager;

        EditorGUILayout.LabelField("Collections", bold);
        
        InitCollection(sfxProp, "SFX");
        InitCollection(musicProp, "Music");
    }

    private void InitCollection(SerializedProperty collection, string shortName)
    {
        Color defColor = GUI.backgroundColor;

        EditorGUILayout.BeginVertical(collectionBlock);

        serializedObject.Update();
        EditorGUILayout.PropertyField(collection, true);

        // Importer Call subblock.
        EditorGUILayout.BeginHorizontal(buttonsBlock);

        defColor = GUI.color;
        GUI.backgroundColor = Color.blue;
        if (GUILayout.Button(string.Format("Call {0} Importer", shortName)))
            Debug.Log("L");
        GUI.backgroundColor = defColor;

        // Clear subblock.
        defColor = GUI.color;
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button(string.Format("Clear {0} Collection", shortName)))
            collection.ClearArray();
        GUI.backgroundColor = defColor;

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.EndVertical();
    }
}
