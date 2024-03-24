public class WeiHeroObject : ChessObject
{
    protected override void Start()
    {
        base.Start();

        objectName = "Wei";
        HP += 100;
        Attack += 10;
    }

    protected override void SetStateBefore()
    {
        base.SetStateBefore();

        AddEquipment(new FanjiaEquipment(this)); // 添加装备
    }
}