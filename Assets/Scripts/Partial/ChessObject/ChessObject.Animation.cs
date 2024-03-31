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
        animator.SetTrigger("isAttack"); // 播放攻击动画

        LookAtTo(beAttackedChessObject); // 朝向被攻击对象
    }
}