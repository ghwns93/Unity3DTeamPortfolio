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
            //��ȭ â�� �������� ��� ������ Ŭ����
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
        #region [ ������ Ȯ�� �� ��ȭâ���� �ű� ]
        //�κ��丮 ���� ĵ������ �ִ� �׷��� �����ɽ�Ʈ ã��
        GraphicRaycaster ray = Inven.transform.parent.GetComponent<GraphicRaycaster>();

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
                Slot slot = result.gameObject.GetComponent<Slot>();

                //�ش� ����� ���� �ϰ�� true
                if (slot != null)
                {
                    Debug.Log("���� Ŭ��!");

                    selectedSlot = slot;

                    ItemInfo items = slot.Items;

                    //������ ��ĭ�� �ƴҰ�� true
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
                            Debug.Log("���� �ƴ�!");
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
        informationText.text = "��ȭ ��ġ\n" + items.item.itemEnhance.ToString().Trim() + "->" + (items.item.itemEnhance + 1).ToString().Trim();
    }

    private void ResetEnhanceInfo()
    {
        informationText.text = "��ȭ ��ġ\n";
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);
    }
}
