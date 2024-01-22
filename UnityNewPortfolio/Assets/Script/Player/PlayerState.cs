using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //ΩÃ±€≈Ê ±∏º∫
    private static PlayerState playerInstance = null;

    [SerializeField]
    private float hp;

    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField]
    private float stamina;

    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }

    [SerializeField]
    private int level;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    [SerializeField]
    private int money;

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    void Awake()
    {
        if(playerInstance == null) 
        {
            playerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static PlayerState Instance
    {
        get
        {
            if(null == playerInstance) return null;
            return playerInstance;
        }
    }
}
