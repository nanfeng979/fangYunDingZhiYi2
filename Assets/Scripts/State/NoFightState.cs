using UnityEngine;

public class NoFightState : IState
{
    private ChessObject chessObject;

    public NoFightState(ChessObject chessObject)
    {
        this.chessObject = chessObject;
    }

    public void Enter()
    {
        Debug.Log(chessObject.ObjectName + " NoFightState Enter");

        chessObject.IsFight = false;
        // 载入装备属性
        chessObject.LoadEquipmentProperties();
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        Debug.Log(chessObject.ObjectName + " NoFightState Exit");
    }
}