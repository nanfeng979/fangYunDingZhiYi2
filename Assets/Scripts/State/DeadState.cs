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
        Debug.Log(chessObject.ObjectName + " DeadState Enter");
    }

    public void Execute()
    {
        Debug.Log(chessObject.ObjectName + " DeadState Execute");
    }

    public void Exit()
    {
        Debug.Log(chessObject.ObjectName + " DeadState Exit");
    }
}