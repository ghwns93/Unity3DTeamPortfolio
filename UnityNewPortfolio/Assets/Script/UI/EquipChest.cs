using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipChest : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private Slot[] slots;

    public List<ItemInfo> itemsE;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }
#endif

    private void Awake()
    {
        itemsE = new List<ItemInfo>();
    }

    private void Start()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;

        for (; i < itemsE.Count && i < slots.Length; i++)
        {
            slots[i].Items = itemsE[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Items = null;
        }
    }

    public void AddItem(Item item)
    {
        if (itemsE.Count < slots.Length)
        {
            bool isDup = false;
            foreach (var n in itemsE)
            {
                if (n.item == item)
                {
                    n.count++;
                    isDup = true;
                }
            }

            if (!isDup)
            {
                itemsE.Add(new ItemInfo { item = item, count = 1 });
            }

            FreshSlot();
        }
        else
        {
            Debug.Log("������ ���� �� �ֽ��ϴ�.");
        }
    }
}
