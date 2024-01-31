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
        if (slot.Items != null)
        {
            ItemInfo tempiteminfo = slot.Items;
            slot.Items = new ItemInfo { item = iteminfo.item, count = iteminfo.count };
            EquipChest.Instance.AddItem(tempiteminfo.item);
        }
        else if(slot.Items==null)
        {
            slot.Items = new ItemInfo { item = iteminfo.item, count = iteminfo.count };
        }
    }
}
