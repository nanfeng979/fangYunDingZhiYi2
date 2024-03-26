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

    protected virtual void Start() { RegisterEvent(); }

    protected virtual void Update() { }

    protected virtual void RegisterEvent() { }
}