using UnityEngine;

public class WeiHeroObject : ChessObject
{
    protected override void Start()
    {
        base.Start();

        objectName = "Wei";
        HP = 100;
        Attack = 10;

        Vector2Int aroundIndex = ChessBoardManager.Instance.GetVector2IntIndexBySurroundingChessObjectWithRandom(vector2IntIndex);
        Debug.Log("WeiHeroObject Start aroundIndex: " + aroundIndex);
    }
}