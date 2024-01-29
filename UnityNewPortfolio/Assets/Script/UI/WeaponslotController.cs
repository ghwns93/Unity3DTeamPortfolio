using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class WeaponslotController : MonoBehaviour
{
    public ChestSlot slot;

    private static WeaponslotController WSC;

    public static WeaponslotController Instance
    {
        get
        {
            if (WSC == null)
            {
                WSC = FindObjectOfType<WeaponslotController>();
            }
            return WSC;
        }
    }

    void Start()
    {
        slot = GetComponent<ChestSlot>();
    }

    public void RegisterWeaponToSlot(Item _item, int _count)
    {
        if (_item == null)
        {
            Debug.LogError("아이템이 null입니다.");
            return;
        }

        slot.AddItem(_item, _count);
    }
}
