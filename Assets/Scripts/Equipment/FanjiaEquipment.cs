public class FanjiaEquipment : EquipmentBaseClass
{
    public FanjiaEquipment(ChessObject chessObject) : base(chessObject) { }

    protected override void DefineProperty()
    {
        propertyName = "Fanjia";
    }

    protected override void DefineEvent()
    {
        // 注册载入事件
        LoadEquipmentDelegate += AddHP;

        // 注册主动事件
        // ExecuteEquipmentDelegate += ActiveEvent;

        // 注册被攻击事件
        chessObject.BeAttackedDelegate += BounceDamage;
    }

    #region 生命周期
    // 执行装备
    public override void ExecuteEvent(ChessObject chessObject)
    {
        base.ExecuteEvent(chessObject);
    }

    #endregion 生命周期

    #region 功能函数区
    // 增加血量
    protected void AddHP(ChessObject chessObject)
    {
        chessObject.HP += 300;
    }

    // 被动事件测试
    public void BounceDamage(ChessObject attackChessObject, float damageValue)
    {
        attackChessObject.HP -= damageValue * 0.1f;
    }

    #endregion 功能函数区
}