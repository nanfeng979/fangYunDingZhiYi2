public class Yinxue : EquipmentBaseClass
{
    public Yinxue(ChessObject chessObject) : base(chessObject) { }

    protected override void DefineProperty()
    {
        propertyName = "Yinxue";
    }

    protected override void DefineEvent()
    {
        // 登记拥有者普通攻击时的事件
        owner.OnNormalAttack += NormalAttack_RestoreHP;
    }

    #region 生命周期
    

    #endregion 生命周期

    #region 功能函数区
    // 角色攻击事件
    protected void NormalAttack_RestoreHP(ChessObject selfObject, float selfNormalAttackValue)
    {
        selfObject.RestoredHpFun(selfNormalAttackValue * 0.1f);
    }

    // 主动测试
    public void Test(ChessObject chessObject)
    {
        UnityEngine.Debug.Log("Yinxue Test");
    }

    #endregion 功能函数区
}