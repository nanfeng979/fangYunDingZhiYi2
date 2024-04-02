using UnityEngine;

public class WeiHeroObject : ChessObject
{
    protected override void TempInit()
    {
        base.TempInit();

        objectID = 56;
        objectName = "Wei";
        HP += 40000;
        Attack += 10;
        MaxHp = HP; 
    }
}