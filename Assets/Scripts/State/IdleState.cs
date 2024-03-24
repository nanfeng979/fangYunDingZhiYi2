using UnityEngine;

public class IdleState : IState
{
    private ChessObject chessObject;

    public IdleState(ChessObject chessObject)
    {
        this.chessObject = chessObject;
    }

    public void Enter()
    {
        Debug.Log(chessObject.ObjectName + " IdleState Enter");

        // 载入装备属性
        chessObject.LoadEquipmentProperties();
    }

    public void Execute()
    {
        string str;

        if (chessObject.ObjectName == "Jiqiren") str = "<color=green>";
        else if (chessObject.ObjectName == "Wei") str = "<color=red>";
        else str = "<color=blue>";

        // str += chessObject.ObjectName + " IdleState Execute";
        string aroundChessObjectName = chessObject.GetSurroundingChessObject().ObjectName;

        str += "</color>";

        // Debug.Log(str);
    }

    public void Exit()
    {
        Debug.Log(chessObject.ObjectName + " IdleState Exit");
    }
}