using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	Image image;
	Text count;

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

}
