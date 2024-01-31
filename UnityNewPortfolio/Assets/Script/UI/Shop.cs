using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Chest chest;

    public GameObject canvas_shop;
    public GameObject slot_weapon;
    public GameObject slot_item;

    public GameObject equipchest;
    public GameObject Consumableschest;

    public GameObject shopDesc;

    GameObject player;

    public float length = 2.0f;

    bool shopOpened = false;    // false = ´ÝÈû true = ¿­¸²

    PlayerController pc;

    public GameObject canvas;
    GameObject target;

    private void Awake()
    {
        canvas_shop.SetActive(true);
        slot_weapon.SetActive(true);
        slot_item.SetActive(true);

        slot_item.SetActive(false);
        canvas_shop.SetActive(false);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();

        target = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        if (CheckLength(player.transform.position))
        {
            canvas.SetActive(true);
            canvas.transform.forward = target.transform.forward;

            if (!shopOpened)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pc.isUiOpen = true;
                    canvas_shop.SetActive(true);
                    shopOpened = true;
                    slot_weapon.SetActive(true);

                    equipchest.SetActive(true);
                    equipchest.GetComponent<RectTransform>().position = new Vector3(1184f, 540f);
                }
            }
        }
        else
            canvas.SetActive(false);
    }

    public void WeaponbuttonClick()
    {
        slot_weapon.SetActive(true);
        slot_item.SetActive(false);
        shopDesc.SetActive(false);
        equipchest.SetActive(true);
        equipchest.GetComponent<RectTransform>().position = new Vector3(1184f, 540f);
        if(Consumableschest.activeSelf)
        {
            Consumableschest.GetComponent<RectTransform>().position = new Vector3(960f, 540f);
            Consumableschest.SetActive(false);
        }
    }

    public void ItembuttonClick()
    {
        slot_weapon.SetActive(false);
        slot_item.SetActive(true);
        shopDesc.SetActive(false);
        Consumableschest.SetActive(true);
        Consumableschest.GetComponent<RectTransform>().position = new Vector3(1184f, 540f);
        if (equipchest.activeSelf)
        {
            equipchest.GetComponent<RectTransform>().position = new Vector3(480f, 540f);
            equipchest.SetActive(false);
        }
    }

    public void Exitbuttonclick()
    {
        WeaponbuttonClick();
        canvas_shop.SetActive(false);
        shopOpened = false;
        pc.isUiOpen = false;

        if (equipchest.activeSelf)
        {
            equipchest.GetComponent<RectTransform>().position = new Vector3(480f, 540f);
            equipchest.SetActive(false);
        }
        else if (Consumableschest.activeSelf)
        {
            Consumableschest.GetComponent<RectTransform>().position = new Vector3(960f, 540f);
            Consumableschest.SetActive(false);
        }
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
