using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject startPos;
    GameObject player;
    GameObject playerBody;
    GameObject playerPos;

    public GameObject map;
    public GameObject playerPrefeb;

    bool onceTime = true;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void Start()
    {
        startPos = GameObject.Find("UserStartPos");
        Debug.Log(startPos.transform.position); 
        player = GameObject.Find("Player");

        playerBody = player.transform.Find("PlayerBody").gameObject;
        DontDestroyOnLoad(player);
        //DontDestroyOnLoad(gameObject);

        player.transform.position = startPos.transform.position;

        if (map != null)
        {
            map.SetActive(false);
            playerPos = map.transform.Find("PlayerPos").gameObject;
        }
    }

    private void Update()
    {
        if (map != null)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                map.SetActive(!map.activeSelf);
            }

            playerBody.SetActive(!map.activeSelf);

            playerPos.transform.position = player.transform.position;
            playerPos.transform.position += new Vector3(0, 100, 0);

        }
    }
}
