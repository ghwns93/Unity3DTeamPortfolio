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

    [HideInInspector]
    public int maxHp = 300;
    [HideInInspector]
    public int hp;
    [HideInInspector]
    public int power = 10;

    public GameObject fireBoll;

    public Transform firePoint;

    public bool isAttacking;

    public float pushBackForce = 10f; // 밀쳐내는 힘의 크기
    public float effectRadius = 10f; // 효과가 적용되는 반경

    // 적 HP 슬라이더
    public Slider hpSlider;

    // 플레이어를 인식할 수 있는 범위
    public float sight =20.0f;

    // 플레이어의 위치 (Transform)
    public Transform player;

    // 공격 가능한 범위   
    public float range = 10.0f;

    // 이동 속도
    public float speed = 2.5f;

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
        Debug.Log("2페이즈 start");
        attackPower = power;

        State = BossType.Idle;
        player = GameObject.Find("PlayerBody").transform;
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        isAttacking= false;

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
                Damaged();
                break;
            case BossType.Die:
                Die();
                break;
        }
        // 현재 HP를 슬라이더의 value에 반영한다
        hpSlider.value = (float)hp / (float)maxHp;
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
        Debug.Log("2페이즈 아이들");
        if (Vector3.Distance(transform.position, player.position) < sight)
        {
            State = BossType.Move;
            anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        Debug.Log("2페이즈 무브");
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

            power = 10;
        Debug.Log("2페이즈 발사");

                float distance = Vector3.Distance(transform.position, player.position);
                
                if (distance <= range)
                {
                    Debug.Log("발사2");
                
                    isAttacking = true;
                    power = 10;
                    anim.SetTrigger("Attack");
               
                    StartCoroutine(CastFireBoll());

                     currentTime= 0;
                    Debug.Log("Attack");

                }

            StartCoroutine(ReactivateNavMeshAgent(moveCooldown));
            moveCooldownTime = moveCooldown; // 공격 후 이동 쿨다운 시작
        }
     }

    IEnumerator CastFireBoll()
    {
        yield return new WaitForSeconds(0.2f);

        GameObject fireboll = Instantiate(fireBoll, firePoint.position, firePoint.rotation);
        Debug.Log("화염 생성");
    }


    IEnumerator ReactivateNavMeshAgent(float delay)
    {
        yield return new WaitForSeconds(delay);

        isAttacking = false;
        Debug.Log("발사3");

        if (State != BossType.Damaged && State != BossType.Die)
        {
            State = BossType.Move;

            agent.isStopped = false;

            // 이동 애니메이션
            anim.SetTrigger("AttackToMove");
        }
    }

    // 데미지 처리 함수
    public void HitEnemy(int hitPower)
    {

        Debug.Log("2페이즈 데미지 입기");
        // 플레이어의 공격력만큼 적 체력을 감소시켜준다
        hp -= hitPower;

        Debug.Log(hp);

        // 적 체력이 0보다 크면 피격 상태로 전환
        if (hp > 0)
        {
        Debug.Log("2페이즈 데미지 데미지드 메서드");
            State = BossType.Damaged;
            print("상태 전환 : Any State -> Damaged");

            // 피격 애니메이션 재생
            anim.SetTrigger("Damaged");

            Damaged();
        }
        // 그렇지 않다면 사망 상태로 전환
        else if(hp <= 0)
        {
        Debug.Log("2페이즈 데미지 죽음 메서드");
            State = BossType.Die;
            print("상태 전환 : Any State -> Die");
            anim.SetTrigger("Die");
            //Die();
        }
    }


    private void Damaged()
    {
        Debug.Log("2페이즈 데미지 메서드");
        // 피격 상태를 처리하는 코루틴 함수를 호출한다
        StartCoroutine(DamageProcess());
    }

    // 피격 상태 처리용 코루틴
    IEnumerator DamageProcess()
    {
        Debug.Log("2페이즈 데미지 코루틴");
        // 피격 애니메이션 재생 시간만큼 기다린다
        yield return new WaitForSeconds(1.0f);

        // 현재 상태를 이동으로 전환한다
        State = BossType.Move;
        print("상태 전환 : Damaged -> Move");
    }

    // 사망 상태
    void Die()
    {
        if (cc.enabled == true)
        {
            Debug.Log("사망");

            // 사망 상태를 처리하기 위한 코루틴을 실행한다
            StartCoroutine(DieProcess());
        }
    }

    // 사망 상태 처리용 코루틴
    IEnumerator DieProcess()
    {
        // 캐릭터 컨트롤러를 비활성화한다
        cc.enabled = false;

        QuestManager.Instance.nowQuest.questNowCount++;

        // 2초 동안 기다린 이후 자기자신을 제거한다
        yield return new WaitForSeconds(5.0f);
        print("소멸!");
        Destroy(gameObject);
    }

    public void ApplyPushBackEffect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // CharacterController 컴포넌트를 가져옵니다.
                CharacterController controller = hitCollider.GetComponent<CharacterController>();

                if (controller != null)
                {
                    // 밀쳐내기 효과를 적용하기 위한 시작 방향과 강도 설정
                    Vector3 pushDirection = (hitCollider.transform.position - transform.position).normalized;
                    pushDirection.y = 0; // 수직 방향으로는 힘을 가하지 않습니다.
                    StartCoroutine(PushBackCharacter(controller, pushDirection, pushBackForce, 0.1f)); // 0.5초 동안 밀쳐내기
                }
            }
        }
    }

    IEnumerator PushBackCharacter(CharacterController controller, Vector3 direction, float force, float duration)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            // CharacterController를 사용하여 오브젝트를 밀쳐냅니다.
            controller.Move(direction * force * Time.deltaTime);
            yield return null;
        }
    }

    // 디버깅을 위해 작용 범위를 시각화합니다. (에디터에서만 보임)
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