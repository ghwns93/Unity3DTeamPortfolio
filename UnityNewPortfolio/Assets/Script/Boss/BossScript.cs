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

    // 플레이어를 인식할 수 있는 범위
    public float sight = 20.0f;

    // 플레이어의 위치 (Transform)
    Transform player;

    // 공격 가능한 범위   
    public float range = 10.0f;

    // 이동 속도
    public float speed = 5.0f;

    // 공격 딜레이
    public float attackDelay = 7.0f;
    // 누적 시간
    float currentTime = 0.0f;

    // 이동 쿨다운 시간
    public float moveCooldown = 3.0f;
    // 공격 후 다음 이동까지의 누적 시간
    private float moveCooldownTime = 0.0f;

    // 적 공격력
    public int attackPower = 10;

    // 공격 카운터
    private int attackCount = 0;

    // 캐릭터 컨트롤러
    CharacterController cc;

    // 애니메이터 컴포넌트
    Animator anim;

    // 내비게이션 메쉬 컴포넌트
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
        //moveCooldownTime을 감소시키기
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
                LookAtPlayer(); // 공격 상태일 때 플레이어를 향해 방향 조정
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
        // 플레이어의 위치를 바라보는 방향을 계산합니다.
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // 캐릭터의 회전을 부드럽게 조정합니다.
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
        if (moveCooldownTime > 0) return; // 공격 후 이동 중이라면 공격하지 않음

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
            moveCooldownTime = moveCooldown; // 공격 후 이동 쿨다운 시작
        }
    }

    IEnumerator ReactivateNavMeshAgent(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (State != BossType.Damaged && State != BossType.Die)
        {
            State = BossType.Move;

            agent.isStopped = false;
            
            // 이동 애니메이션
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