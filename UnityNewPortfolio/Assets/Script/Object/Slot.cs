using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	Image image;
	public Text count;
	public GameObject countobject;

    public Tooltip tooltip;

    ItemInfo items;

    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        count = transform.GetChild(1).GetComponent<Text>();
    }

    public ItemInfo Items
	{
		get { return items; }
		set { 
			items = value; 
			if(items != null)
			{
                image.sprite = items.item.itemImage;
                image.color = new Color(1, 1, 1, 1);
				count.text = items.count.ToString();
			}
			else
			{
				image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
				count.text = "0";
            }
		}
	}

    public ItemInfo Itemc
    {
        get { return items; }
        set
        {
            items = value;
            if (items != null)
            {
                image.sprite = items.item.itemImage;
                image.color = new Color(1, 1, 1, 1);

                if (items.item.itemType != Item.ObjectType.Weapon)
                {
                    countobject.SetActive(true);
                    count.text = items.count.ToString();
                }
            }
            else
            {
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
                count.text = "0";
                countobject.SetActive(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltip != null)
        {
            if (Itemc != null)
            {
                tooltip.ShowToolTip(items.item, transform.position);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip != null)
        {
            tooltip.HideToolTip();
        }
    }

    public void slotClicked()
    {
        //if (Itemc != null)
        //{
        //    if (Itemc.item != null)
        //    {
        //        if (Itemc.item.itemType == Item.ObjectType.Potion)
        //        {
        //            PotionSlot.Instance.AddItem(Itemc);
        //            Itemc = null;
        //        }
        //        else if (Itemc.item.itemType == Item.ObjectType.Weapon)
        //        {
        //            if (WeaponslotController.Instance.slot.Itemc.item == null)
        //            {
        //                WeaponslotController.Instance.RegisterWeaponToSlot(Itemc);
        //                Itemc = null;
        //            }
        //            else
        //            {
        //                ItemInfo tempitem = WeaponslotController.Instance.slot.Itemc;
        //                WeaponslotController.Instance.RegisterWeaponToSlot(Itemc);
        //                Itemc = tempitem;
        //            }
        //        }
        //    }
        //}

        if (Itemc != null && Itemc.item.itemType == Item.ObjectType.Potion)
        {
            if (PotionSlot.Instance != null)
            {
                PotionSlot.Instance.AddItem(Itemc);
                Itemc = null;
            }
        }
        else if (Itemc != null && Itemc.item.itemType == Item.ObjectType.Weapon)
        {
            WeaponslotController.Instance.RegisterWeaponToSlot(Itemc);
            Itemc = null;
        }
    }

    public void disrobebuttonClicked()
    {
        if (Itemc != null)
        {
            EquipChest.Instance.AddItem(Itemc.item);
            Itemc = null;
        }
    }
}
