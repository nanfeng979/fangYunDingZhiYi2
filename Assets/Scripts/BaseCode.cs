using System;
using Y9g;

public class BaseCode : Singleton<BaseCode>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public struct IDToGameObject
{
    public int ID;
    public UnityEngine.GameObject gameObject;
}

public enum YunDingZhiYiBaseObjectType
{
    Chess,
    Prop
}

public enum LeftOrRight
{
    Left,
    Right
}

public enum HPUIType
{
    ReduceHP,
    RestoreHP
}