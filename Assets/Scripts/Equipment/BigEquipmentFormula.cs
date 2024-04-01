using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Y9g;

/// <summary>
/// 大装备合成公式
/// </summary>

public class BigEquipmentFormula : Singleton<BigEquipmentFormula>
{
    private string[,] bigEquipmentFormula = {
        {"Null",        "Dajian",   "Dabang",   "Fanqugong",    "Yanlei",   "Suozijia", "Doupeng",  "Yaodao",   "Quantao",  "Jincc"},
        {"Dajian",      "xx",       "xx",       "xx",           "xx",       "xx",       "xx",       "xx",       "xx",       "xx"},
        {"Dabang",      "xx",       "xx",       "Yangdao",      "xx",       "xx",       "xx",       "xx",       "xx",       "xx"},
        {"Fanqugong",   "xx",       "Yangdao",  "xx",           "xx",       "xx",       "xx",       "xx",       "xx",       "xx"},
        {"Yanlei",      "xx",       "xx",       "xx",           "xx",       "xx",       "xx",       "xx",       "xx",       "xx"},
        {"Suozijia",    "xx",       "xx",       "xx",           "xx",       "xx",       "xx",       "xx",       "xx",       "xx"},
        {"Doupeng",     "xx",       "xx",       "xx",           "xx",       "xx",       "xx",       "xx",       "xx",       "xx"},
        {"Yaodao",      "xx",       "xx",       "xx",           "xx",       "xx",       "xx",       "xx",       "xx",       "xx"},
        {"Quantao",     "xx",       "xx",       "xx",           "xx",       "xx",       "xx",       "xx",       "xx",       "xx"},
        {"Jincc",       "xx",       "xx",       "xx",           "xx",       "xx",       "xx",       "xx",       "xx",       "xx"},
    };

    public string GetBigEquipment(int x, int y)
    {
        if (x < 0 || x >= bigEquipmentFormula.GetLength(0) || y < 0 || y >= bigEquipmentFormula.GetLength(1))
        {
            return "Over the border";
        }

        return bigEquipmentFormula[x, y];
    }
}