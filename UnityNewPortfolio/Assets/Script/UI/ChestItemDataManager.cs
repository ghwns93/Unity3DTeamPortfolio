using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ChestItemDataManager
{
    private static ChestItemDataManager instance;

    public List<ItemInfo> eChestItems = new List<ItemInfo>();
    public List<ItemInfo> cChestItems = new List<ItemInfo>();
    public List<ItemInfo> mChestItems = new List<ItemInfo>();

    public ItemInfo weaponslot;
    public ItemInfo potionslot;

    public static ChestItemDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ChestItemDataManager();
            }
            return instance;
        }
    }

    public ChestItemDataManager()
    {
    }

    public void AddItem(Item item, int count)
    {
        if(item.itemType==Item.ObjectType.Weapon)
        {
            eChestItems.Add(new ItemInfo { item = item, count = count = 1 });

            //bool isDup = false;
            //foreach (var n in eChestItems)
            //{
            //    if (n.item == item)
            //    {
            //        n.count++;
            //        isDup = true;
            //    }
            //}

            //if (!isDup)
            //{
            //    eChestItems.Add(new ItemInfo { item = item, count = count = 1 });
            //}
        }
        else if (item.itemType == Item.ObjectType.Material)
        {
            bool isDup = false;
            foreach (var n in mChestItems)
            {
                if (n.item == item)
                {
                    n.count += count;
                    isDup = true;
                }
            }

            if (!isDup)
            {
                mChestItems.Add(new ItemInfo { item = item, count = count });
            }
        }
        else
        {
            bool isDup = false;
            foreach (var n in cChestItems)
            {
                if (n.item == item)
                {
                    n.count += count;
                    isDup = true;
                }
            }

            if (!isDup)
            {
                cChestItems.Add(new ItemInfo { item = item, count = count });
            }
        }
    }
}
