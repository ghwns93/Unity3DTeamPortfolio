using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public enum ObjectType
    {
        Material,
        Weapon
    }

    public string itemName;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public ObjectType itemType;
}
