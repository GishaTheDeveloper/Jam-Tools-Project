using UnityEngine;
using System;

public class EffectsManager : ImportTarget
{
    #region Singleton
    public static EffectsManager Instance { private set; get; }
    #endregion

    public EffectData[] effectsCollection;

    void Awake()
    {
        Instance = this;
    }

    public void Emit(string effectName, Vector3 position, Quaternion rotation)
    {
        EffectData effect = Array.Find(effectsCollection, x => x.name == effectName);

        if (effect == null)
        {
            Debug.LogErrorFormat("Effect with name {0} wasn't found!", effectName);
            return;
        }

        GameObject.Instantiate(effect.prefab, position, rotation);
    }

    #region ImportTarget
    public override void Import(string _collection, ResourceData[] _resources)
    {
        EffectData[] coll = new EffectData[_resources.Length];

        for (int i = 0; i < _resources.Length; i++)
        {
            EffectData data = new EffectData();
            data.name = _resources[i].name;
            data.prefab = _resources[i].o as GameObject;

            coll[i] = data;
        }

        this.GetType().GetField(_collection).SetValue(this, coll);
    }
    #endregion
}

[Serializable]
public class EffectData
{
    public string name;
    public GameObject prefab;
}