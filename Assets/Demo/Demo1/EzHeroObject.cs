public class EzHeroObject : ChessObject
{
    protected override void TempInit()
    {
        base.TempInit();

        objectName = "Ez";
        HP += 300;
        Attack += 12;
        MaxHp = HP;
    }
}