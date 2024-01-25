using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    bool OpenBox = false;   // false = 닫힌상태  true = 열린상태

    public GameObject panel_Box;
    public GameObject playerUI;
    public GameObject EquipBox;         // 장비 상자
    public GameObject ConsumablesBox;   // 소모품 상자
    public GameObject MaterialBox;      // 재료 상자

    public GameObject stat;             // 장비 및 스탯 창
    public GameObject PotionSlot;       // 인벤토리

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
        Debug.Log("창고 열림");
        OpenBox = true;
        playerUI.SetActive(false);
        PotionSlot.SetActive(false);
    }

    public void StorageBoxselectCloseButtonClicked()
    {
        panel_Box.SetActive(false);
        Debug.Log("창고 닫힘");
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
        Debug.Log("장비 상자 열림");
    }

    public void EquipBoxCloseButtonClicked()
    {
        EquipBox.SetActive(false);
        stat.GetComponent<RectTransform>().position = new Vector3(566f, 540f);
        stat.SetActive(false);
        Debug.Log("장비 상자 닫힘");
        StorageBoxOpen();
    }

    public void ConsumablesBoxOpenButtonClicked()
    {
        panel_Box.SetActive(false);
        ConsumablesBox.SetActive(true);
        PotionSlot.SetActive(true);
        Debug.Log("소모품 상자 열림");
    }

    public void ConsumablesBoxCloseButtonClicked()
    {
        ConsumablesBox.SetActive(false);
        PotionSlot.SetActive(false);
        Debug.Log("소모품 상자 닫힘");
        StorageBoxOpen();
    }

    public void MaterialBoxOpenButtonClicked()
    {
        panel_Box.SetActive(false);
        MaterialBox.SetActive(true);
        Debug.Log("소모품 상자 열림");
    }

    public void MaterialBoxCloseButtonClicked()
    {
        MaterialBox.SetActive(false);
        Debug.Log("소모품 상자 닫힘");
        StorageBoxOpen();
    }

    // 거리 확인
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
