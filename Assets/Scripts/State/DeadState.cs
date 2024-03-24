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
        string str;

        if (chessObject.ObjectName == "Jiqiren") str = "<color=green>";
        else if (chessObject.ObjectName == "Wei") str = "<color=red>";
        else str = "<color=blue>";

        str += chessObject.ObjectName + " DeadState Execute";

        str += "</color>";

        Debug.Log(str);
    }

    public void Exit()
    {
        Debug.Log(chessObject.ObjectName + " DeadState Exit");
    }
}