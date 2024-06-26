using UnityEngine;

public class EquipmentCurrency : PropAreaBaseClass
{
    [SerializeField]
    private string equipmentName; // 装备名称
    [SerializeField] int equipmentID; // 装备ID

    protected override void RegisterEvent()
    {
        base.RegisterEvent();

        OnMouseUpAction += OnMouseUpEvent;
    }

    protected override void TempInit()
    {
        base.TempInit();

        objectName = equipmentName;
        objectID = equipmentID;
    }

    /// <summary>
    /// 道具对象在鼠标释放时的行为
    /// </summary>
    private void OnMouseUpEvent()
    {
        // 当鼠标释放时进行射线检测
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        // 对射线检测到的所有碰撞进行遍历
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<ChessObject>())
            {
                // 如果道具的所属不是当前玩家，则不进行操作
                if (hit.collider.GetComponent<ChessObject>().BelongTo != belongTo)
                {
                    continue;
                }

                hit.collider.GetComponent<ChessObject>().AddEquipmentColumn(equipmentName, sprite, this);
                break; // 匹配到第一个后就停止
            }
        }

        // 将道具位置重置到原始位置
        ResetPosition();
    }
}