using UnityEngine;

public class FanjiaEquipment : EquipmentBaseClass
{
    // 频率
    private float frequency = 1.0f;
    private float frequencyTimer = 0.0f;

    // 载入装备
    public override void LoadEvent(ChessObject chessObject)
    {
        objectName = "Fanjia";
        LoadEquipmentDelegate += AddHP;
        ExecuteEquipmentDelegate += Test;

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

    // 测试
    public void Test(ChessObject chessObject)
    {
        frequencyTimer += Time.deltaTime;
        if (frequencyTimer < frequency)
        {
            return;
        }
        frequencyTimer -= frequency;
        Debug.Log("<color=red>" + chessObject.ObjectName + " ExecuteEvent </color>");
    }
}