using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestSlot : MonoBehaviour
{
    Quest quest;

    Text questTitle;
    Image background;

    private void Start()
    {
        questTitle = transform.GetChild(0).GetComponent<Text>();
        background = GetComponent<Image>();
    }

    public Quest Quests
    {
        get { return quest; }
        set
        {
            quest = value;
            if (quest != null)
            {
                questTitle.text = quest.questName;
                background.color = new Color(1, 1, 1, 1);
            }
            else
            {
                questTitle.text = "";
                background.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
