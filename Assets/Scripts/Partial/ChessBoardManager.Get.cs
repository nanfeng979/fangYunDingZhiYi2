using System.Collections.Generic;
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

    /// <summary>
    /// 通过Vector2Int索引找出周围的棋子中的随机一个棋子
    /// </summary>
    /// <param name="vector2IntIndex"></param>
    /// <returns></returns>
    public ChessObject GetChessObjectBySurroundingChessObjectWithRandom(Vector2Int vector2IntIndex)
    {
        Vector2Int aroundIndex = GetVector2IntIndexBySurroundingChessObjectWithRandom(vector2IntIndex);
        return GetChessObjectByVector2IntIndex(aroundIndex);
    }

    /// <summary>
    /// 通过Vector2Int索引找出棋子
    /// </summary>
    /// <param name="vector2IntIndex"></param>
    /// <returns></returns>
    private ChessObject GetChessObjectByVector2IntIndex(Vector2Int vector2IntIndex)
    {
        return chessBoardModel[vector2IntIndex.x, vector2IntIndex.y];
    }

    /// <summary>
    /// 通过Vector2Int索引找出周围的棋子中的随机一个棋子的Vector2Int索引
    /// </summary>
    /// <param name="vector2IntIndex"></param>
    /// <returns></returns>
    public Vector2Int GetVector2IntIndexBySurroundingChessObjectWithRandom(Vector2Int vector2IntIndex)
    {
        List<Vector2Int> aroundCellIndex = GetSurroundingCellsByVector2IntIndex(vector2IntIndex);
        return GetVector2IndexByChessObjectsWithRandom(aroundCellIndex);
    }

    /// <summary>
    /// 通过Vector2Int索引获取周围格子的各自Vector2Int索引
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    private List<Vector2Int> GetSurroundingCellsByVector2IntIndex(Vector2Int vector2IntIndex)
    {
        int rowIndex = vector2IntIndex.x;
        int columnIndex = vector2IntIndex.y;

        LeftOrRight leftOrRight = rowIndex % 2 == 0 ? LeftOrRight.Left : LeftOrRight.Right;

        List<Vector2Int> aroundCellIndex = new List<Vector2Int>();

        int index1OfRow = rowIndex;
        int index1OfCol = columnIndex + 1;
        if (index1OfRow >= 0 && index1OfRow < 8 && index1OfCol >= 0 && index1OfCol < 7)
        {
            aroundCellIndex.Add(new Vector2Int(index1OfRow, index1OfCol));
        }
        else
        {
            aroundCellIndex.Add(new Vector2Int(-1, -1));
        }

        if (leftOrRight == LeftOrRight.Left)
        {
            int index2OfRow = rowIndex + 1;
            int index2OfCol = columnIndex;
            if (index2OfRow >= 0 && index2OfRow < 8 && index2OfCol >= 0 && index2OfCol < 7)
            {
                aroundCellIndex.Add(new Vector2Int(index2OfRow, index2OfCol));
            }
            else
            {
                aroundCellIndex.Add(new Vector2Int(-1, -1));
            }
        }
        else
        {
            int index2OfRow = rowIndex + 1;
            int index2OfCol = columnIndex + 1;
            if (index2OfRow >= 0 && index2OfRow < 8 && index2OfCol >= 0 && index2OfCol < 7)
            {
                aroundCellIndex.Add(new Vector2Int(index2OfRow, index2OfCol));
            }
            else
            {
                aroundCellIndex.Add(new Vector2Int(-1, -1));
            }
        }
        
        if (leftOrRight == LeftOrRight.Left)
        {
            int index3OfRow = rowIndex + 1;
            int index3OfCol = columnIndex - 1;
            if (index3OfRow >= 0 && index3OfRow < 8 && index3OfCol >= 0 && index3OfCol < 7)
            {
                aroundCellIndex.Add(new Vector2Int(index3OfRow, index3OfCol));
            }
            else
            {
                aroundCellIndex.Add(new Vector2Int(-1, -1));
            }
        }
        else
        {
            int index3OfRow = rowIndex + 1;
            int index3OfCol = columnIndex;
            if (index3OfRow >= 0 && index3OfRow < 8 && index3OfCol >= 0 && index3OfCol < 7)
            {
                aroundCellIndex.Add(new Vector2Int(index3OfRow, index3OfCol));
            }
            else
            {
                aroundCellIndex.Add(new Vector2Int(-1, -1));
            }
        }

        int index4OfRow = rowIndex;
        int index4OfCol = columnIndex - 1;
        if (index4OfRow >= 0 && index4OfRow < 8 && index4OfCol >= 0 && index4OfCol < 7)
        {
            aroundCellIndex.Add(new Vector2Int(index4OfRow, index4OfCol));
        }
        else
        {
            aroundCellIndex.Add(new Vector2Int(-1, -1));
        }

        if (leftOrRight == LeftOrRight.Left)
        {
            int index5OfRow = rowIndex - 1;
            int index5OfCol = columnIndex - 1;
            if (index5OfRow >= 0 && index5OfRow < 8 && index5OfCol >= 0 && index5OfCol < 7)
            {
                aroundCellIndex.Add(new Vector2Int(index5OfRow, index5OfCol));
            }
            else
            {
                aroundCellIndex.Add(new Vector2Int(-1, -1));
            }
        }
        else
        {
            int index5OfRow = rowIndex - 1;
            int index5OfCol = columnIndex;
            if (index5OfRow >= 0 && index5OfRow < 8 && index5OfCol >= 0 && index5OfCol < 7)
            {
                aroundCellIndex.Add(new Vector2Int(index5OfRow, index5OfCol));
            }
            else
            {
                aroundCellIndex.Add(new Vector2Int(-1, -1));
            }
        }
        
        if (leftOrRight == LeftOrRight.Left)
        {
            int index6OfRow = rowIndex - 1;
            int index6OfCol = columnIndex;
            if (index6OfRow >= 0 && index6OfRow < 8 && index6OfCol >= 0 && index6OfCol < 7)
            {
                aroundCellIndex.Add(new Vector2Int(index6OfRow, index6OfCol));
            }
            else
            {
                aroundCellIndex.Add(new Vector2Int(-1, -1));
            }
        }
        else
        {
            int index6OfRow = rowIndex - 1;
            int index6OfCol = columnIndex + 1;
            if (index6OfRow >= 0 && index6OfRow < 8 && index6OfCol >= 0 && index6OfCol < 7)
            {
                aroundCellIndex.Add(new Vector2Int(index6OfRow, index6OfCol));
            }
            else
            {
                aroundCellIndex.Add(new Vector2Int(-1, -1));
            }
        }

        return aroundCellIndex;
    }

    /// <summary>
    /// 随机获取多个格子中的一个有棋子的Vector2Int索引
    /// </summary>
    /// <param name="cells"></param>
    /// <returns></returns>
    private Vector2Int GetVector2IndexByChessObjectsWithRandom(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> availableChess = new List<Vector2Int>();
        foreach (var vector2Int in vector2Ints)
        {
            // 如果格子存在，且格子上有棋子
            if (vector2Int.x != -1 && vector2Int.y != -1 && IsExistChessOnCellByRowColIndex(vector2Int.x, vector2Int.y))
            {
                availableChess.Add(vector2Int);
            }
        }

        if (availableChess.Count == 0)
        {
            return new Vector2Int(-1, -1);
        }

        return availableChess[Random.Range(0, availableChess.Count)];
    }

}