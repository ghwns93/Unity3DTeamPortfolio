using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public string questName;
    public GameObject questSubject;
    public int questCount;
    public int questNowCount;
    public int questPriceGold;
    public Sprite mapImage;

    public bool questClear = false;

    [TextArea] public string questDesc;
    [TextArea] public string questStory;
}
