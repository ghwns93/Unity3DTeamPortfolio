using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesChest : MonoBehaviour
{
    [SerializeField]
    private GameObject slotparent;
    [SerializeField]
    private ChestSlot[] slots;

    private static ConsumablesChest Cchest;

    public static ConsumablesChest Instance
    {
        get
        {
            if (Cchest == null)
            {
                Cchest = FindObjectOfType<ConsumablesChest>();
            }
            return Cchest;
        }
    }

    private void Awake()
    {
        slots = slotparent.GetComponentsInChildren<ChestSlot>();
    }

    public void AcquireItem(Item _item, int _count)
    {
        if (_item == null)
        {
            Debug.LogError("아이템이 null입니다.");
            return;
        }

        if (_item.itemType != Item.ObjectType.Weapon)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
