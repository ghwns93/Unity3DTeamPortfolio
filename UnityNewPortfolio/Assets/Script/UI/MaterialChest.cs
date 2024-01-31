using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChest : MonoBehaviour
{
    [SerializeField]
    private GameObject slotparent;
    [SerializeField]
    private Slot[] slots;

    private List<ItemInfo> mItem = new List<ItemInfo>();

    private static MaterialChest Mchest = null;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotparent.GetComponentsInChildren<Slot>();
    }
#endif

    private void Awake()
    {
        if (null == Mchest)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            Mchest = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        FreshSlot();
    }    

    public void AddItem(Item item, int count)
    {
        if (mItem.Count < slots.Length)
        {
            bool isDup = false;
            foreach (var n in mItem)
            {
                if (n.item == item)
                {
                    n.count += count;
                    isDup = true;
                }
            }

            if (!isDup)
            {
                mItem.Add(new ItemInfo { item = item, count = count });
            }

            FreshSlot();
        }
        else
        {
            Debug.Log("슬롯이 가득 차 있습니다.");
        }
    }

    public void FreshSlot()
    {
        int i = 0;

        for (; i < mItem.Count && i < slots.Length; i++)
        {
            slots[i].Items = mItem[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Items = null;
        }
    }

    public static MaterialChest Instance
    {
        get
        {
            if (null == Mchest)
            {
                return null;
            }
            return Mchest;
        }
    }
}
