using System.Collections.Generic;
using UnityEngine;
using Y9g;

public class ChessObjectPrefabMap : Singleton<ChessObjectPrefabMap>
{
    // 棋子预制体映射表
    [SerializeField]
    private List<ChessObjectPrefabMapStruct> prefabMapList = new List<ChessObjectPrefabMapStruct>();

    /// <summary>
    /// 通过key获取预制体
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public GameObject GetPrefab(string key)
    {
        foreach (var item in prefabMapList)
        {
            if (item.key == key)
            {
                return item.value;
            }
        }
        return null;
    }
}

// 棋子预制体映射表结构
[System.Serializable]
public struct ChessObjectPrefabMapStruct
{
    public string key;
    public GameObject value;
}