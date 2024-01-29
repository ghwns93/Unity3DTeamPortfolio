using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PotionSlot : MonoBehaviour
{
    private ChestSlot slot;

    private static PotionSlot Pqs;

    public static PotionSlot Instance
    {
        get
        {
            if (Pqs == null)
            {
                Pqs = FindObjectOfType<PotionSlot>();
            }
            return Pqs;
        }
    }

    private void Start()
    {
        slot = GetComponent<ChestSlot>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PotionUse();
        }
    }

    public void PotionUse()
    {
        if (slot.item != null)
        {
            if (PlayerState.Instance.Hp < PlayerState.Instance.HpMax)
            {
                PlayerState.Instance.Hp += 50;

                if(PlayerState.Instance.Hp >= PlayerState.Instance.HpMax) 
                {
                    PlayerState.Instance.Hp = PlayerState.Instance.HpMax;
                }

                slot.itemCount--;

                slot.text_Count.text = slot.itemCount.ToString();

                if (slot.itemCount < 1)
                {
                    slot.ClearSlot();
                }
            }
        }
    }

    public void RegisterPotionToQuickSlot(Item _item, int _count)
    {
        if (_item == null)
        {
            Debug.Log("아이템이 null입니다.");
            return;
        }

        if (slot.item != null)
        {
            if (slot.item.itemName == _item.itemName)
            {
                if(slot.itemCount < _count)
                {
                    slot.SetSlotCount(_count);

                    if (slot.itemCount > _count)
                    {
                        slot.itemCount = _count;
                    }
                }
                
                return;
            }
        }

        if (slot.item == null)
        {
            slot.AddItem(_item, _count);
            return;
        }
    }
}
