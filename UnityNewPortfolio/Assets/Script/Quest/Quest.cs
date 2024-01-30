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
    [TextArea] public string questDesc;
}
