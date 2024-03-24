using UnityEngine;

public class AttackState : IState
{
    private ChessObject chessObject;

    public AttackState(ChessObject chessObject)
    {
        this.chessObject = chessObject;
    }

    public void Enter()
    {
        Debug.Log(chessObject.ObjectName + " AttackState Enter");
    }

    public void Execute()
    {
        // Debug.Log(chessObject.ObjectName + " AttackState Execute");

        // 获取周围的棋子对象
        ChessObject aroundChessObject = chessObject.GetSurroundingChessObject();
        if (aroundChessObject != null)
        {
            // 攻击周围的棋子对象
            chessObject.AttackChessObject(aroundChessObject);
        }

        string str;
        if (chessObject.ObjectName == "Jiqiren") str = "<color=green>";
        else if (chessObject.ObjectName == "Wei") str = "<color=red>";
        else str = "<color=blue>";

        str += chessObject.HP + " chessObject.HP";

        str += "</color>";

        Debug.Log(str);
    }

    public void Exit()
    {
        Debug.Log(chessObject.ObjectName + " AttackState Exit");
    }
}