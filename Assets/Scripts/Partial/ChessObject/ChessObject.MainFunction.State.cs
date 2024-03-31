using UnityEngine;

// Desc: 棋子主要功能API，游戏中的状态相关
public partial class ChessObject
{
    /// <summary>
    /// 设置状态前
    /// </summary>
    protected virtual void SetStateBefore() { }

    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="state"></param>
    public void SetState(IState state)
    {
        if (currentState != null)
        {
            currentState.Exit(); // 退出旧状态
        }

        currentState = state; // 设置新状态
        currentState.Enter(); // 进入新状态
    }

    /// <summary>
    /// 获取当前状态
    /// </summary>
    /// <returns></returns>
    public IState GetCurrentState()
    {
        return currentState;
    }

    /// <summary>
    /// 判断当前状态是否是某个状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool IsCurrentState(IState state)
    {
        return currentState == state;
    }

    // 死亡
    public void Die()
    {
        SetState(new DeadState(this));
    }
}