using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class ChessObject : YunDingZhiYiBaseObject, ChessObject_NetWork_Interface
{
    #region 属性区
    protected Vector2Int vector2IntIndex; // 格子位置
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
    protected float attackSpeed; // 攻击速度
    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }
        set
        {
            attackSpeed = value;
        }
    }
    protected float maxAttackSpeed; // 最大攻击速度
    public float MaxAttackSpeed
    {
        get
        {
            return maxAttackSpeed;
        }
        set
        {
            maxAttackSpeed = value;
        }
    }
    protected float spellPower; // 法强
    public float SpellPower
    {
        get
        {
            return spellPower;
        }
        set
        {
            spellPower = value;
        }
    }


    #endregion 属性区

    protected override void Start()
    {
        SetStateBefore(); // 设置状态前

        SetState(new NoFightState(this)); // 默认状态

        LoadUI(); // 加载UI

        base.Start();
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
            // 执行装备的战斗进行时事件
            foreach (var equipment in equipmentList)
            {
                equipment.DoBattleUpdateEvent(this);
            }

            // 更新血条
            // UpdateHealthBar();
        }
    }

    protected override void TempInit()
    {
        base.TempInit();

        objectType = YunDingZhiYiBaseObjectType.Chess;
        AttackSpeed = 1;
        MaxAttackSpeed = 2.5f;
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

    #endregion 状态区

    #region 数据区
    protected List<EquipmentBaseClass> equipmentList = new List<EquipmentBaseClass>(); // 装备列表

    #endregion 数据区

    #region 事件区
    public Action<ChessObject, float> OnNormalAttack; // 攻击事件
    public Action<ChessObject, float> OnBeNormalAttacked; // 被攻击事件

    #endregion 事件区

    #region UI区
    protected Transform canvas; // 画布
    protected GameObject healthBar; // 血条

    protected GameObject equipmentColumn; // 装备栏

    #endregion UI区

    [SerializeField]
    protected Transform modelTransform; // 模型Transform
    public Transform ModelTransform
    {
        get
        {
            return modelTransform;
        }
    }
}