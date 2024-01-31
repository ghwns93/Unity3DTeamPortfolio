using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChest : MonoBehaviour
{
    [SerializeField]
    private GameObject slotparent;
    [SerializeField]
    private Slot[] slots;

    public List<ItemInfo> mItem = new List<ItemInfo>();

    private static MaterialChest Mchest = null;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotparent.GetComponentsInChildren<Slot>();
    }
#endif

    private void Awake()
    {
        if (null == Mchest)
        {
            Mchest = this;
        }
    }

    private void Start()
    {
        FreshSlot();
    }    

    public void AddItem(Item item, int count)
    {
        if (mItem.Count < slots.Length)
        {
            bool isDup = false;
            foreach (var n in mItem)
            {
                if (n.item == item)
                {
                    n.count += count;
                    isDup = true;
                }
            }

            if (!isDup)
            {
                mItem.Add(new ItemInfo { item = item, count = count });
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

        for (; i < mItem.Count && i < slots.Length; i++)
        {
            slots[i].Items = mItem[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Items = null;
        }
    }

    public static MaterialChest Instance
    {
        get
        {
            if (null == Mchest)
            {
                return null;
            }
            return Mchest;
        }
    }
}
