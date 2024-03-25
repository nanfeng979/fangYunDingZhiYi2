public class WeiHeroObject : ChessObject
{
    protected override void Start()
    {
        base.Start();

        objectName = "Wei";
        HP += 100;
        Attack += 10;
        MaxHp = HP;
    }

    protected override void SetStateBefore()
    {
        base.SetStateBefore();
    }
}