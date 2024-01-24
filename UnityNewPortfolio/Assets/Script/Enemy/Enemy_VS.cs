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

    // 내비게이션 메쉬 컴포넌트
    NavMeshAgent agent;

    // 배회 웨이포인트
    public Transform[] points;

    // 해당 목표지점의 인덱스
    private int destPoint = 0;


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

        // 내비게이션 에이전트 컴포넌트 가져오기
        agent = GetComponent<NavMeshAgent>();

        
        // 자동으로 목적지 변경을 멈춤
        agent.autoBraking = false;

        GotoNextPoint();
        
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
            case EnemyState.Move0:
                //Move0();
                break;
            case EnemyState.Move1:
                //Move1();
                break;
            case EnemyState.Find:
                //Find();
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
            print("상태 : Patrol");

        
        // Agent가 현재 목적지에 거의 도달했다면, 다음 목적지로 이동
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
        

    }

    
    // 목적지 순회 메서드
    void GotoNextPoint()
    {
        // 포인트가 설정되지 않았다면, 반환
        if (points.Length == 0)
            return;

        // Agent가 현재의 목적지를 가리키도록 설정
        agent.destination = points[destPoint].position;

        // 배열 내의 다음 위치로 목적지 인덱스를 순환
        destPoint = (destPoint + 1) % points.Length;
    }
    

}
