using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessObject : YunDingZhiYiBaseObject
{
    protected Vector2Int vector2IntIndex; // 位置
    public Vector2Int Vector2IntIndex
    {
        get
        {
            return vector2IntIndex;
        }
        set
        {
            vector2IntIndex = value;
        }
    }

    protected float hp; // 生命值
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }
    protected float maxHp; // 最大生命值
    public float MaxHp
    {
        get
        {
            return maxHp;
        }
        set
        {
            maxHp = value;
        }
    }

    protected float attack; // 攻击力
    public float Attack
    {
        get
        {
            return attack;
        }
        set
        {
            attack = value;
        }
    }
    protected float attackInterval; // 攻击间隔
    public float AttackInterval
    {
        get
        {
            return attackInterval;
        }
        set
        {
            attackInterval = value;
        }
    }

    protected override void Start()
    {
        base.Start();

        SetStateBefore(); // 设置状态前

        SetState(new NoFightState(this)); // 默认状态

        LoadUI(); // 加载UI
    }

    protected override void Update()
    {
        base.Update();

        if (currentState != null)
        {
            currentState.Execute(); // 执行当前状态
        }

        // 测试
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 进入攻击状态
            SetState(new AttackState(this));
        }

        // 战斗时
        if (IsFight)
        {
            // 执行装备主动事件
            foreach (var equipment in equipmentList)
            {
                equipment.ExecuteEvent(this);
            }

            // 更新血条
            UpdateHealthBar();
        }
    }

    /// <summary>
    /// 设置状态前
    /// </summary>
    protected virtual void SetStateBefore() { }

    /// <summary>
    /// 获取周围随机的一个棋子对象
    /// </summary>
    /// <returns></returns>
    public ChessObject GetSurroundingChessObject()
    {
        return ChessBoardManager.Instance.GetChessObjectBySurroundingChessObjectWithRandom(vector2IntIndex);
    }

    /// <summary>
    /// 普通攻击 // 逻辑层面
    /// </summary>
    /// <param name="beAttackedChessObject"></param>
    private void NormalAttackLogic(ChessObject beAttackedChessObject)
    {
        OnNormalAttack?.Invoke(this, attack); // 执行攻击事件
        beAttackedChessObject.BeNormalAttackedFun(this, attack); // 被攻击对象受到攻击
    }

    /// <summary>
    /// 被普通攻击 // 逻辑层面
    /// </summary>
    /// <param name="damageValue"></param>
    /// <param name="attackChessObject"></param>
    public void BeNormalAttackedLogic(ChessObject attackChessObject, float damageValue)
    {
        OnBeNormalAttacked?.Invoke(attackChessObject, damageValue); // 执行被攻击事件

        ReduceHpFun(damageValue); // 扣除生命值
    }

    // 死亡
    public void Die()
    {
        SetState(new DeadState(this));
    }

    #region 状态区
    protected IState currentState; // 当前状态

    protected bool isDead; // 是否死亡
    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
        }
    }
    protected bool isFight; // 是否战斗
    public bool IsFight
    {
        get
        {
            return isFight;
        }
        set
        {
            isFight = value;
        }
    }

    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="state"></param>
    public void SetState(IState state)
    {
        if (currentState != null)
        {
            currentState.Exit(); // 退出旧状态
        }

        currentState = state; // 设置新状态
        currentState.Enter(); // 进入新状态
    }

    /// <summary>
    /// 获取当前状态
    /// </summary>
    /// <returns></returns>
    public IState GetCurrentState()
    {
        return currentState;
    }

    /// <summary>
    /// 判断当前状态是否是某个状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool IsCurrentState(IState state)
    {
        return currentState == state;
    }

    #endregion 状态区

    #region 装备区
    protected List<EquipmentBaseClass> equipmentList = new List<EquipmentBaseClass>(); // 装备列表

    /// <summary>
    /// 添加装备
    /// </summary>
    /// <param name="equipment"></param>
    protected void AddEquipment(EquipmentBaseClass equipment)
    {
        equipment.LoadEvent(this);
        equipmentList.Add(equipment);
    }

    /// <summary>
    /// 移除装备
    /// </summary>
    /// <param name="equipment"></param>
    protected void RemoveEquipment(EquipmentBaseClass equipment)
    {
        equipmentList.Remove(equipment);
    }

    #endregion 装备区

    #region 事件区
    // 委托
    public Action<ChessObject, float> OnNormalAttack; // 攻击事件
    public Action<ChessObject, float> OnBeNormalAttacked; // 被攻击事件

    #endregion 事件区

    #region 展示区
    // 加载UI函数
    private void LoadUI()
    {
        LoadHealthBar(); // 加载血条
        LoadEquipmentColumn(); // 加载装备栏
        LoadReduceHpText(); // 加载扣除的血量UI
    }

    // 血条
    protected GameObject healthBar; // 血条
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

    // 装备
    protected GameObject equipmentColumn; // 装备栏

    protected void LoadEquipmentColumn()
    {
        Transform canvas = transform.Find("Canvas");
        if (canvas != null)
        {
            equipmentColumn = canvas.Find("EquipmentColumn")?.gameObject;
        }
    }

    // 装备栏添加装备
    public void AddEquipmentColumn(string equipmentName, Sprite sprite, PropAreaBaseClass prop)
    {
        if (equipmentColumn == null)
        {
            prop.UnUseProp();
            Debug.LogError("equipmentColumn is null");
            return;
        }

        if (equipmentColumn == null)
        {
            prop.UnUseProp();
            Debug.LogError("equipmentColumn is null");
            return;
        }

        if (equipmentList.Count >= 3)
        {
            prop.UnUseProp();
            Debug.LogError("equipmentList.Count >= 3");
            return;
        }

        // 添加装备
        int equipmentColumnIndex = equipmentList.Count + 1;
        GameObject equipment = equipmentColumn.transform.Find("Equipment" + equipmentColumnIndex)?.gameObject;
        if (equipment == null)
        {
            prop.UnUseProp();
            return;
        }
        equipment.GetComponent<UnityEngine.UI.Image>().sprite = sprite;

        // 将equipmentName实例化为装备对象
        EquipmentBaseClass equipmentSerializableClass = Y9g.Utils.InstanceClassByString<EquipmentBaseClass>(equipmentName, new object[] { this });
        if (equipmentSerializableClass == null)
        {
            prop.UnUseProp();
            Debug.LogError("equipmentBaseClass is null");
            return;
        }

        AddEquipment(equipmentSerializableClass);
        prop.UseProp();
        // 广播出去
        string guangboContent = belongTo + ":"; // 所属
        guangboContent += objectName + ":"; // 棋子对象
        guangboContent += "AddEquipmentColumn:"; // 添加装备栏
        guangboContent += equipmentColumnIndex + ":"; // 装备栏索引
        guangboContent += equipmentName + ":"; // 装备名称

        Guangbo.Instance._SendMessage(guangboContent);
    }

    /// <summary>
    /// 添加装备栏，用途只有展示
    /// </summary>
    /// <param name="equipmentColumnIndex"></param>
    /// <param name="equipmentName"></param>
    public void AddEquipmentColumnOnlyShow(int equipmentColumnIndex, string equipmentName)
    {
        if (equipmentColumn == null)
        {
            return;
        }

        // 添加装备
        GameObject equipment = equipmentColumn.transform.Find("Equipment" + equipmentColumnIndex)?.gameObject;
        if (equipment == null)
        {
            return;
        }
        equipment.GetComponent<UnityEngine.UI.Image>().sprite = EquipmentSpriteMap.Instance.GetEquipmentSprite(equipmentName);
    }

    // 扣除的血量
    protected Text reduceHpText; // 扣除的血量
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

    #endregion 展示区

    #region 动作动画区
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

    #endregion 动作动画区

    #region 玩法区
    /// <summary>
    /// 普通攻击功能函数
    /// </summary>
    /// <param name="beAttackedChessObject"></param>
    public virtual void NormalAttackFun(ChessObject beAttackedChessObject)
    {
        // 当被攻击对象为空或者已经死亡时
        if (beAttackedChessObject.isDead || beAttackedChessObject == null)
        {
            return;
        }

        NormalAttackLogic(beAttackedChessObject); // 普通攻击逻辑
        NormalAttackAnimation(beAttackedChessObject); // 普通攻击动画
    }

    /// <summary>
    /// 恢复生命值功能函数
    /// </summary>
    /// <param name="selfObject"></param>
    /// <param name="restoreHpValue"></param>
    public virtual void RestoredHpFun(float restoreHpValue)
    {
        HP += restoreHpValue;
        Debug.LogError(objectName + " RestoredHpFun hp: " + hp);
    }

    /// <summary>
    /// 扣除生命值功能函数
    /// </summary>
    /// <param name="reduceHpValue"></param>
    protected virtual void ReduceHpFun(float reduceHpValue)
    {
        hp -= reduceHpValue;
        // Debug.LogError(objectName + " ReduceHpFun hp: " + hp);

        ShowReduceHpText(reduceHpValue, 0.5f); // 显示扣除的血量
    }

    // 显示扣除的血量
    public void ShowReduceHpText(float reduceHpValue, float showTime = 1f)
    {
        if (reduceHpText == null)
        {
            Debug.LogError("reduceHpText is null");
            return;
        }

        reduceHpText.text = "-" + reduceHpValue;
        reduceHpText.gameObject.SetActive(true);
        Invoke("HideReduceHpText", showTime);
    }

    // 隐藏扣除的血量
    public void HideReduceHpText()
    {
        reduceHpText.gameObject?.SetActive(false);
    }

    /// <summary>
    /// 被普通攻击功能函数
    /// </summary>
    /// <param name="attackChessObject"></param>
    /// <param name="beAttackedValue"></param>
    protected virtual void BeNormalAttackedFun(ChessObject attackChessObject, float beAttackedValue)
    {
        if (isDead)
        {
            return;
        }

        BeNormalAttackedLogic(attackChessObject, beAttackedValue); // 被普通攻击逻辑

        if (hp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 朝向另一个对象
    /// </summary>
    /// <param name="otherChessObject"></param>
    [SerializeField]
    protected Transform modelTransform; // 模型Transform
    public Transform ModelTransform
    {
        get
        {
            return modelTransform;
        }
    }
    private void LookAtTo(ChessObject otherChessObject)
    {
        if (modelTransform == null)
        {
            Debug.LogError("modelTransform is null");
            return;
        }

        if (otherChessObject.ModelTransform == null)
        {
            Debug.LogError("otherChessObject.ModelTransform is null");
            return;
        }

        modelTransform.LookAt(otherChessObject.ModelTransform.transform);
    }

    #endregion 玩法区

    #region 动画区
    /// <summary>
    /// 普通攻击动画
    /// </summary>
    private void NormalAttackAnimation(ChessObject beAttackedChessObject)
    {
        animator.SetTrigger("isAttack"); // 播放攻击动画

        LookAtTo(beAttackedChessObject); // 朝向被攻击对象
    }
    #endregion 动画区
}