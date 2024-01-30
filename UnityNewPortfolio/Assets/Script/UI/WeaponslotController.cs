using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class WeaponslotController : MonoBehaviour
{
    public Slot slot;

    private static WeaponslotController instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static WeaponslotController Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        slot = GetComponent<Slot>();
    }

    public void RegisterWeaponToSlot(ItemInfo iteminfo)
    {
        if (slot.Itemc != null)
        {
            ItemInfo tempiteminfo = slot.Itemc;
            slot.Itemc = new ItemInfo { item = iteminfo.item, count = iteminfo.count };
            EquipChest.Instance.AddItem(tempiteminfo.item);
        }
        else if(slot.Itemc==null)
        {
            slot.Itemc = new ItemInfo { item = iteminfo.item, count = iteminfo.count };
        }
    }
}
