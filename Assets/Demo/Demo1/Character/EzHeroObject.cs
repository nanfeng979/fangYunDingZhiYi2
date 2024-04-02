public class EzHeroObject : ChessObject
{
    protected override void TempInit()
    {
        base.TempInit();

        objectName = "Ez";
        HP += 30000;
        Attack += 12;
        MaxHp = HP;
    }
}