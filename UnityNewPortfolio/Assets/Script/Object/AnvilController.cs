using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class AnvilController : MonoBehaviour
{
    public Canvas canvas;
    public Canvas enhanceCanvas;
    public GameObject Inven;
    public Image itemImage;
    public Text informationText;

    GameObject target;
    Text text;

    GameObject player;
    PlayerController playerController;

    Slot selectedSlot;

    // Start is called before the first frame update
    void Start()
    {
        text = canvas.transform.Find("Name").GetComponent<Text>();

        target = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        canvas.enabled = false;
        enhanceCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.forward = target.transform.forward;

        if (canvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                enhanceCanvas.enabled = true;
                Inven.SetActive(true);
            }
        }

        if(enhanceCanvas.enabled)
        {
            //강화 창이 열려있을 경우 아이템 클릭시
            if(Input.GetMouseButtonDown(0)) 
            {
                ItemClick();
            }
        }

        playerController.isUiOpen = enhanceCanvas.enabled;
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
            enhanceCanvas.enabled = false;
            Inven.SetActive(false);
            ResetEnhanceInfo();
        }
    }

    public void CloseEnhance()
    {
        enhanceCanvas.enabled = false;
        Inven.SetActive(false);
        ResetEnhanceInfo();
    }

    private void ItemClick()
    {
        #region [ 아이템 확인 후 강화창으로 옮김 ]
        //인벤토리 상위 캔버스에 있는 그래픽 레이케스트 찾기
        GraphicRaycaster ray = Inven.transform.parent.GetComponent<GraphicRaycaster>();

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
                Slot slot = result.gameObject.GetComponent<Slot>();

                //해당 결과가 슬롯 일경우 true
                if (slot != null)
                {
                    Debug.Log("슬롯 클릭!");

                    selectedSlot = slot;

                    ItemInfo items = slot.Items;

                    //슬롯이 빈칸이 아닐경우 true
                    if (items != null)
                    {
                        if (items.item.itemType == Item.ObjectType.Weapon)
                        {
                            Debug.Log(items.item.itemName);

                            itemImage.sprite = items.item.itemImage;
                            itemImage.color = new Color(1, 1, 1, 1);

                            InfoTextView(items);
                        }
                        else
                        {
                            Debug.Log("무기 아님!");
                        }
                    }
                }
            }
        }
        #endregion
    }

    public void UpgradeItem()
    {
        if(selectedSlot != null)
        {
            ItemInfo items = selectedSlot.Items;
            if (items != null) 
            {
                items.item.itemEnhance++;
                InfoTextView(items);
            }
        }
    }

    private void InfoTextView(ItemInfo items)
    {
        informationText.text = "강화 수치\n" + items.item.itemEnhance.ToString().Trim() + "->" + (items.item.itemEnhance + 1).ToString().Trim();
    }

    private void ResetEnhanceInfo()
    {
        informationText.text = "강화 수치\n";
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);
    }
}
