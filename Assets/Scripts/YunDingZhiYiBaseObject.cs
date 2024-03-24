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

    protected virtual void Start() { }

    protected virtual void Update() { }
}