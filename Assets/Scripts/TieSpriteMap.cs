using System.Collections.Generic;
using UnityEngine;
using System;
using Y9g;

public class TieSpriteMap : Singleton<TieSpriteMap>
{
    [SerializeField]
    private List<TieSpriteMapStruct> TieSpriteMapList;
    private Dictionary<string, List<Sprite>> TieSpriteMapDict = new Dictionary<string, List<Sprite>>();


    void Start()
    {
        InitDict();
    }

    private void InitDict()
    {
        foreach (var item in TieSpriteMapList)
        {
            List<Sprite> spriteList = new List<Sprite>();
            spriteList.Add(item.value);
            spriteList.Add(item.value2);
            spriteList.Add(item.value3);
            spriteList.Add(item.value4);
            TieSpriteMapDict.Add(item.key, spriteList);
        }
    }

    public Sprite GetSprite(string key, int index)
    {
        if (index < 0 || index > 3)
        {
            Debug.LogWarning("Index out of range");
            return null;
        }
        return TieSpriteMapDict[key][index];
    }
}

[Serializable]
public struct TieSpriteMapStruct
{
    public string key;
    public Sprite value;
    public Sprite value2;
    public Sprite value3;
    public Sprite value4;
}