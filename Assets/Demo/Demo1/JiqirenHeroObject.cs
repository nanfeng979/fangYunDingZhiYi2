public class JiqirenHeroObject : ChessObject
{
    protected override void Start()
    {
        base.Start();

        objectName = "Jiqiren";
        HP += 50;
        Attack += 10;
        AttackInterval = 1.5f;
    }

    protected override void SetStateBefore()
    {
        base.SetStateBefore();

        AddEquipment(new FanjiaEquipment(this)); // 添加装备
    }
}