public class JiqirenHeroObject : ChessObject
{
    protected override void Start()
    {
        base.Start();

        objectName = "Jiqiren";
        HP += 50;
        Attack += 10;
    }

    protected override void SetStateBefore()
    {
        base.SetStateBefore();

        AddEquipment(new FanjiaEquipment()); // 添加装备
    }
}