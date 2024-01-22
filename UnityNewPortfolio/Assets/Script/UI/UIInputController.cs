using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputController : MonoBehaviour
{
    public GameObject panel_item;
    public GameObject panel_Box;
    public GameObject panel_Equip;
    public Camera camera_EqipChar;

    bool OpenBox = false;   // false = 닫힌상태  true = 열린상태

    void Update()
    {
        // 플레이어와 창고와의 거리가 일정 거리 안일 때를 추가 해야함
        if (!OpenBox)
        {
            //if (Input.GetKeyDown(KeyCode.F))    
            //{
            //    OpenStorageBox();
            //}
        }
    }

    public void OpenStorageBox()
    {
        panel_Box.SetActive(true);
        Debug.Log("창고 열림");
        OpenBox = true;
    }

    public void BoxcloseButtonClicked()
    {
        panel_Box.SetActive(false);
        Debug.Log("창고 닫힘");
        OpenBox = false;
    }

    public void EquipchangeButtonClicked()
    {
        camera_EqipChar.enabled = true;
        panel_Equip.SetActive(true);
        Debug.Log("장비창 열림");
        panel_Box.SetActive(false);
    }

    public void EquipcloseButtonClicked()
    {
        camera_EqipChar.enabled = false;
        panel_Equip.SetActive(false);
        Debug.Log("장비창 닫힘");
        OpenStorageBox();
    }

    public void ItemchangeButtonClicked()
    {
        panel_Box.SetActive(false);
        panel_item.SetActive(true);
        Debug.Log("아이템창 열림");
    }

    public void ItemcloseButtonClicked()
    {
        panel_item.SetActive(false);
        Debug.Log("아이템창 닫힘");
        OpenStorageBox();
    }
}
