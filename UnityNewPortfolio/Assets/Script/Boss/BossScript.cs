using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{
    enum BossType
    {
        Idle,
        Move,
        Attack,
        Damaged,
        Die
    }

    BossType State;

    public int maxHp = 300;
    public int hp = 300;

    // �÷��̾ �ν��� �� �ִ� ����
    public float sight = 20.0f;

    // �÷��̾��� ��ġ (Transform)
    Transform player;

    // ���� ������ ����   
    public float range = 10.0f;

    // �̵� �ӵ�
    public float speed = 5.0f;

    // ���� ������
    public float attackDelay = 7.0f;
    // ���� �ð�
    float currentTime = 0.0f;

    // �̵� ��ٿ� �ð�
    public float moveCooldown = 3.0f;
    // ���� �� ���� �̵������� ���� �ð�
    private float moveCooldownTime = 0.0f;

    // �� ���ݷ�
    public int attackPower = 10;

    // ���� ī����
    private int attackCount = 0;

    // ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    // �ִϸ����� ������Ʈ
    Animator anim;

    // ������̼� �޽� ������Ʈ
    NavMeshAgent agent;

    void Start()
    {
        State = BossType.Idle;
        player = GameObject.Find("PlayerBody").transform;
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //moveCooldownTime�� ���ҽ�Ű��
        if (moveCooldownTime > 0)
        {
            moveCooldownTime -= Time.deltaTime;
        }

        switch (State)
        {
            case BossType.Idle:
                Idle();
                break;
            case BossType.Move:
                Move();
                break;
            case BossType.Attack:
                Attack();
                LookAtPlayer(); // ���� ������ �� �÷��̾ ���� ���� ����
                break;
            case BossType.Damaged:
                //Damaged();
                break;
            case BossType.Die:
                //Die();
                break;
        }
    }
    void LookAtPlayer()
    {
        // �÷��̾��� ��ġ�� �ٶ󺸴� ������ ����մϴ�.
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // ĳ������ ȸ���� �ε巴�� �����մϴ�.
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < sight)
        {
            State = BossType.Move;
            anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > range)
        {
            agent.isStopped = false;
            agent.destination = player.position;

            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
        else if (moveCooldownTime <= 0)
        {
            State = BossType.Attack;
            anim.SetTrigger("MoveToAttackDelay");
        }

    }

    void Attack()
    {
        if (moveCooldownTime > 0) return; // ���� �� �̵� ���̶�� �������� ����

        currentTime += Time.deltaTime;

        if (currentTime > attackDelay)
        {
            agent.isStopped = true;

            attackCount++;
            if (attackCount % 6 == 0)
            {
                attackCount= 0;
                anim.SetTrigger("Skill");
                Debug.Log("Skill Attack");
            }
            else
            {
                int random = Random.Range(0, 3) + 1;

                float distance = Vector3.Distance(transform.position, player.position);
                if (distance <= range && random == 1)
                {
                    anim.SetTrigger("CloseAttack");
                    Debug.Log("Close Attack");

                }
                
                else if (distance <= range  && random == 2)
                {
                    anim.SetTrigger("MiddleAttack");
                    Debug.Log("Middle Attack");
                }
                else if (distance <= range && random == 3)
                {
                    anim.SetTrigger("FarAttack");
                    Debug.Log("Far Attack");
                }
                
            }

            StartCoroutine(ReactivateNavMeshAgent(moveCooldown));

            currentTime = 0;
            moveCooldownTime = moveCooldown; // ���� �� �̵� ��ٿ� ����
        }
    }

    IEnumerator ReactivateNavMeshAgent(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (State != BossType.Damaged && State != BossType.Die)
        {
            State = BossType.Move;

            agent.isStopped = false;
            
            // �̵� �ִϸ��̼�
            anim.SetTrigger("AttackToMove");
        }
    }

    void OnAnimatorMove()
    {
        if (agent.isStopped)
        {
            Vector3 newPosition = transform.position + anim.deltaPosition;
            Quaternion newRotation = transform.rotation * anim.deltaRotation;

            transform.position = newPosition;
            transform.rotation = newRotation;
        }
    }
}