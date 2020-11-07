using UnityEngine;

public class ResourceData
{
    public AudioClip clip;
    public string name;

    public bool isSelected = false;

    public ResourceData(string _name, AudioClip _clip)
    {
        name = _name;
        clip = _clip;
    }
}
