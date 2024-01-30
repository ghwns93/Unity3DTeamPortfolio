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
            //강화 창이 열려있을 경우 아이템 클릭시
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
        #region [ 아이템 확인 후 강화창으로 옮김 ]
        //인벤토리 상위 캔버스에 있는 그래픽 레이케스트 찾기
        GraphicRaycaster ray = questList.transform.parent.parent.GetComponent<GraphicRaycaster>();

        if (ray != null)
        {
            //포인터 이벤트 생성
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            //포인터 이벤트로 받을 결과 리스트 생성
            List<RaycastResult> results = new List<RaycastResult>();

            ray.Raycast(pointerEventData, results);

            //결과 리스트가 여러개 일수 있으니 전체 확인
            foreach (RaycastResult result in results)
            {
                QuestSlot qSlot = result.gameObject.GetComponent<QuestSlot>();

                if(qSlot != null)
                {
                    Debug.Log("퀘스트 있다!");
                }
            }
        }
        #endregion
    }
}
