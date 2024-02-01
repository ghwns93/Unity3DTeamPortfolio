using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class WeaponslotController : MonoBehaviour
{
    public Slot slot;

    private static WeaponslotController instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }

    public static WeaponslotController Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        slot = GetComponent<Slot>();
        FreshSlot();
    }

    public void FreshSlot()
    {
        slot.Items = ChestItemDataManager.Instance.weaponslot;

        if(slot.Items != null)
        {
            Transform weaponPos = GameObject.Find("WeaponPos").transform;

            if (weaponPos.childCount > 0)
            {
                Destroy(weaponPos.GetChild(0).gameObject);
            }

            //Quaternion rotate = Quaternion.Euler(0, 0, 90);
            GameObject weaponIns = Instantiate(ChestItemDataManager.Instance.weaponslot.item.itemPrefab, weaponPos);
        }
    }
}
