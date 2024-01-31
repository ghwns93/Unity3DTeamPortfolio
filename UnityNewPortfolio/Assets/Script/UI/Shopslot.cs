using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Shopslot : MonoBehaviour
{
    public Item item;
    Image image;

    public GameObject shopDesc;
    public Text text_itemName;
    public Text text_itemDesc;
    public Text text_itemPrice;

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

    public void slotClicked(Vector3 _pos)
    {
        shopDesc.SetActive(true);

        _pos += new Vector3(shopDesc.GetComponent<RectTransform>().rect.width * 0.5f,
                            -shopDesc.GetComponent<RectTransform>().rect.height * 0.5f,
                            0);
        shopDesc.transform.position = _pos;

        text_itemDesc.text = item.itemDesc;

        text_itemPrice.text = item.itemprice.ToString() + "G";

        if (item.itemType == Item.ObjectType.Weapon)
        {
            text_itemName.text = item.itemName + " + " + item.itemEnhance;
        }
        else
        {
            text_itemName.text = item.itemName;
        }
    }

    public void buyClicked()
    {
        if (PlayerState.Instance.Money < item.itemprice)
        {
            Debug.Log("구매 실패");
            exitClicked();
            return;
        }
        else
        {
            if (item.itemType == Item.ObjectType.Weapon)
            {
                EquipChest.Instance.AddItem(item);
                PlayerState.Instance.Money -= item.itemprice;
                Debug.Log("구매 성공");
                exitClicked();
            }
            else if (item.itemType == Item.ObjectType.Potion)
            {
                ConsumablesChest.Instance.AddItem(item, 1);
                PlayerState.Instance.Money -= item.itemprice;
                Debug.Log("구매 성공");
                exitClicked();
            }
        }
    }

    // 나가기 클릭 시
    public void exitClicked()
    {
        shopDesc.SetActive(false);
    }
}
