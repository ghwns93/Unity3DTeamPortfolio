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
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            Mchest = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
            //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameMgr�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameMgr)�� �������ش�.
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
            Debug.Log("������ ���� �� �ֽ��ϴ�.");
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
