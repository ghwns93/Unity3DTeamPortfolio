using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputController : MonoBehaviour
{
    bool inventoryOpened = false;
    bool EquipOpened = false;

    public GameObject Inven;
    public GameObject Equip;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) // ÀÎº¥Åä¸® ¿ÀÇÂ
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

        if(Input.GetKeyDown(KeyCode.P)) // Àåºñ ½ºÅÈ Ã¢ ¿ÀÇÂ
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
        EquipOpened = true;
    }

    public void EquipClose()
    {
        Equip.SetActive(false);
        EquipOpened = false;
    }
}
