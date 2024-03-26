public class Yinxue : EquipmentBaseClass
{
    public Yinxue(ChessObject chessObject) : base(chessObject) { }

    protected override void DefineProperty()
    {
        propertyName = "Yinxue";
    }

    protected override void DefineEvent()
    {
        // 注册角色攻击事件
        chessObject.AttackDelegate += AttackEvent;

        // 注册主动事件
        // ExecuteEquipmentDelegate += Test;
    }

    #region 生命周期
    // 执行装备
    public override void ExecuteEvent(ChessObject chessObject)
    {
        base.ExecuteEvent(chessObject);
    }

    #endregion 生命周期

    #region 功能函数区
    // 角色攻击事件
    protected void AttackEvent(ChessObject attackChessObject, float damageValue)
    {
        attackChessObject.HP += 100;
    }

    // 主动测试
    public void Test(ChessObject chessObject)
    {
        UnityEngine.Debug.Log("Yinxue Test");
    }

    #endregion 功能函数区
}