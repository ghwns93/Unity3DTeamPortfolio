using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PotionSlot : MonoBehaviour
{
    public Slot slot;

    private static PotionSlot instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static PotionSlot Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        slot = GetComponent<Slot>();

        FreshSlot();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PotionUse();
        }
    }

    public void PotionUse()
    {
        if (slot.Items.item != null)
        {
            if (PlayerState.Instance.Hp < PlayerState.Instance.HpMax)
            {
                PlayerState.Instance.Hp += 50;

                if(PlayerState.Instance.Hp >= PlayerState.Instance.HpMax) 
                {
                    PlayerState.Instance.Hp = PlayerState.Instance.HpMax;
                }

                slot.Items.count--;

                slot.count.text = slot.Items.count.ToString();

                if (slot.Items.count < 1)
                {
                    slot.Items = null;
                }
            }
        }
    }

    public void RegisterPotionToQuickSlot(ItemInfo iteminfo)
    {
        if (iteminfo == null)
        {
            Debug.Log("아이템이 null입니다.");
            return;
        }

        if (slot != null && slot.Items != null)
        {
            if (slot.Items.item != null && iteminfo.item != null && slot.Items.item.itemName == iteminfo.item.itemName)
            {
                iteminfo.count = slot.Items.count;
            }
        }
        else if (slot != null && slot.Items == null)
        {
            slot.Items = iteminfo;
        }
    }

    public void AddItem(ItemInfo iteminfo)
    {
        bool isDup = false;
        if (slot.Items == iteminfo)
        {
            slot.Items.count += iteminfo.count;
            isDup = true;
        }

        if (!isDup)
        {
            slot.Items = new ItemInfo { item = iteminfo.item, count = iteminfo.count };
        }
    }

    public void FreshSlot()
    {
        slot.Items = ChestItemDataManager.Instance.potionslot;
    }
}


