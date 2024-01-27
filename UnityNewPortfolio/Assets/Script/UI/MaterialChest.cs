using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChest : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private Slot[] slots;

    public static MaterialChest mchest;

    public List<ItemInfo> items;

    private void Awake()
    {
        if (mchest == null)
        {
            mchest = this;
        }

        items = new List<ItemInfo>();
    }

    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;

        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].Items = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Items = null;
        }
    }

    public void AddItem(Item item)
    {
        if (items.Count < slots.Length)
        {
            bool isDup = false;
            foreach (var n in items)
            {
                if (n.item == item)
                {
                    n.count++;
                    isDup = true;
                }
            }

            if (!isDup)
            {
                items.Add(new ItemInfo { item = item, count = 1 });
            }

            FreshSlot();
        }
        else
        {
            Debug.Log("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
        }
    }
}
