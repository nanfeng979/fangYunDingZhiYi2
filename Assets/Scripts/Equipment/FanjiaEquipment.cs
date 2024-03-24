using UnityEngine;

public class FanjiaEquipment : EquipmentBaseClass
{
    public FanjiaEquipment(ChessObject chessObject) : base(chessObject) { }

    // 频率
    private float frequency = 1.0f;
    private float frequencyTimer = 0.0f;

    // 载入装备
    public override void LoadEvent(ChessObject chessObject)
    {
        objectName = "Fanjia";
        LoadEquipmentDelegate += AddHP;

        // 注册被攻击事件
        chessObject.BeAttackedDelegate += BounceDamage;

        base.LoadEvent(chessObject);
    }

    // 执行装备
    public override void ExecuteEvent(ChessObject chessObject)
    {
        base.ExecuteEvent(chessObject);
    }

    // 增加血量
    protected void AddHP(ChessObject chessObject)
    {
        chessObject.HP += 100;
    }

    // 被动事件测试
    public void BounceDamage(ChessObject attackChessObject, float damageValue)
    {
        attackChessObject.HP -= damageValue * 0.1f;
    }
}