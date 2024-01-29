using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startPos;
    GameObject player;
    GameObject playerBody;

    public GameObject playerPrefeb;

    // Start is called before the first frame update
    void Awake()
    {
        if (startPos == null) startPos = GameObject.Find("UserStartPos");
        player = GameObject.Find("Player");

        playerBody = player.transform.Find("PlayerBody").gameObject;
    }

    private void Start()
    {
        playerBody.transform.localPosition = Vector3.zero;
        DontDestroyOnLoad(player);
        //DontDestroyOnLoad(gameObject);

        player.transform.position = startPos.transform.position;
    }
}
