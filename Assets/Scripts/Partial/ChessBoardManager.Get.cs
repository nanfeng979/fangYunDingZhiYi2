using UnityEngine;

public partial class ChessBoardManager
{
    /// <summary>
    /// 通过行列索引获取格子位置
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    private Vector3 GetCellPositionByRowColIndex(int row, int col)
    {
        if (row < 0 || row >= rowCount || col < 0 || col >= columnCount)
        {
            Debug.LogError("GetCellPositionByRowColIndex: row or column out of range");
            return Vector3.zero;
        }

        Vector3 position = Vector3.zero;

        // 如果在敌人区域
        if (row < 4)
        {
            int index = GetIndexByRowColIndex(row, col); // 得到格子索引
            position = enemyChessArea.transform.GetChild(index).position; // 得到格子在敌人棋盘区域的位置
        }
        // 如果在玩家区域
        else
        {
            int index = GetIndexByRowColIndex(row - 4, col); // 得到格子索引
            position = playerChessArea.transform.GetChild(index).position; // 得到格子在玩家棋盘区域的位置
        }

        position.y = 0; // 暂时先这样处理

        return position;
    }

    /// <summary>
    /// 通过行列索引获取格子索引
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    private int GetIndexByRowColIndex(int row, int col)
    {
        return row * columnCount + col;
    }

    /// <summary>
    /// 通过行列索引判断格子是否存在棋子
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    private bool IsExistChessOnCellByRowColIndex(int row, int col)
    {
        return chessBoardModel[row, col] != null;
    }
}