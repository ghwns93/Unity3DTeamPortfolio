using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public GameObject itemPrefab;

    public int count = 0;
}