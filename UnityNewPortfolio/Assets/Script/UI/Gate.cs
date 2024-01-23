using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject Worldmap;

    public float length = 5.0f;

    GameObject player;

    public GameObject canvas;

    bool WorldmapOpened = false;   // false = ´İÈù»óÅÂ  true = ¿­¸°»óÅÂ

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas.SetActive(false);
    }

    void Update()
    {
        if (CheckLength(player.transform.position))
        {
            canvas.SetActive(true);

            if (!WorldmapOpened)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Worldmap.SetActive(true);
                    WorldmapOpened = true;
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Worldmap.SetActive(false);
                    WorldmapOpened = false;
                }
            }
        }
        else
        {
            canvas.SetActive(false);
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
