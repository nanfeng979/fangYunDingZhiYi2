public class EquipmentBaseClass
{
    protected string objectName; // 装备名称
    protected ChessObject chessObject; // 拥有者

    public EquipmentBaseClass(ChessObject chessObject)
    {
        this.chessObject = chessObject;
    }

    // 委托
    public delegate void EquipmentDelegate(ChessObject chessObject);

    // 刚装备上装备时的委托
    public EquipmentDelegate LoadEquipmentDelegate;
    // 战斗开始时的委托
    public EquipmentDelegate BattleStartDelegate;
    // 装备穿戴时的委托
    public EquipmentDelegate ExecuteEquipmentDelegate;
    // 装备卸下时的委托
    public EquipmentDelegate UnWearEquipmentDelegate;


    // 载入装备事件
    public virtual void LoadEvent(ChessObject chessObject)
    {
        LoadEquipmentDelegate?.Invoke(chessObject);
    }

    // 执行装备事件
    public virtual void ExecuteEvent(ChessObject chessObject)
    {
        ExecuteEquipmentDelegate?.Invoke(chessObject);
    }
}