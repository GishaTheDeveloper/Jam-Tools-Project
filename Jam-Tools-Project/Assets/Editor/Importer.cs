using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Importer : EditorWindow
{
    private string folderPath;
    private List<AudioClip> resources = new List<AudioClip>();

    private bool IsLoadedResources { get => resources.Count > 0; }

    Vector2 scrollPos = Vector2.zero;

    #region Styles
    GUIStyle descText;
    #endregion

    [MenuItem("Tools/Importer")]
    static void Init()
    {
        Importer window = (Importer)EditorWindow.GetWindow(typeof(Importer));

        window.Show();
    }

    private List<AudioClip> GetResourcesAtPath(string _path)
    {
        if (!Directory.Exists(_path))
        {
            Debug.LogError("Folder wasn't found.");
            return null;
        }

        string[] files = Directory.GetFiles(_path);
        List<AudioClip> result = new List<AudioClip>();

        for (int i = 0; i < files.Length; i++)
        {
            AudioClip clip = AssetDatabase.LoadAssetAtPath(files[i], typeof(AudioClip)) as AudioClip;
            if (clip != null)
                result.Add(clip);
        }

        return result;
    }

    void OnEnable()
    {
        #region Styles
        descText = new GUIStyle();
        descText.fontSize = 14;
        descText.fontStyle = FontStyle.Bold;
        descText.normal.textColor = Color.white;
        #endregion
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("* Use Importer to easily fill necessary components with resources.", descText);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();

        folderPath = EditorGUILayout.TextField("Path:", folderPath);

        if (GUILayout.Button("Inspect"))
            resources = GetResourcesAtPath(folderPath);

        if (IsLoadedResources)
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, false);
            for (int i = 0; i < resources.Count; i++)
            {
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField((i + 1).ToString());
                EditorGUILayout.LabelField(resources[i].name);
                EditorGUILayout.ObjectField(resources[i], typeof(AudioClip), false);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }


        EditorGUILayout.EndVertical();
    }
}
