using UnityEngine;
using UnityEngine.UI;

// Desc: 棋子UI相关部分
public partial class ChessObject
{
    // 加载UI函数
    private void LoadUI()
    {
        LoadCanvas(); // 加载画布
        LoadHealthBar(); // 加载血条
        LoadEquipmentColumn(); // 加载装备栏
    }

    /// <summary>
    /// 载入画布
    /// </summary>
    protected void LoadCanvas()
    {
        canvas = transform.Find("Canvas");
        if (canvas == null)
        {
            Debug.LogError("Canvas is null");
        }
    }

    /// <summary>
    /// 载入血条
    /// </summary>
    protected void LoadHealthBar()
    {
        if (canvas != null)
        {
            Transform healthBarBackground = canvas.Find("HealthBarBackground");
            if (healthBarBackground != null)
            {
                healthBar = healthBarBackground.Find("HealthBar").gameObject;
            }
        }
    }
    /// <summary>
    /// 更新血条
    /// </summary>
    protected virtual void UpdateHealthBar()
    {
        if (healthBar == null)
        {
            return;
        }

        Image healthBarImage = healthBar.GetComponent<Image>();
        healthBarImage.fillAmount = hp / maxHp;
    }

    /// <summary>
    /// 载入装备栏
    /// </summary>
    protected void LoadEquipmentColumn()
    {
        if (canvas != null)
        {
            equipmentColumn = canvas.Find("EquipmentColumn")?.gameObject;
        }
    }

    /// <summary>
    /// 显示减少的血量
    /// </summary>
    /// <param name="reduceHpValue"></param>
    protected void ShowReduceHP(float reduceHpValue)
    {
        if (canvas != null)
        {
            Transform loseHP = canvas.Find("ShowHPEffect");
            if (loseHP != null)
            {
                GameObject obj = HPObjectPool.Instance.GetObject(loseHP);
                obj.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                HPObjectPool.Instance.LoseHp(obj, reduceHpValue);
            }
        }
    }

    /// <summary>
    /// 显示恢复的血量
    /// </summary>
    /// <param name="restoreHpValue"></param>
    /// <param name="showTime"></param>
    protected void ShowRestoreHp(float restoreHpValue)
    {
        if (canvas != null)
        {
            Transform restoreHP = canvas.Find("ShowHPEffect");
            if (restoreHP != null)
            {
                GameObject obj = HPObjectPool.Instance.GetObject(restoreHP);
                obj.GetComponent<RectTransform>().localPosition = new Vector3(40f, 0, 0);
                HPObjectPool.Instance.RecoverHp(obj, restoreHpValue);
            }
        }
    }
}