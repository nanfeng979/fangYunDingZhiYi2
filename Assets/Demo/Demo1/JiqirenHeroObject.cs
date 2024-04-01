public class JiqirenHeroObject : ChessObject
{
    protected override void Start()
    {
        base.Start();

        objectName = "Jiqiren";
        HP += 50;
        Attack += 10;
        AttackSpeed = 1.5f;
        MaxHp = HP;
    }
}