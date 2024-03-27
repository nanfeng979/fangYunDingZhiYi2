using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Y9g;

public class EquipmentSpriteMap : Singleton<EquipmentSpriteMap>
{
    [SerializeField]
    private List<EquipmentSprite> equipmentSprites;

    /// <summary>
    /// 通过装备名获取装备图片
    /// </summary>
    /// <param name="equipmentName"></param>
    /// <returns></returns>
    public Sprite GetEquipmentSprite(string equipmentName)
    {
        foreach (var equipmentSprite in equipmentSprites)
        {
            if (equipmentSprite.equipmentName == equipmentName)
            {
                return equipmentSprite.equipmentSprite;
            }
        }
        return null;
    }
}

[System.Serializable]
public struct EquipmentSprite
{
    public string equipmentName;
    public Sprite equipmentSprite;
}