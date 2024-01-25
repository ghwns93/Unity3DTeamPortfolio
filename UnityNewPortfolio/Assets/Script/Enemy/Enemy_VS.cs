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

    public Transform[] waypoints; // ��������Ʈ �迭
    
    public float waitTime = 0.5f; // �� ��������Ʈ���� ����ϴ� �ð�

    private int currentWaypointIndex = 0; // ���� ��������Ʈ �ε���
    
    private float waitTimer;               // ��� Ÿ�̸�

    public float rotateSpeed = 2.0f; // ȸ�� �ӵ�

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
        lostSight = 20.0f;
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

        waitTimer = waitTime;

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
            case EnemyState.Find:
                Find();
                break;
            case EnemyState.Lost:
                Lost();
                break;
            case EnemyState.Move0:
                Move0();
                break;
            case EnemyState.Move1:
                //Move1();
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
        //MoveToNextWaypoint();

        if (Vector3.Distance(transform.position, player.position) < sight)
        {
            
            isFinded = true;
            // enum ������ ���� ��ȯ
            E_State = EnemyState.Find;
            print("���� ��ȯ : Patrol -> Find");

            // �̵� �ִϸ��̼� ��ȯ�ϱ�
            anim.SetTrigger("PlayerFind");
        }
    }

    private void Find()
    {
        E_State = EnemyState.Move0;
        print("������ȯ : Find -> Move0");

        anim.SetTrigger("FindToMove0");
    }

    private void Move0()
    {
        // ���� �÷��̾���� �Ÿ��� ���� ���� ���̶�� �÷��̾ ���� �̵��Ѵ�
        if (Vector3.Distance(transform.position, player.position) > sight )//&& Vector3.Distance(transform.position, player.position) < lostSight)
        {
            // �̵� ����
            Vector3 dir = (player.position - transform.position).normalized;

            // ĳ���� ��Ʈ�ѷ��� ����Ͽ� �̵�
            cc.Move(dir * speed * Time.deltaTime);

            // �÷��̾ ���� ���� ��ȯ
            transform.forward = dir;

        }

        // �÷��̾���� �Ÿ��� �ҽ� �Ÿ� ���̸� ������� patrol �Ѵ�.
        else if (Vector3.Distance(transform.position, player.position) > lostSight)
        {
            isFinded = false;

            // enum ������ ���� ��ȯ
            E_State = EnemyState.Lost;
            print("���� ��ȯ : Find -> Lost");
            // �̵� �ִϸ��̼� ��ȯ�ϱ�
            anim.SetTrigger("PlayerLost");
        }
    }

        private void Lost()
        {
            // enum ������ ���� ��ȯ
            E_State = EnemyState.Patrol;
            print("���� ��ȯ : Lost -> Patrol");

        // �̵� �ִϸ��̼� ��ȯ�ϱ�
        anim.SetTrigger("LostToPatrol");
    }

        // ���� ���¸� ����(Attack)���� ��ȯ�Ѵ�
        //else
        //{
        //E_State = EnemyState.Attack0;
        //print("���� ��ȯ : Move0 -> Attack0");

        // ���� �ð��� ������ �ð���ŭ �̸� ������ѵд� (��� ����)
        //currentTime = attackDelay;

        // ���� ��� �ִϸ��̼�
        //anim.SetTrigger("MoveToAttackDelay");
        //}





    /////////////////////////////////

    void MoveToNextWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        // ���� ��������Ʈ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // �̵� �������� ȸ��
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero) // ���� ���°� �ƴ� ���� ȸ��
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        }

        // �������� �����ߴ��� Ȯ��
        if (transform.position == targetPosition)
        {
            // ��� �ð��� �����ִٸ� ����
            if (waitTimer > 0)
            {
                waitTimer -= Time.deltaTime;
            }
            else
            {
                // ���� ��������Ʈ�� �̵�
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                waitTimer = waitTime;
            }
        }
    }


}
