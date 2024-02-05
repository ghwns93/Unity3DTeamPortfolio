using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;
using static UnityEngine.Rendering.DebugUI;
public class BossScript2 : MonoBehaviour
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
    BossScript bs;

    public int maxHp = 300;
    public int hp;
    public int power;

    public bool isAttacking;

    public float pushBackForce = 10f; // ���ĳ��� ���� ũ��
    public float effectRadius = 10f; // ȿ���� ����Ǵ� �ݰ�

    // �� HP �����̴�
    public Slider hpSlider;

    // �÷��̾ �ν��� �� �ִ� ����
    public float sight = 20.0f;

    // �÷��̾��� ��ġ (Transform)
    public Transform player;

    // ���� ������ ����   
    public float range = 15.0f;

    // �̵� �ӵ�
    public float speed = 2.5f;

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
        attackPower = power;

        State = BossType.Idle;
        player = GameObject.Find("PlayerBody").transform;
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        hp = bs.hp;
    }
    void Update()
    {
        isAttacking= false;

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
                Damaged();
                break;
            case BossType.Die:
                Die();
                break;
        }
        // ���� HP�� �����̴��� value�� �ݿ��Ѵ�
        hpSlider.value = (float)hp / (float)maxHp;
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

            power = 10;

            attackCount++;

            /////
            ///
            
            StartCoroutine(ReactivateNavMeshAgent(moveCooldown));

            currentTime = 0;
            moveCooldownTime = moveCooldown; // ���� �� �̵� ��ٿ� ����
        }
    }

    IEnumerator ReactivateNavMeshAgent(float delay)
    {
        yield return new WaitForSeconds(delay);

        isAttacking = false;

        if (State != BossType.Damaged && State != BossType.Die)
        {
            State = BossType.Move;

            agent.isStopped = false;
            
            // �̵� �ִϸ��̼�
            anim.SetTrigger("AttackToMove");
        }
    }

    // �÷��̾��� ������ ó�� �Լ�
    public void AttackAction()
    {
        player.GetComponent<PlayerState>().DamageAction(power);

    }


    // ������ ó�� �Լ�
    public void HitEnemy(int hitPower)
    {

        // �÷��̾��� ���ݷ¸�ŭ �� ü���� ���ҽ����ش�
        hp -= hitPower;

        Debug.Log(hp);

        // �� ü���� 0���� ũ�� �ǰ� ���·� ��ȯ
        if (hp > 0)
        {
            State = BossType.Damaged;
            print("���� ��ȯ : Any State -> Damaged");

            // �ǰ� �ִϸ��̼� ���
            anim.SetTrigger("Damaged");

            Damaged();
        }
        // �׷��� �ʴٸ� ��� ���·� ��ȯ
        else
        {
            State = BossType.Die;
            print("���� ��ȯ : Any State -> Die");
            anim.SetTrigger("Die");
            Die();
        }
    }


    private void Damaged()
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
        State = BossType.Move;
        print("���� ��ȯ : Damaged -> Move");
    }

    // ��� ����
    void Die()
    {
        Debug.Log("���");

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

    public void ApplyPushBackEffect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // CharacterController ������Ʈ�� �����ɴϴ�.
                CharacterController controller = hitCollider.GetComponent<CharacterController>();

                if (controller != null)
                {
                    // ���ĳ��� ȿ���� �����ϱ� ���� ���� ����� ���� ����
                    Vector3 pushDirection = (hitCollider.transform.position - transform.position).normalized;
                    pushDirection.y = 0; // ���� �������δ� ���� ������ �ʽ��ϴ�.
                    StartCoroutine(PushBackCharacter(controller, pushDirection, pushBackForce, 0.1f)); // 0.5�� ���� ���ĳ���
                }
            }
        }
    }

    IEnumerator PushBackCharacter(CharacterController controller, Vector3 direction, float force, float duration)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            // CharacterController�� ����Ͽ� ������Ʈ�� ���ĳ��ϴ�.
            controller.Move(direction * force * Time.deltaTime);
            yield return null;
        }
    }

    // ������� ���� �ۿ� ������ �ð�ȭ�մϴ�. (�����Ϳ����� ����)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, effectRadius);
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