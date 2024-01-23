using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    bool OpenBox = false;   // false = ��������  true = ��������

    public GameObject panel_Box;
    public GameObject playerUI;
    public GameObject panel_item;
    public GameObject panel_Equip;

    public GameObject canvas;

    public float length = 5.0f;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas.SetActive(false);
    }

    void Update()
    {
        if(CheckLength(player.transform.position))
        {
            canvas.SetActive(true);

            if (!OpenBox)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenStorageBox();
                }
            }
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    public void OpenStorageBox()
    {
        panel_Box.SetActive(true);
        Debug.Log("â�� ����");
        OpenBox = true;
        playerUI.SetActive(false);
    }

    public void BoxcloseButtonClicked()
    {
        panel_Box.SetActive(false);
        Debug.Log("â�� ����");
        OpenBox = false;
        playerUI.SetActive(true);
    }

    public void EquipchangeButtonClicked()
    {
        panel_Equip.SetActive(true);
        Debug.Log("���â ����");
        panel_Box.SetActive(false);
    }

    public void EquipcloseButtonClicked()
    {
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

    // �Ÿ� Ȯ��
    bool CheckLength(Vector3 targetPos)
    {
        bool ret = false;
        float d = Vector3.Distance(transform.position, targetPos);

        if (length >= d)
        {
            ret = true;
        }

        return ret;
    }
}
