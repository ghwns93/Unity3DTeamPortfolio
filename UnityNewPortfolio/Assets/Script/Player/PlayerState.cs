using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //싱글톤 구성
    private static PlayerState playerInstance = null;

    // 적 정보를 전달받을 스크립트
    public Enemy_VS efsm;

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
        if (playerInstance == null)
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
            if (null == playerInstance) return null;
            return playerInstance;
        }
    }

    // 플레이어에게 데미지를 입힐 이벤트 함수

    public void PlayerHit()
    {
        efsm.AttackAction();
    }

    public void DamageAction(int damage)
    {
        float playerHp = Hp;

        // 적의 공격력만큼 플레이어의 체력을 깎는다
        playerHp -= damage;

        Hp = playerHp;

        //Debug.Log(playerHp);

        // 플레이어의 HP가 0보다 크면 피격 효과 ON
        if (playerHp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        Debug.Log("피격이펙트 ON");

        // 0.3초간 대기한다
        yield return new WaitForSeconds(0.3f);

        Debug.Log("피격이펙트 OFF");
    }
}
