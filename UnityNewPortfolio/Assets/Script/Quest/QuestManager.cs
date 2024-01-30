using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest nowQuest;

    public GameObject questUi;

    private Text qProgress;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        questUi = GameObject.Find("Image_Quest").gameObject;
        if(questUi != null) qProgress = questUi.transform.GetChild(3).GetComponent<Text>();

        ShowQuest();
    }

    private void LateUpdate()
    {
        if(nowQuest != null && qProgress != null)
        {
            qProgress.text = nowQuest.questNowCount.ToString() + "/" + nowQuest.questCount.ToString();
        }
    }

    private void ShowQuest()
    {
        if (nowQuest != null)
        {
            Text qName = questUi.transform.GetChild(1).GetComponent<Text>();
            Text qDesc = questUi.transform.GetChild(2).GetComponent<Text>();

            qName.text = nowQuest.questName;
            qDesc.text = nowQuest.questDesc;
            qProgress.text = nowQuest.questNowCount.ToString() + "/" + nowQuest.questCount.ToString();
        }
    }
}
