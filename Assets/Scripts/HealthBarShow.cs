using System.Collections.Generic;
using UnityEngine;
using Y9g;

public class HealthBarShow : Singleton<HealthBarShow>
{
    [SerializeField]
    private GameObject healthBarPrefab;

    private Dictionary<string, GameObject> healthBarDic = new Dictionary<string, GameObject>();

    // 实例化血条
    public void InstantiateHealthBar(ChessObject chessObject)
    {
        GameObject healthBar = Instantiate(healthBarPrefab, Vector3.zero, Quaternion.identity);
        healthBar.transform.SetParent(chessObject.transform);
        healthBarDic.Add(chessObject.ObjectName, healthBar);
    }
}