public class Yangdao : EquipmentBaseClass
{
    public Yangdao(ChessObject chessObject) : base(chessObject) { }

    protected override void DefineProperty()
    {
        propertyName = "Yangdao";
        isBaseEquipment = true;
    }

    protected override void DefineEvent()
    {
        // 登记装备戴上时的事件
        OnWear += AddSpeddPower;

        // 登记装备卸下时的事件
        OnUnWear += RemoveSpeddPower;
    }

    #region 生命周期
    // 增加法强
    private void AddSpeddPower(ChessObject chessObject)
    {
        chessObject.SpellPower += 99f;
    }

    // 减少法强
    private void RemoveSpeddPower(ChessObject chessObject)
    {
        chessObject.SpellPower -= 99f;
    }

    #endregion 生命周期

    #region 功能函数区

    #endregion 功能函数区
}