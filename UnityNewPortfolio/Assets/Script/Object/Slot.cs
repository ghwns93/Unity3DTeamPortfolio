using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour
{
	Image image;
	Text count;
	public GameObject countobject;

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
}
