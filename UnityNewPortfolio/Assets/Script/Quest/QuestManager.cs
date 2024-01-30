using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class QuestManager : MonoBehaviour
{
    public Quest nowQuest;

    private static QuestManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static QuestManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }

}
