using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class QuestBoardController : MonoBehaviour
{
    public Canvas canvas;
    public Canvas questCanvas;
    public GameObject questList;

    [SerializeField] Image mapImage;
    [SerializeField] Text questNameText;
    [SerializeField] Text questDescText;
    [SerializeField] Text questPriceGoldText;
    [SerializeField] Text questStoryText;

    GameObject target;
    GameObject player;
    PlayerController playerController;

    Quest selectedQuest;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        canvas.enabled = false;
        questCanvas.enabled = false;
    }

    private void Update()
    {
        canvas.transform.forward = target.transform.forward;

        if (canvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.E) && !questCanvas.enabled)
            {
                questCanvas.enabled = true;
                playerController.isUiOpen = true;
                QuestListSetting();
            }
            else if (Input.GetKeyDown(KeyCode.Escape)  && questCanvas.enabled)
            {
                questCanvas.enabled = false;
                playerController.isUiOpen = false;
            }
        }

        if (questCanvas.enabled)
        {
            //��ȭ â�� �������� ��� ������ Ŭ����
            if (Input.GetMouseButtonDown(0))
            {
                QuestClick();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            canvas.enabled = false;
            questCanvas.enabled = false;
            playerController.isUiOpen = false;
        }
    }

    private void QuestClick()
    {
        #region [ ������ Ȯ�� �� ��ȭâ���� �ű� ]
        //�κ��丮 ���� ĵ������ �ִ� �׷��� �����ɽ�Ʈ ã��
        GraphicRaycaster ray = questList.transform.parent.parent.GetComponent<GraphicRaycaster>();

        if (ray != null)
        {
            //������ �̺�Ʈ ����
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            //������ �̺�Ʈ�� ���� ��� ����Ʈ ����
            List<RaycastResult> results = new List<RaycastResult>();

            ray.Raycast(pointerEventData, results);

            //��� ����Ʈ�� ������ �ϼ� ������ ��ü Ȯ��
            foreach (RaycastResult result in results)
            {
                QuestSlot qSlot = result.gameObject.GetComponent<QuestSlot>();

                if(qSlot != null)
                {
                    QuestSelect(qSlot.Quests);
                }
            }
        }
        #endregion
    }

    private void QuestListSetting()
    {
        QuestManager.Instance.ResetAllQuest();

        for (int i = 0; i < questList.transform.childCount; i++)
        {
            QuestSlot questSlot = questList.transform.GetChild(i).GetComponent<QuestSlot>();

            questSlot.Quests = null;

            if(i < QuestManager.Instance.allQuests.Count)
            {
                questSlot.Quests = QuestManager.Instance.allQuests[i];
                if(i == 0) QuestSelect(questSlot.Quests);
            }
        }
    }

    public void CloseQuestBoard()
    {
        questCanvas.enabled = false;
        playerController.isUiOpen = false;
    }

    public void AcceptQuest()
    {
        QuestManager.Instance.nowQuest = selectedQuest;
        questCanvas.enabled = false;
        playerController.isUiOpen = false;
    }

    private void QuestSelect(Quest quest)
    {
        mapImage.sprite = quest.mapImage;
        questNameText.text = quest.questName;
        questDescText.text = quest.questDesc;
        questPriceGoldText.text = quest.questPriceGold.ToString() + "G";
        questStoryText.text = quest.questStory;

        selectedQuest = quest;
    }
}
