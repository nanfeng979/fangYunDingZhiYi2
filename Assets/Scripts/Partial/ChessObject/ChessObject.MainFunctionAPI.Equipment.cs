// Desc: 棋子主要功能API，装备相关
using UnityEngine;

public partial class ChessObject
{
    /// <summary>
    /// 添加装备
    /// </summary>
    /// <param name="equipment"></param>
    protected void AddEquipment(EquipmentBaseClass equipment)
    {
        equipment.LoadEvent(this);
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

    /// <summary>
    /// 装备栏添加装备
    /// </summary>
    /// <param name="equipmentName"></param>
    /// <param name="sprite"></param>
    /// <param name="prop"></param>
    public void AddEquipmentColumn(string equipmentName, Sprite sprite, PropAreaBaseClass prop)
    {
        if (equipmentColumn == null)
        {
            prop.UnUseProp();
            Debug.LogError("equipmentColumn is null");
            return;
        }

        if (equipmentColumn == null)
        {
            prop.UnUseProp();
            Debug.LogError("equipmentColumn is null");
            return;
        }

        if (equipmentList.Count >= 3)
        {
            prop.UnUseProp();
            Debug.LogError("equipmentList.Count >= 3");
            return;
        }

        // 添加装备
        int equipmentColumnIndex = equipmentList.Count + 1;
        GameObject equipment = equipmentColumn.transform.Find("Equipment" + equipmentColumnIndex)?.gameObject;
        if (equipment == null)
        {
            prop.UnUseProp();
            return;
        }
        equipment.GetComponent<UnityEngine.UI.Image>().sprite = sprite;

        // 将equipmentName实例化为装备对象
        EquipmentBaseClass equipmentSerializableClass = Y9g.Utils.InstanceClassByString<EquipmentBaseClass>(equipmentName, new object[] { this });
        if (equipmentSerializableClass == null)
        {
            prop.UnUseProp();
            Debug.LogError("equipmentBaseClass is null");
            return;
        }

        AddEquipment(equipmentSerializableClass);
        prop.UseProp();
        // 广播出去
        string guangboContent = belongTo + ":"; // 所属
        guangboContent += objectName + ":"; // 棋子对象
        guangboContent += "AddEquipmentColumn:"; // 添加装备栏
        guangboContent += equipmentColumnIndex + ":"; // 装备栏索引
        guangboContent += equipmentName + ":"; // 装备名称

        Guangbo.Instance._SendMessage(guangboContent);
    }

    /// <summary>
    /// 添加装备栏，用途只有展示
    /// </summary>
    /// <param name="equipmentColumnIndex"></param>
    /// <param name="equipmentName"></param>
    public void AddEquipmentColumnOnlyShow(int equipmentColumnIndex, string equipmentName)
    {
        if (equipmentColumn == null)
        {
            return;
        }

        // 添加装备
        GameObject equipment = equipmentColumn.transform.Find("Equipment" + equipmentColumnIndex)?.gameObject;
        if (equipment == null)
        {
            return;
        }
        
        equipment.GetComponent<UnityEngine.UI.Image>().sprite = EquipmentSpriteMap.Instance.GetEquipmentSprite(equipmentName);
    }
}