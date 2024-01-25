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

    // Start is called before the first frame update
    void Awake()
    {
        startPos = GameObject.Find("UserStartPos");
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null )
        {
            player = Instantiate(playerPrefeb);
            playerBody = player.transform.Find("PlayerBody").gameObject;
        }

        DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player.transform.position = startPos.transform.position;

        if (map != null)
        {
            map.SetActive(false);
            playerPos = map.transform.Find("PlayerPos").gameObject;
        }
    }

    private void Update()
    {
        if(map != null)
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                map.SetActive(!map.activeSelf);
            }

            playerBody.SetActive(!map.activeSelf);

            playerPos.transform.position = playerBody.transform.position;
            playerPos.transform.position += new Vector3(0, 100, 0);

        }
    }
}
