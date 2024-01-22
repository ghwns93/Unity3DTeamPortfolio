using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	Image image;
	Text count;

	private Item item;

    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        count = transform.GetChild(1).GetComponent<Text>();
    }

    public Item Item
	{
		get { return item; }
		set { 
			item = value; 
			if(item != null)
			{
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
				count.text = item.count.ToString();
			}
			else
            {
                image.color = new Color(1, 1, 1, 0);
                count.text = "0";
			}
		}
	}

}
