using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Shopslot : MonoBehaviour
{
    public Item item;
    Image image;

    public ShopDesc shopdesc;

    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        image.sprite = item.itemImage;

        if (item != null)
        {
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0);
        }        
    }

    public void slotClicked()
    {
        shopdesc.ShowPurchaseWindow(item);
    }
}
