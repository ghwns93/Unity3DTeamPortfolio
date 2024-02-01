using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject startPos;
    GameObject player;
    GameObject playerBody;

    public GameObject playerPrefeb;

    // Start is called before the first frame update
    void Awake()
    {
        var obj = FindObjectsOfType<GameManager>();

        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.Find("Player")?GameObject.Find("Player"):Instantiate(playerPrefeb);
        player.name = "Player";

        playerBody = player.transform.Find("PlayerBody").gameObject;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);

        startPos = null;
    }

    void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player");
        playerBody = player.transform.Find("PlayerBody").gameObject;
        startPos = GameObject.FindGameObjectWithTag("UserStartPos");

        if (startPos != null) Debug.Log("startPos : " + startPos.transform.position);
        player.transform.position = startPos.transform.position;
        playerBody.transform.localPosition = Vector3.zero;

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
