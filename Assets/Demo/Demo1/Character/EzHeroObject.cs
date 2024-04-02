public class EzHeroObject : ChessObject
{
    protected override void TempInit()
    {
        base.TempInit();

        objectName = "Ez";
        objectID = 77;
        HP += 500;
        Attack += 12;
        MaxHp = HP;
    }
}