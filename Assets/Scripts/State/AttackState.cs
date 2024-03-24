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
        Debug.Log(chessObject.ObjectName + " AttackState Execute");
    }

    public void Exit()
    {
        Debug.Log(chessObject.ObjectName + " AttackState Exit");
    }
}