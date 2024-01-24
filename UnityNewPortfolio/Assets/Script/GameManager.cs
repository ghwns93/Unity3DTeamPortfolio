using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject startPos;
    GameObject player;

    public GameObject playerPrefeb;

    // Start is called before the first frame update
    void Awake()
    {
        startPos = GameObject.Find("UserStartPos");
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null )
        {
            player = Instantiate(playerPrefeb) as GameObject;
        }
    }

    private void Start()
    {
        player.transform.position = startPos.transform.position;
    }
}
