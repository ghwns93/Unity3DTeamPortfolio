using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

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

        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;
        for(; i < items.Count && i < slots.Length; i++) 
        {
            Debug.Log("i : " + i);
            slots[i].Item = items[i];
        }
        for(; i< slots.Length;i++)
        {
            slots[i].Item = null;
        }
    }

    public void AddItem(Item item)
    {
        if(items.Count < slots.Length) 
        {
            if (!items.Contains(item))
            {
                item.count = 0;
                items.Add(item);
            }
            
            for(int i  = 0; i < items.Count; i++)
            {
                if (items[i] == item)
                {
                    items[i].count++;
                    break;
                }
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
