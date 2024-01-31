using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopDesc : MonoBehaviour
{
    public GameObject shopDesc;
    public Text text_itemName;
    public Text text_itemDesc;
    public Text text_itemPrice;

    Item currentItem;
    public void ShowPurchaseWindow(Item item)
    {
        currentItem = item;

        shopDesc.SetActive(true);

        // 아이템 정보 업데이트
        text_itemName.text = currentItem.itemName;
        text_itemDesc.text = currentItem.itemDesc;
        text_itemPrice.text = currentItem.itemprice.ToString() + "G";
    }

    public void buyitem()
    {
        if (currentItem != null)
        {
            EquipChest.Instance.AddItem(currentItem);
        }

        if (PlayerState.Instance.Money < currentItem.itemprice)
        {
            Debug.Log("구매 실패");
            return;
        }
        else
        {
            if (currentItem.itemType == Item.ObjectType.Weapon)
            {
                if (currentItem == null)
                {
                    Debug.LogError("Item is null.");
                    return;
                }

                EquipChest.Instance.AddItem(currentItem);
                PlayerState.Instance.Money -= currentItem.itemprice;
                Debug.Log(currentItem.itemName + " 구매 성공");
                exitClicked();
            }
            else if (currentItem.itemType == Item.ObjectType.Potion)
            {
                ConsumablesChest.Instance.AddItem(currentItem, 1);
                PlayerState.Instance.Money -= currentItem.itemprice;
                Debug.Log(currentItem.itemName + " 구매 성공");
            }
        }
    }

    public void exitClicked()
    {
        shopDesc.SetActive(false);
    }
}
