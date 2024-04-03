using System.Collections.Generic;
using UnityEngine;
using System;
using Y9g;

public class TieSpriteMap : Singleton<TieSpriteMap>
{
    [SerializeField]
    private List<TieSpriteMapStruct> TieSpriteMapList;
    private Dictionary<string, Sprite> TieSpriteMapDict = new Dictionary<string, Sprite>();


    void Start()
    {
        InitDict();
    }

    private void InitDict()
    {
        foreach (var item in TieSpriteMapList)
        {
            TieSpriteMapDict.Add(item.key, item.value);
        }
    }

    public Sprite GetSprite(string key)
    {
        if (TieSpriteMapDict.ContainsKey(key))
        {
            return TieSpriteMapDict[key];
        }
        return null;
    }
}

[Serializable]
public struct TieSpriteMapStruct
{
    public string key;
    public Sprite value;
}