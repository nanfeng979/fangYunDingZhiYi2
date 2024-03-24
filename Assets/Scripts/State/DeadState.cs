using UnityEngine;

public class DeadState : IState
{
    private ChessObject chessObject;

    public DeadState(ChessObject chessObject)
    {
        this.chessObject = chessObject;
    }

    public void Enter()
    {
        chessObject.IsDead = true;

        // 从棋盘中移除
        ChessBoardManager.Instance.RemoveChessObjectByVector2IntIndex(chessObject.Vector2IntIndex);

        Debug.Log(chessObject.ObjectName + " DeadState Enter");
    }

    public void Execute()
    {
        chessObject.gameObject.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log(chessObject.ObjectName + " DeadState Exit");
    }
}