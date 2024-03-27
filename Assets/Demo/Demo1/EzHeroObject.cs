public class EzHeroObject : ChessObject
{
    protected override void Start()
    {
        base.Start();

        objectName = "Ez";
        HP += 100;
        Attack += 10;
        MaxHp = HP;
    }
}