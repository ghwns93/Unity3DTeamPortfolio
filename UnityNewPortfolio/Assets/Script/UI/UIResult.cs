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
    }

    public void GetallItem()        // 모두 받기 클릭
    {
        // UI 버튼 상태 조정
        getitem.interactable = false;
        villagemove.interactable = true;

        for (int i = 0; i < slots.Length; i++)
        {
            ItemInfo currentItem = slots[i].Items;

            if (currentItem != null)
            {
                switch (currentItem.item.itemType)
                {
                    case Item.ObjectType.Weapon:
                        ChestItemDataManager.Instance.AddItem(currentItem.item,1);
                        EquipChest.Instance.FreshSlot();
                        Inventory.Instance.items.Remove(currentItem);
                        break;

                    case Item.ObjectType.Material:
                        ChestItemDataManager.Instance.AddItem(currentItem.item, currentItem.count);
                        MaterialChest.Instance.FreshSlot();
                        Inventory.Instance.items.Remove(currentItem);
                        break;

                    case Item.ObjectType.Potion:
                        ChestItemDataManager.Instance.AddItem(currentItem.item, currentItem.count);
                        ConsumablesChest.Instance.FreshSlot();
                        Inventory.Instance.items.Remove(currentItem);
                        break;
                }

                // 인벤토리 슬롯 초기화
                Inventory.Instance.slots[i].Items = null;
            }
        }

        // 인벤토리 슬롯 갱신
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Items = null;
        }
    }



    public void VillageMoveButtonClicked()  // 마을로 이동 클릭
    {
        pc.isUiOpen = false;
        getitem.interactable = true;
        villagemove.interactable = false;

        LoadingSceneManager.LoadScene(nextScene);
    }
}
