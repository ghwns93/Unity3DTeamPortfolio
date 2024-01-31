using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool OpenBox = false;   // false = ��������  true = ��������

    public GameObject panel_Box;
    public GameObject playerUI;

    public GameObject PotionSlot;       // ���� ������

    public GameObject canvas;

    public float length = 5.0f;

    GameObject player;
    PlayerController pc;

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
        Debug.Log("â�� ����");
        OpenBox = true;
        pc.isUiOpen = true;
        playerUI.SetActive(false);
        PotionSlot.SetActive(false);
    }

    // �Ÿ� Ȯ��
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
