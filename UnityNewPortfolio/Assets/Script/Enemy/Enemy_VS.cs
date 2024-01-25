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
        power = 10.0f;//LevelingStat(10.0f, pStat.Level); ;
        defence = 5.0f;//LevelingStat(5.0f, pStat.Level);
        speed = 10.0f;
        exp = 50.0f;
        sight = 10.0f;
        lostSight = 20.0f;
        attackRange1 = 2.5f;
        attackRange2 = 2.5f;
        morale = 100.0f;


        // 최초의 상태는 대기 모드
        E_State = EnemyState.Idle;

        // 플레이어의 위치 좌표 가져오기
        player = GameObject.Find("Player").transform;

        // 캐릭터 컨트롤러 가져오기
        cc = GetComponent<CharacterController>();

        // 초기 위치와 회전 값 저장
        originPos = transform.position;
        originRot = transform.rotation;

        // 애니메이터 컴포넌트를 가져오기 (자식 오브젝트의 컴포넌트)
        anim = GetComponentInChildren<Animator>();

        waitTimer = waitTime;

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
            print("상태 : Idle");
            //enum 변수의 상태 전환
            E_State = EnemyState.Patrol;
            print("상태 전환 : Idle -> Patrol");

            //이동 애니메이션 전환하기
            anim.SetTrigger("IdleToPatrol");
    }

    private void Patrol()
    {
        //MoveToNextWaypoint();

        if (Vector3.Distance(transform.position, player.position) < sight)
        {
            
            isFinded = true;
            // enum 변수의 상태 전환
            E_State = EnemyState.Find;
            print("상태 전환 : Patrol -> Find");

            // 이동 애니메이션 전환하기
            anim.SetTrigger("PlayerFind");
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
        if (Vector3.Distance(transform.position, player.position) > sight )//&& Vector3.Distance(transform.position, player.position) < lostSight)
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
    }

        private void Lost()
        {
            // enum 변수의 상태 전환
            E_State = EnemyState.Patrol;
            print("상태 전환 : Lost -> Patrol");

        // 이동 애니메이션 전환하기
        anim.SetTrigger("LostToPatrol");
    }

        // 현재 상태를 공격(Attack)으로 전환한다
        //else
        //{
        //E_State = EnemyState.Attack0;
        //print("상태 전환 : Move0 -> Attack0");

        // 누적 시간을 딜레이 시간만큼 미리 진행시켜둔다 (즉시 공격)
        //currentTime = attackDelay;

        // 공격 대기 애니메이션
        //anim.SetTrigger("MoveToAttackDelay");
        //}





    /////////////////////////////////

    void MoveToNextWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        // 현재 웨이포인트로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 이동 방향으로 회전
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero) // 정지 상태가 아닐 때만 회전
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        }

        // 목적지에 도착했는지 확인
        if (transform.position == targetPosition)
        {
            // 대기 시간이 남아있다면 감소
            if (waitTimer > 0)
            {
                waitTimer -= Time.deltaTime;
            }
            else
            {
                // 다음 웨이포인트로 이동
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                waitTimer = waitTime;
            }
        }
    }


}
