public abstract class EquipmentBaseClass
{
    protected string propertyName = "默认装备"; // 装备名称
    protected ChessObject owner; // 拥有者
    protected bool isBaseEquipment = false; // 是否是基础装备
    public bool IsBaseEquipment { get { return isBaseEquipment; } }
    protected int idOfBaseEquipment = 0; // 基础装备的ID
    public int IdOfBaseEquipment { get { return idOfBaseEquipment; } }

    public EquipmentBaseClass(ChessObject chessObject)
    {
        owner = chessObject;
        
        DefineProperty();
        DefineEvent();
    }

    // 子类的属性和事件的自定义函数
    // 一般通过配置文件读取
    protected abstract void DefineProperty(); // 强制子类定义属性
    protected abstract void DefineEvent(); // 强制子类定义事件

    // 委托
    // 装备的生命周期
    public delegate void EquipmentLifeCycle(ChessObject chessObject); // 装备生命周期

    // 装备戴上时的委托
    public EquipmentLifeCycle OnWear;
    // 装备卸下时的委托
    public EquipmentLifeCycle OnUnWear;
    // 战斗开始时的委托
    public EquipmentLifeCycle OnBattleStart;
    // 战斗进行时的委托
    public EquipmentLifeCycle OnBattleUpdate;
    // 战斗结束时的委托
    public EquipmentLifeCycle OnBattleEnd;


    // 执行装备戴上时的事件
    public virtual void OnWearEvent(ChessObject chessObject)
    {
        OnWear?.Invoke(chessObject);
    }

    // 执行装备卸下时的事件
    public virtual void OnUnWearEvent(ChessObject chessObject)
    {
        OnUnWear?.Invoke(chessObject);
    }

    // 执行战斗进行时的事件
    public virtual void DoBattleUpdateEvent(ChessObject chessObject)
    {
        OnBattleUpdate?.Invoke(chessObject);
    }
}