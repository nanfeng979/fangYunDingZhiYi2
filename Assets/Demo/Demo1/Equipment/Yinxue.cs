using UnityEngine;

public class Yinxue : EquipmentBaseClass
{
    public Yinxue(ChessObject chessObject) : base(chessObject) { }

    #region 生命周期
    // 载入装备
    public override void LoadEvent(ChessObject chessObject)
    {
        objectName = "Yinxue";
        // 注册载入事件
        // LoadEquipmentDelegate += AddHP;

        // 注册主动事件
        // ExecuteEquipmentDelegate += ActiveEvent;

        // 注册角色攻击事件
        chessObject.AttackDelegate += AttackEvent;

        // 注册被攻击事件
        // chessObject.BeAttackedDelegate += BounceDamage;

        base.LoadEvent(chessObject);
    }

    // 执行装备
    public override void ExecuteEvent(ChessObject chessObject)
    {
        base.ExecuteEvent(chessObject);
    }

    #endregion 生命周期

    #region 功能函数区
    protected void AttackEvent(ChessObject attackChessObject, float damageValue)
    {
        attackChessObject.HP += damageValue * 0.1f;
    }

    #endregion 功能函数区
}