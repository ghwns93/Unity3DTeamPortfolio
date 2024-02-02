using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestConroller : MonoBehaviour
{
    public Chest chest;

    public GameObject panel_Box;
    public GameObject playerUI;
    public GameObject EquipBox;         // ��� ����
    public GameObject ConsumablesBox;   // �Ҹ�ǰ ����
    public GameObject MaterialBox;      // ��� ����

    public GameObject stat;             // ��� �� ���� â
    public GameObject PotionSlot;       // ���� ������

    public GameObject inventory;

    GameObject player;
    PlayerController pc;

    private void Awake()
    {
        inventory.SetActive(true);
        panel_Box.SetActive(true);
        EquipBox.SetActive(true);
        ConsumablesBox.SetActive(true);
        MaterialBox.SetActive(true);

        inventory.SetActive(true);
        panel_Box.SetActive(false);
        EquipBox.SetActive(false);
        ConsumablesBox.SetActive(false);
        MaterialBox.SetActive(false);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();


        WeaponslotController.Instance.FreshSlot();
    }

    public void StorageBoxselectCloseButtonClicked()
    {
        panel_Box.SetActive(false);
        Debug.Log("â�� ����");
        chest.OpenBox = false;
        pc.isUiOpen = false;
        playerUI.SetActive(true);
        PotionSlot.SetActive(true);
    }

    public void EquipBoxOpenButtonClicked()
    {
        panel_Box.SetActive(false);
        EquipBox.SetActive(true);
        EquipBox.GetComponent<RectTransform>().position = new Vector3(500f, 540f);
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
        panel_Box.SetActive(true);
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
        panel_Box.SetActive(true);
    }

    public void MaterialBoxOpenButtonClicked()
    {
        panel_Box.SetActive(false);
        MaterialBox.SetActive(true);
        Debug.Log("��� ���� ����");
    }

    public void MaterialBoxCloseButtonClicked()
    {
        MaterialBox.SetActive(false);
        Debug.Log("��� ���� ����");
        panel_Box.SetActive(true);
    }

}
