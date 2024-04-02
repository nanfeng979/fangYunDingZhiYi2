using UnityEngine;

// Desc: 棋子Animation动画相关部分
public partial class ChessObject
{
    [SerializeField]
    protected Animator animator; // 动画控制器
    protected void PlayAnimation(string animationName)
    {
        if (animator == null)
        {
            return;
        }

        animator.Play(animationName);
    }
    
    /// <summary>
    /// 普通攻击动画
    /// </summary>
    private void NormalAttackAnimation(ChessObject beAttackedChessObject)
    {
        // if (animator == null)
        // {
        //     Debug.LogError("animator is null");
        //     return;
        // }
        
        // animator.speed = attackSpeed; // 设置动画播放速度

        // animator.SetTrigger("isAttack"); // 播放攻击动画

        // LookAtTo(beAttackedChessObject); // 朝向被攻击对象

        // 广播
        string guangboContent = belongTo + ":"; // 玩家所属
        guangboContent += objectID + ":"; // 道具ID
        guangboContent += objectType + ":"; // 道具类型
        guangboContent += "NormalAttackAnimation" + ":"; // 使用道具
        guangboContent += beAttackedChessObject.objectID.ToString() + ":"; // 被攻击对象ID
        guangboContent += attackSpeed.ToString(); // 攻击速度

        Guangbo.Instance._SendMessage(guangboContent); // 广播

        Debug.Log("guangboContent : " + guangboContent);
    }

    public void NormalAttackAnimation_Network(string beAttackedChessObjectID_str, string attackSpeed_str)
    {
        if (animator == null)
        {
            Debug.LogError("animator is null");
            return;
        }

        float attackSpeed = float.Parse(attackSpeed_str); // 攻击速度

        ChessObject beAttackedChessObject = IDToGameObjectMap.Instance.GetObject(int.Parse(beAttackedChessObjectID_str)).GetComponent<ChessObject>();
        if (beAttackedChessObject == null)
        {
            Debug.LogError("beAttackedChessObject is null");
            return;
        }
        
        animator.speed = attackSpeed; // 设置动画播放速度
        if (objectName == "Wei")
        {
            Debug.Log("attackSpeed : " + attackSpeed);
        }

        animator.SetTrigger("isAttack"); // 播放攻击动画

        LookAtTo(beAttackedChessObject); // 朝向被攻击对象
    }
}