using UnityEngine;

public class AttackState : IState
{
    private ChessObject chessObject;

    private float timeBetweenAttacks; // 攻击间隔

    public AttackState(ChessObject chessObject)
    {
        this.chessObject = chessObject;
    }

    public void Enter()
    {
        chessObject.IsFight = true; // 设置为战斗状态

        Debug.Log(chessObject.ObjectName + " AttackState Enter");
    }

    public void Execute()
    {
        float attackSpeed = chessObject.AttackSpeed == 0 ? 1 : chessObject.AttackSpeed; // 设置攻击速度

        // 攻击速度判定
        timeBetweenAttacks += Time.deltaTime;
        if (timeBetweenAttacks < 1 / attackSpeed)
        {
            return;
        }
        timeBetweenAttacks = 0;
        
        // 获取周围的棋子对象
        ChessObject aroundChessObject = chessObject.GetSurroundingChessObject();
        if (aroundChessObject != null)
        {
            // 攻击周围的棋子对象
            chessObject.NormalAttackFun(aroundChessObject);
        }

        string str;
        if (chessObject.ObjectName == "Jiqiren") str = "<color=green>";
        else if (chessObject.ObjectName == "Wei") str = "<color=red>";
        else str = "<color=blue>";

        str += chessObject.HP + " chessObject.HP";

        str += "</color>";

        // Debug.Log(str);
    }

    public void Exit()
    {
        Debug.Log(chessObject.ObjectName + " AttackState Exit");
    }
}