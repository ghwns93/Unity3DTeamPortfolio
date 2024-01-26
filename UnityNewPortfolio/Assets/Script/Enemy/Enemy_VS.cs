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
        power = 10;//LevelingStat(10.0f, pStat.Level); ;
        defence = 5.0f;//LevelingStat(5.0f, pStat.Level);
        speed = 3.0f;
        exp = 50.0f;
        sight = 15.0f;
        lostSight = 22.0f;
        attackRange1 = 1.0f;
        attackRange2 = 2.5f;
        morale = 100.0f;


        // ������ ���´� ��� ���
        E_State = EnemyState.Idle;

        // �÷��̾� �±��� ������Ʈ�� ��ġ ��ǥ ��������
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        

        // ĳ���� ��Ʈ�ѷ� ��������
        cc = GetComponent<CharacterController>();

        // �ʱ� ��ġ�� ȸ�� �� ����
        originPos = transform.position;
        originRot = transform.rotation;

        // �ִϸ����� ������Ʈ�� �������� (�ڽ� ������Ʈ�� ������Ʈ)
        anim = GetComponentInChildren<Animator>();

        waitTimer = waitTime;

        isFinded= false;
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
                Attack0();
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
                Damaged();
                break;
            case EnemyState.LowMorale:
                //LowMorale();
                break;
            case EnemyState.Die:
                Die();
                break;
        }

        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isFinded)
        {
            if (distanceToPlayer > lostSight)
            {
                isFinded = false;
            }
            else
            {
                MoveTowards(player.position);
            }
        }
        else
        {
            if (distanceToPlayer <= sight)
            {
                isFinded = true;
            }
            else
            {
                MoveTowards(originPos);
            }
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
        MoveToNextWaypoint();

        // �÷��̾� �߰� �� ���¸� �����ϰ� Patrol �޼ҵ带 �������´�.
        if (Vector3.Distance(transform.position, player.position) < sight)
        {
            isFinded = true;
            E_State = EnemyState.Find;
            print("���� ��ȯ : Patrol -> Find");
            anim.SetTrigger("PlayerFind");
            return; // �̵��� ���߰� Update ������ ���ư���.
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
        if (Vector3.Distance(transform.position, player.position) > attackRange1 && Vector3.Distance(transform.position, player.position) < lostSight)
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

        else
        {
            E_State = EnemyState.Attack0;
            print("���� ��ȯ : Move -> Attack");

            // ���� �ð��� ������ �ð���ŭ �̸� ������ѵд� (��� ����)
            currentTime = attackDelay;

            // ���� ��� �ִϸ��̼�
            anim.SetTrigger("MoveToAttackDelay");
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

    void Attack0()
    {
        speed= 0;
        // �÷��̾ ���� ���� ����� ������ �����Ѵ�
        if (Vector3.Distance(transform.position, player.position) < attackRange1)
        {
            // �����ð����� �����Ѵ�
            // ������ �ð��� �����̸� �Ѿ ������ �ʱ�ȭ
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                print("����!");
                AttackAction();
                currentTime = 0;

                // ���� �ִϸ��̼�
                anim.SetTrigger("StartAttack");
            }
        }
        // ���� ������ ����ٸ� ���� ���¸� Move�� ��ȯ�Ѵ� (���߰�)
        else
        {
            speed = 3.0f;
            E_State = EnemyState.Move0;
            print("���� ��ȯ : Attack -> Move");
            currentTime = 0;

            // �̵� �ִϸ��̼�
            anim.SetTrigger("AttackToMove");
        }
    }

    /////////////////////////////////

    void MoveToNextWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        // �̵��� �Ÿ��� ����մϴ�.
        float moveDistance = speed * Time.deltaTime;

        // ��������Ʈ�� ���� ĳ���� ȸ��
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        // CharacterController�� ����Ͽ� �̵��մϴ�.
        cc.Move(direction * moveDistance);

        // ��������Ʈ�� ����� ����������� Ȯ���մϴ�.
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (waitTimer > 0)
            {
                waitTimer -= Time.deltaTime;
            }
            else
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                waitTimer = waitTime;
            }
        }
    }

    // �÷��̾��� ������ ó�� �Լ�
    public void AttackAction()
    {
        player.GetComponent<PlayerState>().DamageAction(power);
        //Debug.Log(player.GetComponent<PlayerState>().Hp);
    }

    // ��� ����
    void Die()
    {
        // ���� ���� �ǰ� �ڷ�ƾ �Լ��� �����Ѵ�
        StopAllCoroutines();

        // ��� ���¸� ó���ϱ� ���� �ڷ�ƾ�� �����Ѵ�
        StartCoroutine(DieProcess());
    }

    // ��� ���� ó���� �ڷ�ƾ
    IEnumerator DieProcess()
    {
        // ĳ���� ��Ʈ�ѷ��� ��Ȱ��ȭ�Ѵ�
        cc.enabled = false;

        // 2�� ���� ��ٸ� ���� �ڱ��ڽ��� �����Ѵ�
        yield return new WaitForSeconds(2.0f);
        print("�Ҹ�!");
        Destroy(gameObject);
    }

    // ������ ó�� �Լ�
    public void HitEnemy(int hitPower)
    {
        // �ǰ�, ���, ���� ������ ��쿡�� �Լ� ��� ����
        if (E_State == EnemyState.Damaged ||
            E_State == EnemyState.Die)
        {
            return;
        }

        // �÷��̾��� ���ݷ¸�ŭ �� ü���� ���ҽ����ش�
        hp -= hitPower;

        Debug.Log(hp);

        // �� ü���� 0���� ũ�� �ǰ� ���·� ��ȯ
        if (hp > 0)
        {
            E_State = EnemyState.Damaged;
            print("���� ��ȯ : Any State -> Damaged");

            // �ǰ� �ִϸ��̼� ���
            anim.SetTrigger("Damaged");
            Damaged();
        }
        // �׷��� �ʴٸ� ��� ���·� ��ȯ
        else
        {
            E_State = EnemyState.Die;
            print("���� ��ȯ : Any State -> Die");

            // ��� �ִϸ��̼� ���
            anim.SetTrigger("Die");
            Die();
        }
    }

    // �ǰ� ����
    void Damaged()
    {
        // �ǰ� ���¸� ó���ϴ� �ڷ�ƾ �Լ��� ȣ���Ѵ�
        StartCoroutine(DamageProcess());
    }

    // �ǰ� ���� ó���� �ڷ�ƾ
    IEnumerator DamageProcess()
    {
        // �ǰ� �ִϸ��̼� ��� �ð���ŭ ��ٸ���
        yield return new WaitForSeconds(1.0f);

        // ���� ���¸� �̵����� ��ȯ�Ѵ�
        E_State = EnemyState.Move0;
        print("���� ��ȯ : Damaged -> Move");
    }

void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }


}
