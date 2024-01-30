using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfoScript : MonoBehaviour
{
    Quest nowQuest;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        nowQuest = QuestManager.Instance.nowQuest;
        ShowQuest();
    }

    private void ShowQuest()
    {
        if (nowQuest != null)
        {
            Text qName = transform.GetChild(1).GetComponent<Text>();
            Text qDesc = transform.GetChild(2).GetComponent<Text>();
            Text qProgress = transform.GetChild(3).GetComponent<Text>();

            qName.text = nowQuest.questName;
            qDesc.text = nowQuest.questDesc;
            qProgress.text = nowQuest.questNowCount.ToString() + "/" + nowQuest.questCount.ToString();
        }
    }
}
