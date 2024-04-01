using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Y9g;

public class IDToGameObjectMap : Singleton<IDToGameObjectMap>
{
    private Dictionary<int, GameObject> idToGameObject = new Dictionary<int, GameObject>();

    // 注册对象
    public void RegisterObject(int id, GameObject gameObject)
    {
        if (!idToGameObject.ContainsKey(id))
        {
            idToGameObject.Add(id, gameObject);
        }
    }

    // 获取对象
    public GameObject GetObject(int id)
    {
        if (idToGameObject.ContainsKey(id))
        {
            return idToGameObject[id];
        }
        return null;
    }

    // 打印所有对象
    public void PrintAll()
    {
        foreach (var item in idToGameObject)
        {
            Debug.Log(item.Key + " : " + item.Value);
        }
    }
}