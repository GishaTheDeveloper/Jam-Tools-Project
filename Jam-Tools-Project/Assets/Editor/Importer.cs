using UnityEditor;
using UnityEngine;

public class Importer : Editor
{
    [MenuItem("Tools/Importer")]
    static void Init()
    {
        Debug.Log("Window was called");
    }
}
