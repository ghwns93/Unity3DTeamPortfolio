using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInfo> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

    private static Inventory instance = null;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        items = new List<ItemInfo>();
    }

    private void Start()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;

        for(; i < items.Count && i < slots.Length; i++) 
        {
            slots[i].Items = items[i];
        }
        for(; i< slots.Length;i++)
        {
            slots[i].Items = null;
        }
    }

    public void AddItem(Item item)
    {
        if(items.Count < slots.Length) 
        {
            bool isDup = false;
            foreach(var n in items)
            {
                if(n.item == item)
                {
                    n.count++;
                    isDup = true;
                }
            }

            if(!isDup)
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

    public static Inventory Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }
}

public class ItemInfo
{
    public Item item;
    public int count;
}