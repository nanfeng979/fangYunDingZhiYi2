using System.Collections.Generic;
using UnityEngine;

// Desc: 棋子羁绊相关部分
public partial class ChessObject
{
    protected List<string> selfTies = new List<string>(); // 羁绊列表
    protected GameObject tiesListGameObject; // 羁绊列表对象

    /// <summary>
    /// 绑定羁绊列表
    /// </summary>
    protected void BingdTiesList()
    {
        tiesListGameObject = TiesManager.Instance.GetTiesListGameObject();
    }

    /// <summary>
    /// 将自身羁绊加入到羁绊列表
    /// </summary>
    protected void LoadSelfTiesToTiesList()
    {
        if (tiesListGameObject == null)
        {
            Debug.LogError("tiesListGameObject is null");
        }
        
    }
}