using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipChest : MonoBehaviour
{
    [SerializeField]
    private GameObject slotparent;
    [SerializeField]
    private Slot[] slots;

    public List<ItemInfo> eItem = new List<ItemInfo>();

    private static EquipChest Echest = null;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotparent.GetComponentsInChildren<Slot>();
    }
#endif

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
        FreshSlot();
    }    

    public void AddItem(Item item)
    {
        if (eItem.Count < slots.Length)
        {
            bool isDup = false;
            foreach (var n in eItem)
            {
                if (n.item == item)
                {
                    n.count++;
                    isDup = true;
                }
            }

            if (!isDup)
            {
                eItem.Add(new ItemInfo { item = item, count = 1 });
            }

            FreshSlot();
        }
        else
        {
            Debug.Log("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
        }
    }


    public void FreshSlot()
    {
        int i = 0;

        for (; i < eItem.Count && i < slots.Length; i++)
        {
            slots[i].Itemc = eItem[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Itemc = null;
        }
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
//            Debug.Log("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
//        }
//    }