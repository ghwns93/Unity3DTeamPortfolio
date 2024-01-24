using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopslot : MonoBehaviour
{
    public GameObject description;
    public Image image_Item;

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

    }

    // ������ Ŭ�� ��
    public void exitClicked()
    {
        description.SetActive(false);
    }


}
