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

        // ������ ���� ������Ʈ
        text_itemName.text = currentItem.itemName;
        text_itemDesc.text = currentItem.itemDesc;
        text_itemPrice.text = currentItem.itemprice.ToString() + "G";
    }

    public void buyitem()
    {
        if (PlayerState.Instance.Money < currentItem.itemprice)
        {
            Debug.Log("���� ����");
            return;
        }
        else
        {
            if (currentItem.itemType == Item.ObjectType.Weapon)
            {
                ChestItemDataManager.Instance.AddItem(currentItem, 1);
                EquipChest.Instance.FreshSlot();
                PlayerState.Instance.Money -= currentItem.itemprice;
                Debug.Log(currentItem.itemName + " ���� ����");
                exitClicked();
            }
            else if (currentItem.itemType == Item.ObjectType.Potion)
            {
                ChestItemDataManager.Instance.AddItem(currentItem, 1);
                ConsumablesChest.Instance.FreshSlot();
                PlayerState.Instance.Money -= currentItem.itemprice;
                Debug.Log(currentItem.itemName + " ���� ����");
            }
        }
    }

    public void exitClicked()
    {
        shopDesc.SetActive(false);
    }
}
