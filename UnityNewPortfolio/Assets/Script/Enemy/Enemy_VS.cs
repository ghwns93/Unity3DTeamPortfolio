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

    // ��ȸ ��������Ʈ
    public Transform[] waypoints;
   
    
    private int destPoint = 0;
   

    // Start is called before the first frame update
    void Start()
    {


        Unitname = "����ŷ ��Ż��";
        UnitKey = 11111;
        maxHp = 100;
        hp = 100;
        power = 10.0f;
        defence = 5.0f;
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
                //Patrol();
                break;
            case EnemyState.Move0:
                Move0();
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

        if(E_State==EnemyState.Move0)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GoToNextWaypoint();
        }


        // ���� HP�� �����̴��� value�� �ݿ��Ѵ�
        //hpSlider.value = (float)hp / (float)maxHp;
    }

    private void Idle()
    {
            //enum ������ ���� ��ȯ
            E_State = EnemyState.Move0;
            print("���� ��ȯ : Idle -> Move");

            //�̵� �ִϸ��̼� ��ȯ�ϱ�
            anim.SetTrigger("IdleToMove");
    }

    private void Move0()
    {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.autoBraking = false;
            GoToNextWaypoint();
     }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;
        agent.destination = waypoints[destPoint].position;
        destPoint = (destPoint + 1) % waypoints.Length;
    }
}
