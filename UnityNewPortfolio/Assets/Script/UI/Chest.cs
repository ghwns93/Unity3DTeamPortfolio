using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    bool OpenBox = false;   // false = ��������  true = ��������

    public GameObject panel_Box;
    public GameObject playerUI;
    public GameObject EquipBox;         // ��� ����
    public GameObject ConsumablesBox;   // �Ҹ�ǰ ����
    public GameObject MaterialBox;      // ��� ����

    public GameObject stat;             // ��� �� ���� â
    public GameObject PotionSlot;       // �κ��丮

    public GameObject canvas;

    public float length = 5.0f;

    GameObject player;

    private void Awake()
    {
        canvas.SetActive(true);
        panel_Box.SetActive(true);
        EquipBox.SetActive(true);
        ConsumablesBox.SetActive(true);
        MaterialBox.SetActive(true);
        
        canvas.SetActive(false);
        panel_Box.SetActive(false);
        EquipBox.SetActive(false);
        ConsumablesBox.SetActive(false);
        MaterialBox.SetActive(false);
    }

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

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!OpenBox)
                {
                    StorageBoxOpen();
                }
            }            
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    public void StorageBoxOpen()
    {
        panel_Box.SetActive(true);
        Debug.Log("â�� ����");
        OpenBox = true;
        playerUI.SetActive(false);
        PotionSlot.SetActive(false);
    }

    public void StorageBoxselectCloseButtonClicked()
    {
        panel_Box.SetActive(false);
        Debug.Log("â�� ����");
        OpenBox = false;
        playerUI.SetActive(true);
        PotionSlot.SetActive(true);
    }

    public void EquipBoxOpenButtonClicked()
    {
        panel_Box.SetActive(false);
        EquipBox.SetActive(true);
        stat.SetActive(true);
        stat.GetComponent<RectTransform>().position = new Vector3(1376.5f, 540f);        
        Debug.Log("��� ���� ����");
    }

    public void EquipBoxCloseButtonClicked()
    {
        EquipBox.SetActive(false);
        stat.GetComponent<RectTransform>().position = new Vector3(566f, 540f);
        stat.SetActive(false);
        Debug.Log("��� ���� ����");
        StorageBoxOpen();
    }

    public void ConsumablesBoxOpenButtonClicked()
    {
        panel_Box.SetActive(false);
        ConsumablesBox.SetActive(true);
        PotionSlot.SetActive(true);
        Debug.Log("�Ҹ�ǰ ���� ����");
    }

    public void ConsumablesBoxCloseButtonClicked()
    {
        ConsumablesBox.SetActive(false);
        PotionSlot.SetActive(false);
        Debug.Log("�Ҹ�ǰ ���� ����");
        StorageBoxOpen();
    }

    public void MaterialBoxOpenButtonClicked()
    {
        panel_Box.SetActive(false);
        MaterialBox.SetActive(true);
        Debug.Log("�Ҹ�ǰ ���� ����");
    }

    public void MaterialBoxCloseButtonClicked()
    {
        MaterialBox.SetActive(false);
        Debug.Log("�Ҹ�ǰ ���� ����");
        StorageBoxOpen();
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
