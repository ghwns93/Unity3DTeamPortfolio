using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

public class Enemy_VS : EnemyStat
{
    // 플레이어 스크립트
    PlayerState pStat;
        
    // 적 상태 구조체
    EnemyState E_State;
    
    // 플레이어 위치
    Transform player;

    // 적 캐릭터 컨트롤러
    CharacterController cc;

    // 누적 시간
    protected float currentTime = 0.0f;

    // 딜레이
    protected float attackDelay = 2.0f;

    // 적 초기 위치
    Vector3 originPos;
    Quaternion originRot;

    // 애니메이터 컴포넌트
    Animator anim;

    public Transform[] waypoints; // 웨이포인트 배열
    
    public float waitTime = 0.5f; // 각 웨이포인트에서 대기하는 시간

    private int currentWaypointIndex = 0; // 현재 웨이포인트 인덱스
    
    private float waitTimer;               // 대기 타이머

    public float rotateSpeed = 2.0f; // 회전 속도

    // Start is called before the first frame update
    void Start()
    {
        Unitname = "바이킹 약탈자";
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


        // 최초의 상태는 대기 모드
        E_State = EnemyState.Idle;

        // 플레이어 태그의 오브젝트의 위치 좌표 가져오기
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        

        // 캐릭터 컨트롤러 가져오기
        cc = GetComponent<CharacterController>();

        // 초기 위치와 회전 값 저장
        originPos = transform.position;
        originRot = transform.rotation;

        // 애니메이터 컴포넌트를 가져오기 (자식 오브젝트의 컴포넌트)
        anim = GetComponentInChildren<Animator>();

        waitTimer = waitTime;

        isFinded= false;
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 상태를 검사하고 상태별로 정해진 기능을 수행한다
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
            print("상태 : Idle");
            //enum 변수의 상태 전환
            E_State = EnemyState.Patrol;
            print("상태 전환 : Idle -> Patrol");

            //이동 애니메이션 전환하기
            anim.SetTrigger("IdleToPatrol");
    }
    private void Patrol()
    {
        MoveToNextWaypoint();

        // 플레이어 발견 시 상태를 변경하고 Patrol 메소드를 빠져나온다.
        if (Vector3.Distance(transform.position, player.position) < sight)
        {
            isFinded = true;
            E_State = EnemyState.Find;
            print("상태 전환 : Patrol -> Find");
            anim.SetTrigger("PlayerFind");
            return; // 이동을 멈추고 Update 루프로 돌아간다.
        }

    }

    private void Find()
    {
                E_State = EnemyState.Move0;
        print("상태전환 : Find -> Move0");

        anim.SetTrigger("FindToMove0");
    }



    private void Move0()
    {
        // 만약 플레이어와의 거리가 공격 범위 밖이라면 플레이어를 향해 이동한다
        if (Vector3.Distance(transform.position, player.position) > attackRange1 && Vector3.Distance(transform.position, player.position) < lostSight)
        {
            // 이동 방향
            Vector3 dir = (player.position - transform.position).normalized;

            // 캐릭터 컨트롤러를 사용하여 이동
            cc.Move(dir * speed * Time.deltaTime);

            // 플레이어를 향해 방향 전환
            transform.forward = dir;


        }

        // 플레이어와의 거리가 소실 거리 밖이면 원래대로 patrol 한다.
        else if (Vector3.Distance(transform.position, player.position) > lostSight)
        {
            isFinded = false;

            // enum 변수의 상태 전환
            E_State = EnemyState.Lost;
            print("상태 전환 : Find -> Lost");
            // 이동 애니메이션 전환하기
            anim.SetTrigger("PlayerLost");
        }

        else
        {
            E_State = EnemyState.Attack0;
            print("상태 전환 : Move -> Attack");

            // 누적 시간을 딜레이 시간만큼 미리 진행시켜둔다 (즉시 공격)
            currentTime = attackDelay;

            // 공격 대기 애니메이션
            anim.SetTrigger("MoveToAttackDelay");
        }
    }

        private void Lost()
        {
            // enum 변수의 상태 전환
            E_State = EnemyState.Patrol;
            print("상태 전환 : Lost -> Patrol");

            // 이동 애니메이션 전환하기
            anim.SetTrigger("LostToPatrol");
       }

    void Attack0()
    {
        speed= 0;
        // 플레이어가 공격 범위 내라면 공격을 시작한다
        if (Vector3.Distance(transform.position, player.position) < attackRange1)
        {
            // 일정시간마다 공격한다
            // 누적된 시간이 딜레이를 넘어설 때마다 초기화
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                print("공격!");
                AttackAction();
                currentTime = 0;

                // 공격 애니메이션
                anim.SetTrigger("StartAttack");
            }
        }
        // 공격 범위를 벗어났다면 현재 상태를 Move로 전환한다 (재추격)
        else
        {
            speed = 3.0f;
            E_State = EnemyState.Move0;
            print("상태 전환 : Attack -> Move");
            currentTime = 0;

            // 이동 애니메이션
            anim.SetTrigger("AttackToMove");
        }
    }

    /////////////////////////////////

    void MoveToNextWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 이동할 거리를 계산합니다.
        float moveDistance = speed * Time.deltaTime;

        // 웨이포인트를 향해 캐릭터 회전
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        // CharacterController를 사용하여 이동합니다.
        cc.Move(direction * moveDistance);

        // 웨이포인트에 충분히 가까워졌는지 확인합니다.
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

    // 플레이어의 데미지 처리 함수
    public void AttackAction()
    {
        player.GetComponent<PlayerState>().DamageAction(power);
        //Debug.Log(player.GetComponent<PlayerState>().Hp);
    }

    // 사망 상태
    void Die()
    {
        // 진행 중인 피격 코루틴 함수를 중지한다
        StopAllCoroutines();

        // 사망 상태를 처리하기 위한 코루틴을 실행한다
        StartCoroutine(DieProcess());
    }

    // 사망 상태 처리용 코루틴
    IEnumerator DieProcess()
    {
        // 캐릭터 컨트롤러를 비활성화한다
        cc.enabled = false;

        // 2초 동안 기다린 이후 자기자신을 제거한다
        yield return new WaitForSeconds(2.0f);
        print("소멸!");
        Destroy(gameObject);
    }

    // 데미지 처리 함수
    public void HitEnemy(int hitPower)
    {
        // 피격, 사망, 복귀 상태일 경우에는 함수 즉시 종료
        if (E_State == EnemyState.Damaged ||
            E_State == EnemyState.Die)
        {
            return;
        }

        // 플레이어의 공격력만큼 적 체력을 감소시켜준다
        hp -= hitPower;

        Debug.Log(hp);

        // 적 체력이 0보다 크면 피격 상태로 전환
        if (hp > 0)
        {
            E_State = EnemyState.Damaged;
            print("상태 전환 : Any State -> Damaged");

            // 피격 애니메이션 재생
            anim.SetTrigger("Damaged");
            Damaged();
        }
        // 그렇지 않다면 사망 상태로 전환
        else
        {
            E_State = EnemyState.Die;
            print("상태 전환 : Any State -> Die");

            // 사망 애니메이션 재생
            anim.SetTrigger("Die");
            Die();
        }
    }

    // 피격 상태
    void Damaged()
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
        E_State = EnemyState.Move0;
        print("상태 전환 : Damaged -> Move");
    }

void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }


}
