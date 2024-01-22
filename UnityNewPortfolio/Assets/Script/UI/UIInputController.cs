using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputController : MonoBehaviour
{
    public GameObject panel_item;
    public GameObject panel_Box;
    public GameObject panel_Equip;
    public Camera camera_EqipChar;

    bool OpenBox = false;   // false = ��������  true = ��������

    void Update()
    {
        // �÷��̾�� â����� �Ÿ��� ���� �Ÿ� ���� ���� �߰� �ؾ���
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
        Debug.Log("â�� ����");
        OpenBox = true;
    }

    public void BoxcloseButtonClicked()
    {
        panel_Box.SetActive(false);
        Debug.Log("â�� ����");
        OpenBox = false;
    }

    public void EquipchangeButtonClicked()
    {
        camera_EqipChar.enabled = true;
        panel_Equip.SetActive(true);
        Debug.Log("���â ����");
        panel_Box.SetActive(false);
    }

    public void EquipcloseButtonClicked()
    {
        camera_EqipChar.enabled = false;
        panel_Equip.SetActive(false);
        Debug.Log("���â ����");
        OpenStorageBox();
    }

    public void ItemchangeButtonClicked()
    {
        panel_Box.SetActive(false);
        panel_item.SetActive(true);
        Debug.Log("������â ����");
    }

    public void ItemcloseButtonClicked()
    {
        panel_item.SetActive(false);
        Debug.Log("������â ����");
        OpenStorageBox();
    }
}
