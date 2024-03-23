using UnityEngine;
using Y9g;

public partial class ChessBoardManager : Singleton<ChessBoardManager>
{
    private readonly int cellCount = 56; // 棋盘格子数量
    private readonly int rowCount = 8; // 棋盘行数
    private readonly int columnCount = 7; // 棋盘列数

    // 敌人棋子
    private GameObject enemyChessArea;
    // 玩家棋子
    private GameObject playerChessArea;
    // 所有实例化的棋子
    private GameObject chessObjectListObject;

    // 棋盘格子数据
    private ChessObject[,] chessBoardModel; 


    void Start()
    {
        Init();

        TempInit();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        enemyChessArea = transform.Find("EnemyChessArea").gameObject;
        playerChessArea = transform.Find("PlayerChessArea").gameObject;
        chessObjectListObject = transform.Find("ChessObjectListObject").gameObject;

        InitChessBoardModel();
    }

    private void TempInit()
    {
        AddChessObject(ChessObjectPrefabMap.Instance.GetPrefab("Kaier"), 2, 3); // 添加棋子到棋盘格子
        AddChessObject(ChessObjectPrefabMap.Instance.GetPrefab("Kasha"), 3, 3); // 添加棋子到棋盘格子
    }

    /// <summary>
    /// 初始化棋盘格子数据
    /// </summary>
    private void InitChessBoardModel()
    {
        chessBoardModel = new ChessObject[rowCount, columnCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                chessBoardModel[i, j] = null;
            }
        }
    }

    /// <summary>
    /// 添加棋子到棋盘格子
    /// </summary>
    /// <param name="chessObject"></param>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public void AddChessObject(GameObject chessObject, int row, int col)
    {
        if (row < 0 || row >= rowCount || col < 0 || col >= columnCount)
        {
            Debug.LogError("AddChessObject: row or column out of range");
            return;
        }

        if (IsExistChessOnCellByRowColIndex(row, col))
        {
            Debug.LogError("AddChessObject: cell already has a chess object");
            return;
        }

        if (chessObject == null)
        {
            Debug.LogError("AddChessObject: chess object is null");
            return;
        }

        if (chessObject.GetComponent<ChessObject>() == null)
        {
            Debug.LogError("AddChessObject: chess object has no ChessObject component");
            return;
        }

        GameObject instantChess = InstantiateChess(chessObject, row, col); // 实例化棋子
        chessBoardModel[row, col] = instantChess.GetComponent<ChessObject>(); // 添加到棋盘格子
    }

    /// <summary>
    /// 实例化棋子
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="row"></param>
    /// <param name="column"></param>
    private GameObject InstantiateChess(GameObject prefab, int row, int column)
    {
        GameObject chess = Instantiate(prefab, chessObjectListObject.transform); // 实例化棋子
        chess.transform.localPosition = GetCellPositionByRowColIndex(row, column); // 设置棋子位置
        return chess;
    }

}