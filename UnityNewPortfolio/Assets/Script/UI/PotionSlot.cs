using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PotionSlot : MonoBehaviour
{
    private Slot slot;

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
        if (slot.Itemc.item != null)
        {
            if (PlayerState.Instance.Hp < PlayerState.Instance.HpMax)
            {
                PlayerState.Instance.Hp += 50;

                if(PlayerState.Instance.Hp >= PlayerState.Instance.HpMax) 
                {
                    PlayerState.Instance.Hp = PlayerState.Instance.HpMax;
                }

                slot.Itemc.count--;

                slot.count.text = slot.Itemc.count.ToString();

                if (slot.Itemc.count < 1)
                {
                    slot.Itemc = null;
                }
            }
        }
    }

    public void RegisterPotionToQuickSlot(ItemInfo iteminfo)
    {
        //if (iteminfo == null)
        //{
        //    Debug.Log("아이템이 null입니다.");
        //    return;
        //}

        //if (slot.Itemc != null)
        //{
        //    if (slot.Itemc.item.itemName == iteminfo.item.itemName)
        //    {
        //        iteminfo.count = slot.Itemc.count;
        //    }
        //}
        //else if (slot.Itemc == null)
        //{
        //    slot.Itemc = iteminfo;
        //}

        if (iteminfo == null)
        {
            Debug.Log("아이템이 null입니다.");
            return;
        }

        if (slot != null && slot.Itemc != null)
        {
            if (slot.Itemc.item != null && iteminfo.item != null && slot.Itemc.item.itemName == iteminfo.item.itemName)
            {
                iteminfo.count = slot.Itemc.count;
            }
        }
        else if (slot != null && slot.Itemc == null)
        {
            slot.Itemc = iteminfo;
        }
    }
}
