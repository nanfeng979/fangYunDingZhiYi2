using System;
using UnityEngine;

public class PropAreaBaseClass : YunDingZhiYiBaseObject
{
    private Vector3 mOffset; // 鼠标点击位置和物体位置的偏移量
    private float mZCoord; // 道具的 z 坐标
    [SerializeField]
    protected Sprite sprite; // 道具的图片

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        // 将游戏对象的世界坐标转换成屏幕坐标

        mOffset = gameObject.transform.position - GetMouseWorldPos();
        // 计算物体位置和鼠标点击位置的偏移量
    }

    private Vector3 GetMouseWorldPos()
    {
        // 把鼠标的屏幕坐标转换成世界坐标
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
        // 更新物体的位置到鼠标的位置（考虑偏移量）
    }

    // 道具对象在鼠标释放时的行为
    protected Action OnMouseUpAction;
    void OnMouseUp()
    {
        OnMouseUpAction?.Invoke();
    }
}