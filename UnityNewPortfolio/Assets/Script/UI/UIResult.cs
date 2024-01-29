using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResult : MonoBehaviour
{
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;


    private void Start()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();

        for (int i = 0; i < Inventory.Instance.slots.Length; i++)
        {
            slots[i].Items = Inventory.Instance.slots[i].Items;
        }
    }

    public void GetallItem()
    {
        for (int i = 0; i < Inventory.Instance.slots.Length; i++)
        {
            if (slots[i].Items != null)
            {
                if (slots[i].Items.item.itemType == Item.ObjectType.Weapon)
                {
                    EquipChest.Instance.AcquireItem(slots[i].Items.item);
                    Inventory.Instance.slots[i].Items = null;
                    slots[i].Items = null;
                }
                else if (slots[i].Items.item.itemType == Item.ObjectType.Material)
                {
                    MaterialChest.Instance.AcquireItem(slots[i].Items.item, slots[i].Items.count);
                    Inventory.Instance.slots[i].Items = null;
                    slots[i].Items = null;
                }
                else if (slots[i].Items.item.itemType==Item.ObjectType.Potion)
                {
                    ConsumablesChest.Instance.AcquireItem(slots[i].Items.item, slots[i].Items.count);
                    Inventory.Instance.slots[i].Items = null;
                    slots[i].Items = null;
                }
            }            
        }
    }
}
