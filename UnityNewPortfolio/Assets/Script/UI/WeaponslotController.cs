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

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
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
    }

    public void RegisterWeaponToSlot(ItemInfo iteminfo)
    {
        if (iteminfo == null)
        {
            Debug.LogError("아이템이 null입니다.");
            return;
        }

        slot.Itemc = iteminfo;
    }
}
