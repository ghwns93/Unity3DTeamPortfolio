using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public GameObject tooltip_base;

    public Text text_itemname;
    public Text text_itemdesc;
    public Text text_itemused;

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        tooltip_base.SetActive(true);
        _pos += new Vector3(tooltip_base.GetComponent<RectTransform>().rect.width * 0.5f,
                            -tooltip_base.GetComponent<RectTransform>().rect.height * 0.5f,
                            0);
        tooltip_base.transform.position = _pos;

        text_itemdesc.text = _item.itemDesc;

        if(_item.itemType==Item.ObjectType.Weapon)
        {
            text_itemname.text = _item.itemName + " + " + _item.itemEnhance;
        }
        else
        {
            text_itemname.text = _item.itemName;
        }

        if (_item.itemType == Item.ObjectType.Weapon)
            text_itemused.text = "Å¬¸¯ - ÀåÂø";
        else if (_item.itemType == Item.ObjectType.Potion)
            text_itemused.text = "Å¬¸¯ - Äü½½·Ô µî·Ï";
        else
            text_itemused.text = "";
    }

    public void HideToolTip()
    {
        tooltip_base.SetActive(false);
    }
}
