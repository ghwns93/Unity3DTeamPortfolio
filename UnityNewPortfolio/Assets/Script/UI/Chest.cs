using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    bool OpenBox = false;   // false = ´İÈù»óÅÂ  true = ¿­¸°»óÅÂ

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
        Debug.Log("Ã¢°í ¿­¸²");
        OpenBox = true;
        playerUI.SetActive(false);
    }

    public void BoxcloseButtonClicked()
    {
        panel_Box.SetActive(false);
        Debug.Log("Ã¢°í ´İÈû");
        OpenBox = false;
        playerUI.SetActive(true);
    }

    public void EquipchangeButtonClicked()
    {
        panel_Equip.SetActive(true);
        Debug.Log("ÀåºñÃ¢ ¿­¸²");
        panel_Box.SetActive(false);
    }

    public void EquipcloseButtonClicked()
    {
        panel_Equip.SetActive(false);
        Debug.Log("ÀåºñÃ¢ ´İÈû");
        OpenStorageBox();
    }

    public void ItemchangeButtonClicked()
    {
        panel_Box.SetActive(false);
        panel_item.SetActive(true);
        Debug.Log("¾ÆÀÌÅÛÃ¢ ¿­¸²");
    }

    public void ItemcloseButtonClicked()
    {
        panel_item.SetActive(false);
        Debug.Log("¾ÆÀÌÅÛÃ¢ ´İÈû");
        OpenStorageBox();
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
