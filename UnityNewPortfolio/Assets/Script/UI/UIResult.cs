using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIResult : MonoBehaviour
{
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

    public GameObject EquipchestPrefab;
    public GameObject ConsumablechestPrefab;
    public GameObject MaterialchestPrefab;

    GameObject player;
    PlayerController pc;

    public Button getitem;
    public Button villagemove;

    public string nextScene;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();

        pc.isUiOpen = true;

        slots = slotParent.GetComponentsInChildren<Slot>();

        for (int i = 0; i < Inventory.Instance.slots.Length; i++)
        {
            slots[i].Items = Inventory.Instance.slots[i].Items;
        }

        EquipchestPrefab.SetActive(true);
        ConsumablechestPrefab.SetActive(true);
        MaterialchestPrefab.SetActive(true);

        EquipchestPrefab.GetComponent<RectTransform>().position = new Vector3(3000f, 2000f);
        ConsumablechestPrefab.GetComponent<RectTransform>().position = new Vector3(3000f, 2000f);
        MaterialchestPrefab.GetComponent<RectTransform>().position = new Vector3(3000f, 2000f);
    }

    public void GetallItem()        // 모두 받기 클릭
    {
        for (int i = 0; i < Inventory.Instance.slots.Length; i++)
        {
            if (slots[i].Items != null)
            {
                while (slots[i].Items != null)
                {
                    if (slots[i].Items.item.itemType == Item.ObjectType.Weapon)
                    {
                        EquipChest.Instance.AcquireItem(slots[i].Items.item);
                        Inventory.Instance.slots[i].Items = null;
                        slots[i].Items = null;
                        return;
                    }
                    if (slots[i].Items.item.itemType == Item.ObjectType.Material)
                    {
                        MaterialChest.Instance.AcquireItem(slots[i].Items.item, slots[i].Items.count);
                        Inventory.Instance.slots[i].Items = null;
                        slots[i].Items = null;
                        return;
                    }

                    if (slots[i].Items.item.itemType == Item.ObjectType.Potion)
                    {
                        ConsumablesChest.Instance.AcquireItem(slots[i].Items.item, slots[i].Items.count);
                        Inventory.Instance.slots[i].Items = null;
                        slots[i].Items = null;
                        return;
                    }
                }                
            }
            if (slots[i].Items == null)
            {
                getitem.interactable = false;
                villagemove.interactable = true;
            }
        }        
    }

    public void VillageMoveButtonClicked()  // 마을로 이동 클릭
    {
        pc.isUiOpen = false;
        getitem.interactable = true;
        villagemove.interactable = false;

        EquipchestPrefab.SetActive(false);
        ConsumablechestPrefab.SetActive(false);
        MaterialchestPrefab.SetActive(false);

        LoadingSceneManager.LoadScene(nextScene);
    }
}
