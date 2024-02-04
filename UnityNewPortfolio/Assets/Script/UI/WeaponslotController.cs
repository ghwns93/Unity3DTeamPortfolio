using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class WeaponslotController : MonoBehaviour
{
    public Slot slot;

    private static WeaponslotController instance = null;

    public bool open;



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

        if (slot.Items != null)
        {
            Transform weaponPos = GameObject.Find("WeaponPos").transform;

            if (weaponPos.childCount > 0)
            {
                Destroy(weaponPos.GetChild(0).gameObject);
            }

            GameObject weaponIns = Instantiate(ChestItemDataManager.Instance.weaponslot.item.itemPrefab, weaponPos);
        }
        else
        {
            if (ChestItemDataManager.Instance.weaponslot == null)
                Debug.Log("장착 무기 없음");

            Transform weaponPos = GameObject.Find("WeaponPos").transform;

            if (weaponPos.childCount > 0)
            {
                Destroy(weaponPos.GetChild(0).gameObject);
            }
        }
    }

    public void UnmountingWeapon()
    {
        if (slot.Items != null)
        {
            SoundManager.soundManager.SEPlay(SEType.EquipChange);
            ChestItemDataManager.Instance.AddItem(slot.Items.item, 1);
            EquipChest.Instance.FreshSlot();
            ChestItemDataManager.Instance.weaponslot = null;
            FreshSlot();
        }
        else
            SoundManager.soundManager.SEPlay(SEType.WrongButtonClick);
    }
}
