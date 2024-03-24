public class ChessObject : YunDingZhiYiBaseObject
{
    protected IState currentState;

    public void SetState(IState state)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = state;
        currentState.Enter();
    }

    protected override void Start()
    {
        base.Start();

        SetState(new IdleState(this));
    }

    protected override void Update()
    {
        base.Update();

        if (currentState != null)
        {
            currentState.Execute();
        }
    }
}