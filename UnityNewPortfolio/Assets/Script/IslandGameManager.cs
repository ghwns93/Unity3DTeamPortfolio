using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandGameManager : MonoBehaviour
{
    GameObject player;
    GameObject playerBody;
    GameObject playerPos;

    public GameObject map;
    public Text text;

    public bool btnClicked = false;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        playerBody = player.transform.Find("PlayerBody").gameObject;
    }

    private void Start()
    {
        if (map != null)
        {
            map.SetActive(true);
            playerPos = map.transform.Find("PlayerPos").gameObject;
        }
    }

    private void Update()
    {
        if (map != null)
        {
            if (!btnClicked)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    map.SetActive(!map.activeSelf);

                    text.text = "이동할 위치를\n선택해주세요";
                }

                playerBody.SetActive(!map.activeSelf);

                playerPos.transform.position = player.transform.position;
                playerPos.transform.position += new Vector3(0, 100, 0);
            }
            else
            {
                map.SetActive(false);
                playerBody.SetActive(true);
                playerPos.transform.position = player.transform.position;
                playerPos.transform.position += new Vector3(0, 100, 0);

                btnClicked = false;
            }
        }
    }
}
