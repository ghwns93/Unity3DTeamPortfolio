using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopslot : MonoBehaviour
{
    public Item iteem;
    public GameObject description;
    Image image;

    public int price;

    EquipChest Echest;
    ConsumablesChest Cchest;

    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        image.sprite = iteem.itemImage;

        if (iteem != null)
        {
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0);
        }
    }

    // 구매하려는 아이템 아이콘 클릭 시
    public void slotClicked()
    {
        Vector3 mPos = Input.mousePosition;
        description.transform.position = mPos;
        description.SetActive(true);
    }

    // 구매 클릭 시
    public void buyClicked()
    {
        if (PlayerState.Instance.Money < price)
        {
            Debug.Log("구매 실패");
            return;
        }
        else
        {
            if (iteem.itemType == Item.ObjectType.Weapon)
            {
                Echest.AddItem(iteem);
                PlayerState.Instance.Money -= price;
                Debug.Log("구매 성공");
                exitClicked();
            }
            else if (iteem.itemType == Item.ObjectType.Potion)
            {
                Cchest.AddItem(iteem);
                PlayerState.Instance.Money -= price;
                Debug.Log("구매 성공");
                exitClicked();
            }
            else
            {
                return;
            }
        }
    }

    // 나가기 클릭 시
    public void exitClicked()
    {
        description.SetActive(false);
    }
}
