using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    public Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    public Tooltip tooltip;

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (item != null && item.itemType == Item.ObjectType.Potion)
            {
                PotionSlot.Instance.RegisterPotionToQuickSlot(item, itemCount);
            }
        }        
    }

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ObjectType.Weapon)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(tooltip != null)
        {
            if (item != null)
                tooltip.ShowToolTip(item, transform.position);
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideToolTip();
    }
}
