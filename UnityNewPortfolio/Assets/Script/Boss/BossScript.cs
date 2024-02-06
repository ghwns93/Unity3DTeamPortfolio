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
    // 2페이즈
    public bool phaseChange = false;
    [HideInInspector]
    public bool isAttacking;

    public float pushBackForce = 10f; // 밀쳐내는 힘의 크기
    public float effectRadius = 10f; // 효과가 적용되는 반경


    // 적 HP 슬라이더
    public Slider hpSlider;

    // 플레이어를 인식할 수 있는 범위
    public float sight = 20.0f;

    // 플레이어의 위치 (Transform)
    public Transform player;

    // 공격 가능한 범위   
    public float range = 10.0f;

    // 이동 속도
    public float speed = 5.0f;

    // 공격 딜레이
    public float attackDelay = 5.0f;
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
    Renderer renderer;

    public RuntimeAnimatorController boss2; // Inspector에서 Boss2의 Animator Controller를 할당

    private BossScript2 bossScript2; // 이 오브젝트에 붙어있는 BossScript2 컴포넌트의 참조

    void Start()
    {
        State = BossType.Idle;
        player = GameObject.Find("PlayerBody").transform;
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponentInChildren<Renderer>();

        // BossScript2 컴포넌트를 찾아 참조를 저장합니다.
        bossScript2 = GetComponent<BossScript2>();

        // 초기에는 BossScript2를 비활성화 상태로 만듭니다.
        if (bossScript2 != null)
        {
            bossScript2.enabled = false;
        }

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
            case BossType.Phase2:
                Phase2();
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
            moveCooldownTime = moveCooldown; // 공격 후 이동 쿨다운 시작
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
            
            // 이동 애니메이션
            anim.SetTrigger("AttackToMove");
        }
    }

    // 플레이어의 데미지 처리 함수
    public void AttackAction()
    {
        player.GetComponent<PlayerState>().DamageAction(power);

    }


    // 데미지 처리 함수
    public void HitEnemy(int hitPower)
    {

        // 플레이어의 공격력만큼 적 체력을 감소시켜준다
        hp -= hitPower;

        Debug.Log(hp);

        // 적 체력이 0보다 크면 피격 상태로 전환
        if (hp > maxHp/2)
        {
            State = BossType.Damaged;
            print("상태 전환 : Any State -> Damaged");

            // 피격 애니메이션 재생
            anim.SetTrigger("Damaged");

            Damaged();
        }
        // 그렇지 않다면 사망 상태로 전환
        else
        {
            State = BossType.Phase2;
            print("상태 전환 : Any State -> Phase2");

            // 2페이즈 시작
            Phase2();
            anim.SetTrigger("Phase2");
        }
    }


    private void Damaged()
    {
        // 피격 상태를 처리하는 코루틴 함수를 호출한다
        StartCoroutine(DamageProcess());
    }

    // 피격 상태 처리용 코루틴
    IEnumerator DamageProcess()
    {
        // 피격 애니메이션 재생 시간만큼 기다린다
        yield return new WaitForSeconds(1.0f);

        // 현재 상태를 이동으로 전환한다
        State = BossType.Move;
        print("상태 전환 : Damaged -> Move");
    }


    private void Phase2()
    {
        State= BossType.Phase2;

        if (hp <= maxHp / 2)
        {
            Transform colorChange = transform.Find("0");



            if (renderer != null)
            {
                // Material의 Albedo 색상을 변경합니다.
                renderer.material.color = new Color32(255, 0, 0, 255); 
            }

            PlayParticleEffect();

            // 페이즈2 상태를 처리하는 코루틴 함수를 호출한다
            ApplyPushBackEffect();
            bossScript2.hp = hp;
            StartCoroutine(Phase2Change());
        }
    }
    
    // 피격 상태 처리용 코루틴
    IEnumerator Phase2Change()
    {
         // 피격 애니메이션 재생 시간만큼 기다린다
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

    public void ChangePhase()
    {
        // BossScript를 비활성화합니다.
        this.enabled = false;

        // BossScript2를 활성화합니다.
        if (bossScript2 != null)
        {
            bossScript2.enabled = true;
        }

        // Animator 컨트롤러를 변경합니다.
        if (anim != null && boss2 != null)
        {
            anim.runtimeAnimatorController = boss2;
        }
    }

    void PlayParticleEffect()
    {
        // 파티클을 생성할 위치
        Vector3 spawnPosition = transform.position;

        // 파티클을 생성하고 변수에 할당
        GameObject particleObject = Instantiate((GameObject)hitPrefab, spawnPosition, Quaternion.identity);

        // 파티클 시스템 컴포넌트 가져오기
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();

        // 파티클이 재생되는 동안 대기
        float duration = particleSystem.main.duration;

        Destroy(particleObject, duration);
    }
}