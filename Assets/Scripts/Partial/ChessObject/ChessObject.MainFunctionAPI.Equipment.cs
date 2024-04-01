// Desc: 棋子主要功能API，装备相关
using UnityEngine;

public partial class ChessObject
{
    private int indexOfBaseEquipment = -1; // 基础装备的索引

    /// <summary>
    /// 添加装备
    /// </summary>
    /// <param name="equipment"></param>
    private void AddEquipment(EquipmentBaseClass equipment)
    {
        equipment.OnWearEvent(this); // 执行装备戴上时的事件
        equipmentList.Add(equipment); // 在装备列表中添加装备
    }

    /// <summary>
    /// 移除装备
    /// </summary>
    /// <param name="equipment"></param>
    private void RemoveEquipment(EquipmentBaseClass equipment)
    {
        equipment.OnUnWearEvent(this); // 执行装备卸下时的事件
        equipmentList.Remove(equipment); // 在装备列表中移除装备
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
            prop.CancelUseProp(); // 取消使用道具
            Debug.LogError("equipmentColumn is null");
            return;
        }

        if (equipmentColumn == null)
        {
            prop.CancelUseProp(); // 取消使用道具
            Debug.LogError("equipmentColumn is null");
            return;
        }

        if (equipmentList.Count >= 3)
        {
            prop.CancelUseProp(); // 取消使用道具
            Debug.LogError("equipmentList.Count >= 3");
            return;
        }

        // 将equipmentName实例化为装备对象
        EquipmentBaseClass equipmentSerializableClass = Y9g.Utils.InstanceClassByString<EquipmentBaseClass>(equipmentName, new object[] { this });
        if (equipmentSerializableClass == null)
        {
            prop.CancelUseProp(); // 取消使用道具
            Debug.LogError("equipmentBaseClass is null");
            return;
        }

        // 判断是否有是础装备
        bool isBaseEquipment = equipmentSerializableClass.IsBaseEquipment;

        // 添加装备
        // 获取装备栏索引
        int equipmentColumnIndex;
        // 是基础装备，且已经有基础装备，合成装备
        if (isBaseEquipment && indexOfBaseEquipment != -1)
        {
            Debug.Log(indexOfBaseEquipment);
            equipmentColumnIndex = indexOfBaseEquipment;
            // 移除基础装备
            RemoveEquipment(equipmentList[indexOfBaseEquipment - 1]);
        }
        // 追加装备
        else
        {
            equipmentColumnIndex = equipmentList.Count + 1;
        }

        // 寻找对应的装备栏
        GameObject equipment = equipmentColumn.transform.Find("Equipment" + equipmentColumnIndex)?.gameObject;
        if (equipment == null)
        {
            prop.CancelUseProp(); // 取消使用道具
            return;
        }

        // 替换装备栏图片
        equipment.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        // 是基础装备，且已经有基础装备
        if (isBaseEquipment && indexOfBaseEquipment != -1)
        {
            indexOfBaseEquipment = -1;
        }
        // 是基础装备，且还没有基础装备
        else if (isBaseEquipment && indexOfBaseEquipment == -1)
        {
            indexOfBaseEquipment = equipmentColumnIndex;
        }

        // 添加装备
        AddEquipment(equipmentSerializableClass);

        prop.UseProp(); // 使用道具
        // 广播出去
        string guangboContent = belongTo + ":"; // 所属
        guangboContent += objectName + ":"; // 棋子对象
        guangboContent += "AddEquipmentColumn:"; // 添加装备栏
        guangboContent += equipmentColumnIndex + ":"; // 装备栏索引
        guangboContent += equipmentName + ":"; // 装备名称

        Guangbo.Instance._SendMessage(guangboContent); // 广播
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