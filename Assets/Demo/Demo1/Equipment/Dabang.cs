public class Dabang : EquipmentBaseClass
{
    public Dabang(ChessObject chessObject) : base(chessObject) { }

    protected override void DefineProperty()
    {
        propertyName = "Dabang";
        isBaseEquipment = true;
    }

    protected override void DefineEvent()
    {
        // 登记装备戴上时的事件
        OnWear += AddSpeddPower;
    }

    #region 生命周期
    private void AddSpeddPower(ChessObject chessObject)
    {
        chessObject.SpellPower += 10f;
    }

    #endregion 生命周期

    #region 功能函数区

    #endregion 功能函数区
}