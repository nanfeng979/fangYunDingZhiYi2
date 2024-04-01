public class Fanqugong : EquipmentBaseClass
{
    public Fanqugong(ChessObject chessObject) : base(chessObject) { }

    protected override void DefineProperty()
    {
        propertyName = "Fanqugong";
        isBaseEquipment = true;
    }

    protected override void DefineEvent()
    {
        // 登记装备戴上时的事件
        OnWear += AddAttackSpeed;
    }

    #region 生命周期
    private void AddAttackSpeed(ChessObject chessObject)
    {
        chessObject.AttackSpeed += 0.5f;
    }

    #endregion 生命周期

    #region 功能函数区

    #endregion 功能函数区
}