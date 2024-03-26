using System;
using System.Collections.Generic;
using UnityEngine;

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

        LoadHealthBar(); // 载入血条
        LoadEquipmentColumn(); // 载入装备栏
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

    // 攻击
    public void AttackChessObject(ChessObject otherChessObject)
    {
        if (otherChessObject.isDead)
        {
            return;
        }

        AttackDelegate?.Invoke(this, attack); // 攻击事件
        otherChessObject.BeAttacked(attack, this);
    }

    // 被攻击
    public void BeAttacked(float attack, ChessObject attackChessObject)
    {
        BeAttackedDelegate?.Invoke(attackChessObject, attack); // 被攻击事件

        hp -= attack;

        Debug.LogError(objectName + " BeAttacked hp: " + hp);

        if (hp <= 0)
        {
            Die();
        }
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
    public Action<ChessObject, float> AttackDelegate; // 攻击事件
    public Action<ChessObject, float> BeAttackedDelegate; // 被攻击事件

    #endregion 事件区

    #region 展示区
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

        UnityEngine.UI.Image healthBarImage = healthBar.GetComponent<UnityEngine.UI.Image>();
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
    public void AddEquipmentColumn(string equipmentName, Sprite sprite)
    {
        if (equipmentColumn == null)
        {
            Debug.LogError("equipmentColumn is null");
            return;
        }

        if (equipmentColumn == null)
        {
            Debug.LogError("equipmentColumn is null");
            return;
        }

        // 添加装备
        GameObject equipment = equipmentColumn.transform.Find("Equipment1")?.gameObject;
        if (equipment == null)
        {
            return;
        }
        equipment.GetComponent<UnityEngine.UI.Image>().sprite = sprite;

        // 将equipmentName实例化为装备对象
        EquipmentBaseClass equipmentSerializableClass = (EquipmentBaseClass)Activator.CreateInstance(Type.GetType(equipmentName), new object[] { this });
        if (equipmentSerializableClass == null)
        {
            Debug.LogError("equipmentBaseClass is null");
            return;
        }
        AddEquipment(equipmentSerializableClass);
    }

    #endregion 展示区
}