using UnityEngine;

public abstract class ImportTarget : MonoBehaviour
{
    public abstract void Import(string _collection, ResourceData[] _resources);
}
