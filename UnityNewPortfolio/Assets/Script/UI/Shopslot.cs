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

    // �����Ϸ��� ������ ������ Ŭ�� ��
    public void slotClicked()
    {
        Vector3 mPos = Input.mousePosition;
        description.transform.position = mPos;
        description.SetActive(true);
    }

    // ���� Ŭ�� ��
    public void buyClicked()
    {
        if (PlayerState.Instance.Money < price)
        {
            Debug.Log("���� ����");
            return;
        }
        else
        {
            if (iteem.itemType == Item.ObjectType.Weapon)
            {
                Echest.AddItem(iteem);
                PlayerState.Instance.Money -= price;
                Debug.Log("���� ����");
                exitClicked();
            }
            else if (iteem.itemType == Item.ObjectType.Potion)
            {
                Cchest.AddItem(iteem);
                PlayerState.Instance.Money -= price;
                Debug.Log("���� ����");
                exitClicked();
            }
            else
            {
                return;
            }
        }
    }

    // ������ Ŭ�� ��
    public void exitClicked()
    {
        description.SetActive(false);
    }
}
