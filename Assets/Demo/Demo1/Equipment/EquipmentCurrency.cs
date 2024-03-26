using UnityEngine;

public class EquipmentCurrency : PropAreaBaseClass
{
    [SerializeField]
    private string equipmentName; // 装备名称

    protected override void RegisterEvent()
    {
        base.RegisterEvent();

        OnMouseUpAction += OnMouseUpEvent;
    }

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
                hit.collider.GetComponent<ChessObject>().AddEquipmentColumn(equipmentName, sprite);
                gameObject.SetActive(false);
                break; // 匹配到第一个后就停止
            }
        }
    }
}