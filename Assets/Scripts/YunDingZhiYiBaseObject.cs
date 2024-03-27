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

    protected virtual void Start() { RegisterEvent(); }

    protected virtual void Update() { }

    protected virtual void RegisterEvent() { }
}