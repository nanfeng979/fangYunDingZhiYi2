public class Fanqugong : EquipmentBaseClass
{
    public Fanqugong(ChessObject chessObject) : base(chessObject) { }

    protected override void DefineProperty()
    {
        propertyName = "Fanqugong";
        isBaseEquipment = true;
        idOfBaseEquipment = 3;
    }

    protected override void DefineEvent()
    {
        // 登记装备戴上时的事件
        OnWear += AddAttackSpeed;

        // 登记装备卸下时的事件
        OnUnWear += RemoveAttackSpeed;
    }

    #region 生命周期
    // 增加攻速
    private void AddAttackSpeed(ChessObject chessObject)
    {
        chessObject.AttackSpeed += 0.5f;
    }

    // 减少攻速
    private void RemoveAttackSpeed(ChessObject chessObject)
    {
        chessObject.AttackSpeed -= 0.5f;
    }

    #endregion 生命周期

    #region 功能函数区

    #endregion 功能函数区
}