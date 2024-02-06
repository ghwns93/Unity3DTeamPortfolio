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
    public Image stateBackground;
    public Text stateText;

    public int priceGold = 500;

    GameObject target;
    Text text;

    GameObject player;
    PlayerController playerController;

    ItemInfo selectedItem;

    IEnumerator nowCoroutine;

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
                playerController.isUiOpen = true;

                SoundManager.soundManager.SEPlay(SEType.OpenAnvil);

                Inven.SetActive(true);
                Inven.GetComponent<RectTransform>().position = new Vector3(1200f, 540f);
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

            if (enhanceCanvas.enabled == true)
            {
                enhanceCanvas.enabled = false;
                playerController.isUiOpen = false;
                Inven.SetActive(false);
                SoundManager.soundManager.SEPlay(SEType.OpenAnvil);
                ResetEnhanceInfo();
            }
        }
    }

    public void CloseEnhance()
    {
        enhanceCanvas.enabled = false;
        playerController.isUiOpen = false;
        Inven.SetActive(false);
        SoundManager.soundManager.SEPlay(SEType.OpenAnvil);
        ResetEnhanceInfo();
    }

    private void ItemClick()
    {
        #region [ 아이템 확인 후 강화창으로 옮김 ]
        //인벤토리 상위 캔버스에 있는 그래픽 레이케스트 찾기
        GraphicRaycaster ray = Inven.transform.parent.parent.GetComponent<GraphicRaycaster>();

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

                    selectedItem = slot.Items;

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
                else
                {
                    Debug.Log(result.gameObject.name);
                }
            }
        }
        #endregion
    }

    public void UpgradeItem()
    {
        if(selectedItem != null)
        {
            ItemInfo items = selectedItem;
            if (items != null) 
            {
                int money = PlayerState.Instance.Money;

                if (money >= priceGold)
                {
                    SoundManager.soundManager.SEPlay(SEType.ButtonAnvil);

                    //items.item.itemEnhance++;
                    items.enchant++;
                    money -= priceGold;
                    InfoTextView(items);

                    PlayerState.Instance.Money = money;
                }
                else
                {
                    if(nowCoroutine != null) StopCoroutine(nowCoroutine);
                    Debug.Log("골드 없음");
                    nowCoroutine = FadeOut();
                    StartCoroutine(nowCoroutine);
                }
            }
        }
    }

    private void InfoTextView(ItemInfo items)
    {
        informationText.text = "강화 수치\n" + items.enchant.ToString().Trim() + "->" + (items.enchant + 1).ToString().Trim();
    }

    private void ResetEnhanceInfo()
    {
        informationText.text = "강화 수치\n";
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);
    }

    IEnumerator FadeOut()
    {
        float fadeCount = 1; // 처음 알파값
        while(fadeCount > 0.0f) // 알파 값이 0일 될때까지 반복
        {
            fadeCount -= 0.005f;
            yield return new WaitForSeconds(0.005f); // 0.01초 마다 실행
            stateBackground.color = new Color(0, 0, 0, fadeCount);
            stateText.color = new Color(1,1,1, fadeCount);
        }
    }
}
