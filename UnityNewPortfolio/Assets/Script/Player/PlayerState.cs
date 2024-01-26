using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //�̱��� ����
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
    
    // ////////////////////////
    public int power = 10;
    public void DamageAction(int damage)
    {
        // ���� ���ݷ¸�ŭ �÷��̾��� ü���� ��´�
        hp -= damage;

        // �÷��̾��� HP�� 0���� ũ�� �ǰ� ȿ�� ON
        if (hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        Debug.Log("�ǰ�����Ʈ ON");

        // 0.3�ʰ� ����Ѵ�
        yield return new WaitForSeconds(0.3f);

        Debug.Log("�ǰ�����Ʈ OFF");
    }

    // �� ������ ���޹��� ��ũ��Ʈ
    public Enemy_VS efsm;

    // �÷��̾�� �������� ���� �̺�Ʈ �Լ�
    public void PlayerHit()
    {
        efsm.AttackAction();
    }
    // ////////////////////////


    void Awake()
    {
        if(playerInstance == null) 
        {
            playerInstance = this;
            //DontDestroyOnLoad(this.gameObject);
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
