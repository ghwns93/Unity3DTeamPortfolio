using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopslot : MonoBehaviour
{
    public GameObject description;
    public Image image_Item;

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

    }

    // 나가기 클릭 시
    public void exitClicked()
    {
        description.SetActive(false);
    }


}
