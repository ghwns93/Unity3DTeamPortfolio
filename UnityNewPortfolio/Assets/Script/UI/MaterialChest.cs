using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChest : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private Slot[] slots;

    public List<ItemInfo> itemsM;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }
#endif

    private void Awake()
    {
        itemsM = new List<ItemInfo>();
    }

    private void Start()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;

        for (; i < itemsM.Count && i < slots.Length; i++)
        {
            slots[i].Items = itemsM[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Items = null;
        }
    }

    public void AddItem(Item item)
    {
        if (itemsM.Count < slots.Length)
        {
            bool isDup = false;
            foreach (var n in itemsM)
            {
                if (n.item == item)
                {
                    n.count++;
                    isDup = true;
                }
            }

            if (!isDup)
            {
                itemsM.Add(new ItemInfo { item = item, count = 1 });
            }

            FreshSlot();
        }
        else
        {
            Debug.Log("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
        }
    }
}
