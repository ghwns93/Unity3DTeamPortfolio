using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shop : MonoBehaviour
{
    public GameObject canvas;

    public GameObject slot_weapon;
    public GameObject slot_armor;
    public GameObject slot_item;

    GameObject player;

    public float length = 2.0f;

    bool shopOpened = false;    // false = ´İÈû true = ¿­¸²

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (CheckLength(player.transform.position))
        {
            if (!shopOpened)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    canvas.SetActive(true);
                    shopOpened = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Exitbuttonclick();
                }
            }
        }
    }

    public void WeaponbuttonClick()
    {
        slot_weapon.SetActive(true);
        slot_armor.SetActive(false);
        slot_item.SetActive(false);
    }

    public void ArmorbuttonClick()
    {
        slot_weapon.SetActive(false);
        slot_armor.SetActive(true);
        slot_item.SetActive(false);
    }

    public void ItembuttonClick()
    {
        slot_weapon.SetActive(false);
        slot_armor.SetActive(false);
        slot_item.SetActive(true);
    }

    public void Exitbuttonclick()
    {
        canvas.SetActive(false);
        shopOpened = false;
    }

    // °Å¸® È®ÀÎ
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
