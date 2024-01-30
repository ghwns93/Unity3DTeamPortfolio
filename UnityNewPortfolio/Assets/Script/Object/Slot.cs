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
        //if (items != null && items.item.itemType == Item.ObjectType.Potion)
        //{
        //    PotionSlot.Instance.RegisterPotionToQuickSlot(items);

        //    items = null;
        //}
        //else if (items != null && items.item.itemType == Item.ObjectType.Weapon)
        //{
        //    if (WeaponslotController.Instance.slot.Itemc.item == null)    // 장착중인 무기가 없을 때
        //    {
        //        WeaponslotController.Instance.RegisterWeaponToSlot(items);

        //        items = null;
        //    }
        //    else        // 장착중인 무기가 있을 때
        //    {
        //        ItemInfo tempitem = WeaponslotController.Instance.slot.items;

        //        WeaponslotController.Instance.RegisterWeaponToSlot(items);
        //        items = tempitem;
        //    }
        //}

        Debug.Log("slotClicked - Start");

        if (items != null)
        {
            if (items.item != null)
            {
                Debug.Log("item is not null");

                if (items.item.itemType == Item.ObjectType.Potion)
                {
                    PotionSlot.Instance.RegisterPotionToQuickSlot(items);
                    items = null;
                }
                else if (items.item.itemType == Item.ObjectType.Weapon)
                {
                    if (WeaponslotController.Instance.slot.Itemc.item == null)
                    {
                        WeaponslotController.Instance.RegisterWeaponToSlot(items);
                        items = null;
                    }
                    else
                    {
                        ItemInfo tempitem = WeaponslotController.Instance.slot.items;
                        WeaponslotController.Instance.RegisterWeaponToSlot(items);
                        items = tempitem;
                    }
                }
            }
            else
            {
                Debug.Log("item is null");
            }
        }
        else
        {
            Debug.Log("items is null");
        }

        Debug.Log("slotClicked - End");
    }
}
