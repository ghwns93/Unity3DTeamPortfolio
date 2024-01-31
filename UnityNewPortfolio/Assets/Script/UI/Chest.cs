using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Chest : MonoBehaviour
{
    public bool OpenBox = false;   // false = 닫힌상태  true = 열린상태

    public GameObject panel_Box;
    public GameObject playerUI;

    public GameObject PotionSlot;       // 포션 퀵슬롯

    public GameObject canvas;

    public float length = 5.0f;

    GameObject player;
    PlayerController pc;

    GameObject target;

    private void Awake()
    {
        canvas.SetActive(true);
        panel_Box.SetActive(true);
        
        panel_Box.SetActive(false);
        canvas.SetActive(false);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();

        target = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if(CheckLength(player.transform.position))
        {
            canvas.SetActive(true);
            canvas.transform.forward = target.transform.forward;

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
        pc.isUiOpen = true;
        playerUI.SetActive(false);
        PotionSlot.SetActive(false);
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
