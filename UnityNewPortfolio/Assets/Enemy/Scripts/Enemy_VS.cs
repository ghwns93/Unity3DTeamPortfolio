using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

public class Enemy_VS : EnemyStat
{

    PlayerState pStat;

    Slider hpSlider;

    EnemyState E_State;
    
    // �÷��̾� ��ġ
    public Transform player;

    // �� ĳ���� ��Ʈ�ѷ�
    public CharacterController cc;

    // ���� �ð�
    protected float currentTime = 0.0f;

    // ������
    protected float attackDelay;

    // �� �ʱ� ��ġ
    Vector3 originPos;
    Quaternion originRot;

    // �ִϸ����� ������Ʈ
    Animator anim;

    // ������̼� �޽� ������Ʈ
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {


        /*
        [[UnitKey]]
        
        ù° �ڸ� 0 - ���� NPC��
        ù° �ڸ� 1 - �� �ʵ�(���� ���� ����)

        ��° �ڸ� 0 - �������
        ��° �ڸ� 1 - ������
        ��° �ڸ� 2 - �񼱰���

        ��° �ڸ� 0 - ��������
        ��° �ڸ� 1 - �ٰŸ���
        ��° �ڸ� 2 - �߰Ÿ���
        ��° �ڸ� 3 - ���Ÿ���
        ��° �ڸ� 4 - ������(�ٰŸ�, ���Ÿ� ��� ���)

        ��° �ڸ� 0 - ���� ����
        ��° �ڸ� 1 - �ΰ���
        ��° �ڸ� 2 - ������
        ��° �ڸ� 3 - ���� ����
        ��° �ڸ� 4 - �ܼ� NPC

        �ټ�° �ڸ� 1 - ���� ���� ����

        [Ű �ڵ� ����]
        ex) ������ ������ ������ �ٰŸ� �ΰ�(�˰� ����) - 11111
        ex) ������ ������ ������ �ٰŸ� �ΰ�(�Ѽ� ��) - 11112
        ex) ������ ������ ������ �ٰŸ� �ΰ�(â) - 11211
        ex) �������� ������ ����� ���� - 00001
        ex) �������� ������ ����� ���� NPC - 00041
        ex) ������ ������ �񼱰� ������ ���̸��� - 12421
         
         */

        Unitname = "����ŷ ��Ż��";
        UnitKey = 11111;  
        maxHp = LevelingStat(100 , pStat.Level);
        hp = LevelingStat(100 , pStat.Level);
        power = LevelingStat(10.0f, pStat.Level);
        defence = LevelingStat(5.0f, pStat.Level);
        speed = 10.0f;
        exp = 50.0f;
        sight = 10.0f;
        attackRange1 = 2.5f;
        attackRange2 = 2.5f;
        morale = 100.0f;


        // ������ ���´� ��� ���
        E_State = EnemyState.Idle;

        // �÷��̾��� ��ġ ��ǥ ��������
        player = GameObject.Find("Player").transform;

        // ĳ���� ��Ʈ�ѷ� ��������
        cc = GetComponent<CharacterController>();

        // �ʱ� ��ġ�� ȸ�� �� ����
        originPos = transform.position;
        originRot = transform.rotation;

        // �ִϸ����� ������Ʈ�� �������� (�ڽ� ������Ʈ�� ������Ʈ)
        anim = transform.GetComponentInChildren<Animator>();

        // ������̼� ������Ʈ ������Ʈ ��������
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���¸� �˻��ϰ� ���º��� ������ ����� �����Ѵ�
        switch (E_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Move0:
                Move0();
                break;
            case EnemyState.Move1:
                Move1();
                break;
            case EnemyState.Find:
                Find();
                break;
            case EnemyState.Attack0:
                Attack0();
                break;
            case EnemyState.Attack1:
                Attack1();
                break;
            case EnemyState.Attack2:
                Attack2();
                break;
            case EnemyState.Attack3:
                Attack3();
                break;
            case EnemyState.Guard:
                Guard();
                break;
            case EnemyState.AttackDelay:
                AttackDelay();
                break;
            case EnemyState.Reload:
                Reload();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.LowMorale:
                LowMorale();
                break;
            case EnemyState.Die:
                Die();
                break;
        }

        // ���� HP�� �����̴��� value�� �ݿ��Ѵ�
        hpSlider.value = (float)hp / (float)maxHp;
    }

    private void Die()
    {
    }

    private void LowMorale()
    {
    }

    private void Damaged()
    {
    }

    private void Return()
    {
    }

    private void Reload()
    {
    }

    private void AttackDelay()
    {
    }

    private void Guard()
    {
    }

    private void Attack3()
    {
    }

    private void Attack2()
    {
    }

    private void Attack1()
    {
    }

    private void Attack0()
    {
    }

    private void Find()
    {
    }

    private void Move1()
    {
    }

    private void Move0()
    {
    }

    private void Patrol()
    {
    }

    private void Idle()
    {
        // ���� �÷��̾���� �Ÿ��� ������ ������ ���� ��� Move ���·� ��ȯ
        if (Vector3.Distance(transform.position, player.position) < 0)
        {
            /*
            // enum ������ ���� ��ȯ
            m_State = EnemyState.Move;
            print("���� ��ȯ : Idle -> Move");

            // �̵� �ִϸ��̼� ��ȯ�ϱ�
            anim.SetTrigger("IdleToMove");
             */
        }
    }
}
