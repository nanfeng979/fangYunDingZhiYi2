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

        SetState(new IdleState(this)); // 默认状态
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
            SetState(new AttackState(this));
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

        otherChessObject.BeAttacked(attack);
    }

    // 被攻击
    protected void BeAttacked(float attack)
    {
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

    // 载入装备属性
    public void LoadEquipmentProperties()
    {
        foreach (var equipment in equipmentList)
        {
            equipment.LoadProperties(this);
        }
    }

    #endregion 装备区
}