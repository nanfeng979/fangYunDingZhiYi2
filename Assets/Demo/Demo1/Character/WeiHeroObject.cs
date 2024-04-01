using UnityEngine;

public class WeiHeroObject : ChessObject
{
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("AttackSpeed: " + AttackSpeed + " SpellPower: " + SpellPower);
        }
    }

    protected override void TempInit()
    {
        base.TempInit();

        objectID = 56;
        objectName = "Wei";
        HP += 100;
        Attack += 10;
        MaxHp = HP;
    }
}