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

    GameObject target;
    GameObject player;
    PlayerController playerController;

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
            }
            else if (Input.GetKeyDown(KeyCode.Escape)  && questCanvas.enabled)
            {
                questCanvas.enabled = false;
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

        playerController.isUiOpen = questCanvas.enabled;
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
                    Debug.Log("����Ʈ �ִ�!");
                }
            }
        }
        #endregion
    }
}
