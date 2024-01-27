using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopslot : MonoBehaviour
{
    public Item item;
    public GameObject description;
    Image image;

    public int price;


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
        Vector3 mPos = Input.mousePosition;
        description.transform.position = mPos;
        description.SetActive(true);
    }

    public void buyClicked()
    {
        if (PlayerState.Instance.Money < price)
        {
            Debug.Log("구매 실패");
            exitClicked();
            return;
        }
        else
        {
            if (item.itemType == Item.ObjectType.Weapon)
            {
                EquipChest.Instance.AcquireItem(item);
                PlayerState.Instance.Money -= price;
                Debug.Log("구매 성공");
                exitClicked();
            }
            else if (item.itemType == Item.ObjectType.Potion)
            {
                ConsumablesChest.Instance.AcquireItem(item);
                PlayerState.Instance.Money -= price;
                Debug.Log("구매 성공");
                exitClicked();
            }
        }
    }

    // 나가기 클릭 시
    public void exitClicked()
    {
        description.SetActive(false);
    }
}
