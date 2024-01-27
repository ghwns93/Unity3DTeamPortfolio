using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesChest : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private Slot[] slots;

    public List<ItemInfo> itemsC;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }
#endif

    private void Awake()
    {
        itemsC = new List<ItemInfo>();
    }

    private void Start()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;

        for (; i < itemsC.Count && i < slots.Length; i++)
        {
            slots[i].Items = itemsC[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Items = null;
        }
    }

    public void AddItem(Item item)
    {
        if (itemsC.Count < slots.Length)
        {
            bool isDup = false;
            foreach (var n in itemsC)
            {
                if (n.item == item)
                {
                    n.count++;
                    isDup = true;
                }
            }

            if (!isDup)
            {
                itemsC.Add(new ItemInfo { item = item, count = 1 });
            }

            FreshSlot();
        }
        else
        {
            Debug.Log("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
        }
    }
}
