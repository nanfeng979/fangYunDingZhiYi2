public class FanjiaEquipment : EquipmentBaseClass
{
    public FanjiaEquipment(ChessObject chessObject) : base(chessObject) { }

    protected override void DefineProperty()
    {
        propertyName = "Fanjia";
    }

    protected override void DefineEvent()
    {
        // 登记装备戴上时的事件
        OnWear += AddHP;

        // 登记拥有者被攻击时的事件
        owner.OnBeNormalAttacked += BounceDamage;
    }

    #region 生命周期

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

    // 主动测试
    public void Test(ChessObject chessObject)
    {
        UnityEngine.Debug.Log("Fanjia Test");
    }

    #endregion 功能函数区
}