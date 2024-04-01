// Desc: 棋子主要功能API，游戏中的操作相关
using UnityEngine;

public partial class ChessObject
{
    /// <summary>
    /// 普通攻击功能函数
    /// </summary>
    /// <param name="beAttackedChessObject"></param>
    public virtual void NormalAttackFun(ChessObject beAttackedChessObject)
    {
        // 当被攻击对象为空或者已经死亡时
        if (beAttackedChessObject.isDead || beAttackedChessObject == null)
        {
            return;
        }

        NormalAttackLogic(beAttackedChessObject); // 普通攻击逻辑
        NormalAttackAnimation(beAttackedChessObject); // 普通攻击动画
    }

    /// <summary>
    /// 普通攻击 // 逻辑层面
    /// </summary>
    /// <param name="beAttackedChessObject"></param>
    private void NormalAttackLogic(ChessObject beAttackedChessObject)
    {
        OnNormalAttack?.Invoke(this, attack); // 执行攻击事件
        beAttackedChessObject.BeNormalAttackedFun(this, attack); // 被攻击对象受到攻击
    }

    /// <summary>
    /// 被普通攻击 // 逻辑层面
    /// </summary>
    /// <param name="damageValue"></param>
    /// <param name="attackChessObject"></param>
    public void BeNormalAttackedLogic(ChessObject attackChessObject, float damageValue)
    {
        OnBeNormalAttacked?.Invoke(attackChessObject, damageValue); // 执行被攻击事件

        ReduceHpFun(damageValue); // 扣除生命值
    }

    /// <summary>
    /// 恢复生命值功能函数
    /// </summary>
    /// <param name="selfObject"></param>
    /// <param name="restoreHpValue"></param>
    public virtual void RestoredHpFun(float restoreHpValue)
    {
        HP += restoreHpValue;
        // Debug.LogError(objectName + " RestoredHpFun hp: " + hp);

        ShowRestoreHp(restoreHpValue); // 显示恢复的血量
    }

    /// <summary>
    /// 扣除生命值功能函数
    /// </summary>
    /// <param name="reduceHpValue"></param>
    protected virtual void ReduceHpFun(float reduceHpValue)
    {
        hp -= reduceHpValue;
        // Debug.LogError(objectName + " ReduceHpFun hp: " + hp);

        ShowReduceHP(reduceHpValue); // 显示掉血效果
    }

    /// <summary>
    /// 被普通攻击功能函数
    /// </summary>
    /// <param name="attackChessObject"></param>
    /// <param name="beAttackedValue"></param>
    protected virtual void BeNormalAttackedFun(ChessObject attackChessObject, float beAttackedValue)
    {
        if (isDead)
        {
            return;
        }

        BeNormalAttackedLogic(attackChessObject, beAttackedValue); // 被普通攻击逻辑

        if (hp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 朝向另一个对象
    /// </summary>
    /// <param name="otherChessObject"></param>
    private void LookAtTo(ChessObject otherChessObject)
    {
        if (modelTransform == null)
        {
            Debug.LogError("modelTransform is null");
            return;
        }

        if (otherChessObject.ModelTransform == null)
        {
            Debug.LogError("otherChessObject.ModelTransform is null");
            return;
        }

        modelTransform.LookAt(otherChessObject.ModelTransform.transform); // 实行朝向另一个对象
    }
}