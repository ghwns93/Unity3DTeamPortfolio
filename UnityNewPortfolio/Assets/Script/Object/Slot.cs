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
                if (countobject != null)
                {
                    if (items.item.itemType != Item.ObjectType.Weapon)
                    {
                        countobject.SetActive(true);
                        count.text = items.count.ToString();
                    }
                }
                else
                    count.text = items.count.ToString();
            }
			else
			{
				image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
				count.text = "0";
                if (countobject != null)
                {
                    countobject.SetActive(false);
                }
            }
		}
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltip != null)
        {
            if (Items != null)
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
        if (Items != null && Items.item.itemType == Item.ObjectType.Potion)
        {
            if (PotionSlot.Instance != null)
            {
                PotionSlot.Instance.AddItem(Items);
                Items = null;
            }
        }
        else if (Items != null && Items.item.itemType == Item.ObjectType.Weapon)
        {
            WeaponslotController.Instance.RegisterWeaponToSlot(Items);
            Items = null;
        }
    }

    public void disrobebuttonClicked()
    {
        if (Items != null)
        {
            EquipChest.Instance.AddItem(Items.item);
            Items = null;
        }
    }
}
