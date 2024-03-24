using UnityEngine;

public class AttackState : IState
{
    private ChessObject chessObject;

    private float attackTime = 0; // 攻击时间
    private float attackInterval; // 攻击间隔

    public AttackState(ChessObject chessObject)
    {
        this.chessObject = chessObject;
    }

    public void Enter()
    {
        attackInterval = chessObject.AttackInterval == 0 ? 1 : chessObject.AttackInterval;

        Debug.Log(chessObject.ObjectName + " AttackState Enter");
    }

    public void Execute()
    {
        attackTime += Time.deltaTime;
        if (attackTime < attackInterval)
        {
            return;
        }
        attackTime -= attackInterval;

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