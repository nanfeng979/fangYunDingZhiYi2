using UnityEngine;

public class YunDingZhiYiBaseObject : MonoBehaviour
{
    protected string objectName; // 对象名称
    public string ObjectName
    {
        get
        {
            return objectName;
        }
        set
        {
            objectName = value;
        }
    }
    protected int objectID; // 对象ID
    public int ObjectID
    {
        get
        {
            return objectID;
        }
        set
        {
            objectID = value;
        }
    }
    protected YunDingZhiYiBaseObjectType objectType; // 对象类型
    public YunDingZhiYiBaseObjectType ObjectType
    {
        get
        {
            return objectType;
        }
        set
        {
            objectType = value;
        }
    }

    [SerializeField]
    protected string belongTo; // 对象所属
    public string BelongTo
    {
        get
        {
            return belongTo;
        }
        set
        {
            belongTo = value;
        }
    }

    protected virtual void Start() {
        TempInit();

        RegisterEvent();
        RegisterIDGameObject();
    }

    protected virtual void Update() { }

    protected virtual void RegisterEvent() { }

    protected virtual void TempInit() { }

    /// <summary>
    /// 注册ID与GameObject的对应关系
    /// </summary>
    protected void RegisterIDGameObject()
    {
        IDToGameObjectMap.Instance.RegisterObject(objectID, gameObject);
    }
}