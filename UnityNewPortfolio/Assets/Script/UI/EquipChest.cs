using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipChest : MonoBehaviour
{
    [SerializeField]
    private GameObject slotparent;
    [SerializeField]
    private ChestSlot[] slots;

    public List<Item> eItem;

    private static EquipChest Echest = null;

    private void Awake()
    {
        if (null == Echest)
        {
            Echest = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        slots = slotparent.GetComponentsInChildren<ChestSlot>();
    }

    public static EquipChest Instance
    {
        get
        {
            if (null == Echest)
            {
                return null;
            }
            return Echest;
        }
    }

    public void AcquireItem(Item _item, int _count = 1)
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




//#if UNITY_EDITOR
//    private void OnValidate()
//    {
//        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
//    }
//#endif

//    private void Awake()
//    {
//        itemsE = new List<ItemInfo>();
//    }

//    private void Start()
//    {
//        FreshSlot();
//    }

//    public void FreshSlot()
//    {
//        int i = 0;

//        for (; i < itemsE.Count && i < slots.Length; i++)
//        {
//            slots[i].Items = itemsE[i];
//        }
//        for (; i < slots.Length; i++)
//        {
//            slots[i].Items = null;
//        }
//    }

//    public void AddItem(Item item)
//    {
//        if (itemsE.Count < slots.Length)
//        {
//            bool isDup = false;
//            foreach (var n in itemsE)
//            {
//                if (n.item == item)
//                {
//                    n.count++;
//                    isDup = true;
//                }
//            }

//            if (!isDup)
//            {
//                itemsE.Add(new ItemInfo { item = item, count = 1 });
//            }

//            FreshSlot();
//        }
//        else
//        {
//            Debug.Log("슬롯이 가득 차 있습니다.");
//        }
//    }