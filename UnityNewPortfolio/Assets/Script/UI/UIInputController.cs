using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputController : MonoBehaviour
{
    bool inventoryOpened = false;
    bool EquipOpened = false;

    public GameObject Inven;
    public GameObject Equip;

    private void Awake()
    {
        Inven.SetActive(true);
        Equip.SetActive(true);

        Inven.SetActive(false);
        Equip.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) // �κ��丮 ����
        {
            if(!inventoryOpened)
            {
                InventoryOpen();
            }
            else
            {
                InventoryClose();
            }
        }

        if(Input.GetKeyDown(KeyCode.P)) // ��� ���� â ����
        {
            if(!EquipOpened)
            {
                EquipOpen();
            }
            else
            {
                EquipClose();
            }
        }
    }

    public void InventoryOpen()
    {
        Inven.SetActive(true);
        inventoryOpened = true;
    }

    public void InventoryClose()
    {
        Inven.SetActive(false);
        inventoryOpened = false;
    }

    public void EquipOpen()
    {
        Equip.SetActive(true);
        WeaponslotController.Instance.open = true;
        EquipOpened = true;
    }

    public void EquipClose()
    {
        WeaponslotController.Instance.open = false;
        Equip.SetActive(false);
        EquipOpened = false;
    }
}
