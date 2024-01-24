using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

public class Enemy_VS : EnemyStat
{
    // �÷��̾� ��ũ��Ʈ
    PlayerState pStat;
        
    // �� ���� ����ü
    EnemyState E_State;
    
    // �÷��̾� ��ġ
    Transform player;

    // �� ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    // ���� �ð�
    protected float currentTime = 0.0f;

    // ������
    protected float attackDelay = 2.0f;

    // �� �ʱ� ��ġ
    Vector3 originPos;
    Quaternion originRot;

    // �ִϸ����� ������Ʈ
    Animator anim;

    // ������̼� �޽� ������Ʈ
    NavMeshAgent agent;

    // ��ȸ ��������Ʈ
    public Transform[] points;

    // �ش� ��ǥ������ �ε���
    private int destPoint = 0;


    // Start is called before the first frame update
    void Start()
    {
        Unitname = "����ŷ ��Ż��";
        UnitKey = 11111;
        maxHp = 100;//LevelingStat(100 , pStat.Level.ge);
        hp = maxHp;//LevelingStat(100, pStat.Level);
        power = 10.0f;//LevelingStat(10.0f, pStat.Level); ;
        defence = 5.0f;//LevelingStat(5.0f, pStat.Level);
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
        anim = GetComponentInChildren<Animator>();

        // ������̼� ������Ʈ ������Ʈ ��������
        agent = GetComponent<NavMeshAgent>();

        
        // �ڵ����� ������ ������ ����
        agent.autoBraking = false;

        GotoNextPoint();
        
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
                //Move0();
                break;
            case EnemyState.Move1:
                //Move1();
                break;
            case EnemyState.Find:
                //Find();
                break;
            case EnemyState.Attack0:
                //Attack0();
                break;
            case EnemyState.Attack1:
                //Attack1();
                break;
            case EnemyState.Attack2:
                //Attack2();
                break;
            case EnemyState.Attack3:
                //Attack3();
                break;
            case EnemyState.Guard:
                //Guard();
                break;
            case EnemyState.AttackDelay:
                //AttackDelay();
                break;
            case EnemyState.Reload:
                //Reload();
                break;
            case EnemyState.Return:
                //Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.LowMorale:
                //LowMorale();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
    }

    private void Idle()
    {
            print("���� : Idle");
            //enum ������ ���� ��ȯ
            E_State = EnemyState.Patrol;
            print("���� ��ȯ : Idle -> Patrol");

            //�̵� �ִϸ��̼� ��ȯ�ϱ�
            anim.SetTrigger("IdleToPatrol");
    }

    private void Patrol()
    {
            print("���� : Patrol");

        
        // Agent�� ���� �������� ���� �����ߴٸ�, ���� �������� �̵�
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
        

    }

    
    // ������ ��ȸ �޼���
    void GotoNextPoint()
    {
        // ����Ʈ�� �������� �ʾҴٸ�, ��ȯ
        if (points.Length == 0)
            return;

        // Agent�� ������ �������� ����Ű���� ����
        agent.destination = points[destPoint].position;

        // �迭 ���� ���� ��ġ�� ������ �ε����� ��ȯ
        destPoint = (destPoint + 1) % points.Length;
    }
    

}
