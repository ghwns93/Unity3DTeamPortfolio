using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesChest : MonoBehaviour
{
    [SerializeField]
    private GameObject slotparent;
    [SerializeField]
    private Slot[] slots;

    public List<ItemInfo> cItem = new List<ItemInfo>();

    private static ConsumablesChest Cchest = null;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotparent.GetComponentsInChildren<Slot>();
    }
#endif

    private void Awake()
    {
        if (null == Cchest)
        {
            Cchest = this;
        }
    }

    private void Start()
    {
        FreshSlot();
    }    

    public void AddItem(Item item, int count)
    {
        if (cItem.Count < slots.Length)
        {
            bool isDup = false;
            foreach (var n in cItem)
            {
                if (n.item == item)
                {
                    n.count += count;
                    isDup = true;
                }
            }

            if (!isDup)
            {
                cItem.Add(new ItemInfo { item = item, count = count });
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

        for (; i < cItem.Count && i < slots.Length; i++)
        {
            slots[i].Items = cItem[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Items = null;
        }
    }

    public static ConsumablesChest Instance
    {
        get
        {
            if (null == Cchest)
            {
                return null;
            }
            return Cchest;
        }
    }
}
