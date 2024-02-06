using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;
using static UnityEngine.Rendering.DebugUI;
public class BossScript : MonoBehaviour
{
    enum BossType
    {
        Idle,
        Move,
        Attack,
        Damaged,
        Phase2
    }

    BossType State;
    public GameObject hitPrefab;
    public int maxHp = 300;
    public int hp = 300;
    public int power;
    // 2������
    public bool phaseChange = false;
    [HideInInspector]
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
    public float range = 10.0f;

    // �̵� �ӵ�
    public float speed = 5.0f;

    // ���� ������
    public float attackDelay = 5.0f;
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
    Renderer renderer;

    public RuntimeAnimatorController boss2; // Inspector���� Boss2�� Animator Controller�� �Ҵ�

    private BossScript2 bossScript2; // �� ������Ʈ�� �پ��ִ� BossScript2 ������Ʈ�� ����

    void Start()
    {
        State = BossType.Idle;
        player = GameObject.Find("PlayerBody").transform;
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponentInChildren<Renderer>();

        // BossScript2 ������Ʈ�� ã�� ������ �����մϴ�.
        bossScript2 = GetComponent<BossScript2>();

        // �ʱ⿡�� BossScript2�� ��Ȱ��ȭ ���·� ����ϴ�.
        if (bossScript2 != null)
        {
            bossScript2.enabled = false;
        }

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
            case BossType.Phase2:
                Phase2();
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
            if (attackCount % 6 == 0)
            {
                isAttacking = true;
                attackCount= 0;
                anim.SetTrigger("Skill");
                power = 20;
                AttackAction();
                Debug.Log("Skill Attack");
            }
            else
            {
                int random = UnityEngine.Random.Range(0, 3) + 1;

                float distance = Vector3.Distance(transform.position, player.position);
                if (distance <= range && random == 1)
                {
                    isAttacking = true;
                    power = 10;
                    anim.SetTrigger("CloseAttack");
                    AttackAction();
                    Debug.Log("Close Attack");

                }
                
                else if (distance <= range  && random == 2)
                {
                    isAttacking = true;
                    power = 15;
                    anim.SetTrigger("MiddleAttack");
                    AttackAction();
                    Debug.Log("Middle Attack");
                }
                else if (distance <= range && random == 3)
                {
                    isAttacking = true;
                    power = 15;
                    anim.SetTrigger("FarAttack");
                    AttackAction();
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

        isAttacking = false;

        if (State != BossType.Damaged && State != BossType.Phase2)
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
        if (hp > maxHp/2)
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
            State = BossType.Phase2;
            print("���� ��ȯ : Any State -> Phase2");

            // 2������ ����
            Phase2();
            anim.SetTrigger("Phase2");
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


    private void Phase2()
    {
        State= BossType.Phase2;

        if (hp <= maxHp / 2)
        {
            Transform colorChange = transform.Find("0");



            if (renderer != null)
            {
                // Material�� Albedo ������ �����մϴ�.
                renderer.material.color = new Color32(255, 0, 0, 255); 
            }

            PlayParticleEffect();

            // ������2 ���¸� ó���ϴ� �ڷ�ƾ �Լ��� ȣ���Ѵ�
            ApplyPushBackEffect();
            bossScript2.hp = hp;
            StartCoroutine(Phase2Change());
        }
    }
    
    // �ǰ� ���� ó���� �ڷ�ƾ
    IEnumerator Phase2Change()
    {
         // �ǰ� �ִϸ��̼� ��� �ð���ŭ ��ٸ���
         yield return new WaitForSeconds(1.0f);

        ChangePhase();

        Destroy(this);
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

    public void ChangePhase()
    {
        // BossScript�� ��Ȱ��ȭ�մϴ�.
        this.enabled = false;

        // BossScript2�� Ȱ��ȭ�մϴ�.
        if (bossScript2 != null)
        {
            bossScript2.enabled = true;
        }

        // Animator ��Ʈ�ѷ��� �����մϴ�.
        if (anim != null && boss2 != null)
        {
            anim.runtimeAnimatorController = boss2;
        }
    }

    void PlayParticleEffect()
    {
        // ��ƼŬ�� ������ ��ġ
        Vector3 spawnPosition = transform.position;

        // ��ƼŬ�� �����ϰ� ������ �Ҵ�
        GameObject particleObject = Instantiate((GameObject)hitPrefab, spawnPosition, Quaternion.identity);

        // ��ƼŬ �ý��� ������Ʈ ��������
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();

        // ��ƼŬ�� ����Ǵ� ���� ���
        float duration = particleSystem.main.duration;

        Destroy(particleObject, duration);
    }
}