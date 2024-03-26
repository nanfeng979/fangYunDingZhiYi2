public abstract class EquipmentBaseClass
{
    protected string propertyName = "默认装备"; // 装备名称
    protected ChessObject chessObject; // 拥有者

    public EquipmentBaseClass(ChessObject chessObject)
    {
        this.chessObject = chessObject;
        
        DefineProperty();
        DefineEvent();
    }

    // 子类的属性和事件的自定义函数
    // 一般通过配置文件读取
    protected abstract void DefineProperty(); // 强制子类定义属性
    protected abstract void DefineEvent(); // 强制子类定义事件

    // 委托
    public delegate void EquipmentDelegate(ChessObject chessObject);

    // 载入装备时的委托
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