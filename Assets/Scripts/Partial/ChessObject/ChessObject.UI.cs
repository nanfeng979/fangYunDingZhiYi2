using UnityEngine;
using UnityEngine.UI;

// Desc: 棋子UI相关部分
public partial class ChessObject
{
    // 加载UI函数
    private void LoadUI()
    {
        LoadHealthBar(); // 加载血条
        LoadEquipmentColumn(); // 加载装备栏
        LoadReduceHpText(); // 加载扣除的血量UI
    }

    /// <summary>
    /// 载入血条
    /// </summary>
    protected void LoadHealthBar()
    {
        Transform canvas = transform.Find("Canvas");
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
        Transform canvas = transform.Find("Canvas");
        if (canvas != null)
        {
            equipmentColumn = canvas.Find("EquipmentColumn")?.gameObject;
        }
    }

    /// <summary>
    /// 载入扣除的血量UI
    /// </summary>
    protected void LoadReduceHpText()
    {
        Transform canvas = transform.Find("Canvas");
        if (canvas != null)
        {
            reduceHpText = canvas.Find("ReduceHp")?.GetComponent<Text>();
            if (reduceHpText == null)
            {
                Debug.LogError("reduceHpText is null at just start");
            }
            else
            {
                reduceHpText.gameObject.SetActive(false); // 默认隐藏
            }
        }
    }

    /// <summary>
    /// 显示扣除的血量
    /// </summary>
    /// <param name="reduceHpValue"></param>
    /// <param name="showTime"></param>
    public void ShowReduceHpText(float reduceHpValue, float showTime = 1f)
    {
        if (reduceHpText == null)
        {
            Debug.LogError("reduceHpText is null");
            return;
        }

        reduceHpText.text = "-" + reduceHpValue;
        reduceHpText.color = Color.red;
        reduceHpText.gameObject.SetActive(true);
        Invoke(nameof(HideReduceHpText), showTime);
    }

    /// <summary>
    /// 隐藏扣除的血量
    /// </summary>
    public void HideReduceHpText()
    {
        reduceHpText.gameObject?.SetActive(false);
    }

    protected void ShowLoseHP(float damage)
    {
        Transform canvas = transform.Find("Canvas");
        if (canvas != null)
        {
            Transform loseHP = canvas.Find("ShowLoseHP");
            if (loseHP != null)
            {
                HPObjectPool.Instance.GetObject(loseHP, damage);
            }
        }
        
    }
}