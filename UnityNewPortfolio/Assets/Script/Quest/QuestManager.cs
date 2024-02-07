using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class QuestManager : MonoBehaviour
{
    public Quest nowQuest;
    public List<Quest> allQuests;

    GameObject Result;

    private static QuestManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        var obj = FindObjectsOfType<QuestManager>();

        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        ResetAllQuest();
    }

    private void LateUpdate()
    {
        if(nowQuest != null) 
        {
            if (nowQuest.questClear == false)
            {
                if (nowQuest.questCount <= nowQuest.questNowCount)
                {
                    Debug.Log("퀘스트 완료!");

                    nowQuest.questClear = true;

                    PlayerState.Instance.Money += nowQuest.questPriceGold;

                    Result = GameObject.Find("Canvas_UI_final").transform.Find("Result").gameObject;
                    Result.SetActive(true);

                    UIResult uIResult = Result.GetComponent<UIResult>();
                    uIResult.questName.text = nowQuest.questName;
                    uIResult.questDesc.text = nowQuest.questDesc;

                    Timer time = GameObject.Find("Canvas_UI_final").GetComponent<Timer>();
                    time.questclear = true;

                    PlayerController pc = GameObject.Find("PlayerBody").GetComponent<PlayerController>();
                    pc.questClear = true;

                    SoundManager.soundManager.PlayBGM(BGMType.QuestClear);

                    nowQuest = null;
                }
            }
        }
    }

    public static QuestManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }

    public void ResetAllQuest()
    {
        foreach (Quest quest in allQuests)
        {
            quest.questNowCount = 0;
            quest.questClear = false;
        }
    }
}
