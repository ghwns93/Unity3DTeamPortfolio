using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChest : MonoBehaviour
{
    [SerializeField]
    private GameObject slotparent;
    [SerializeField]
    private ChestSlot[] slots;

    private static MaterialChest Mchest = null;

    private void Awake()
    {
        if (null == Mchest)
        {
            Mchest = this;
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

    public void AcquireItem(Item _item, int _count)
    {
        if (_item == null)
        {
            Debug.LogError("�������� null�Դϴ�.");
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
