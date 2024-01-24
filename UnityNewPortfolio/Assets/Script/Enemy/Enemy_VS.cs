using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

public class Enemy_VS : EnemyStat
{

    PlayerState pStat;

    Slider hpSlider;

    EnemyState E_State;
    
    // 플레이어 위치
    public Transform player;

    // 적 캐릭터 컨트롤러
    public CharacterController cc;

    // 누적 시간
    protected float currentTime = 0.0f;

    // 딜레이
    protected float attackDelay;

    // 적 초기 위치
    Vector3 originPos;
    Quaternion originRot;

    // 애니메이터 컴포넌트
    Animator anim;

    // 내비게이션 메쉬 컴포넌트
    NavMeshAgent agent;

    // 배회 웨이포인트
    public Transform[] waypoints;
   
    
    private int destPoint = 0;
   

    // Start is called before the first frame update
    void Start()
    {


        Unitname = "바이킹 약탈자";
        UnitKey = 11111;
        maxHp = 100;
        hp = 100;
        power = 10.0f;
        defence = 5.0f;
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
        anim = transform.GetComponentInChildren<Animator>();

        // 내비게이션 에이전트 컴포넌트 가져오기
        agent = GetComponent<NavMeshAgent>();
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
                //Patrol();
                break;
            case EnemyState.Move0:
                Move0();
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

        if(E_State==EnemyState.Move0)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GoToNextWaypoint();
        }


        // 현재 HP를 슬라이더의 value에 반영한다
        //hpSlider.value = (float)hp / (float)maxHp;
    }

    private void Idle()
    {
            //enum 변수의 상태 전환
            E_State = EnemyState.Move0;
            print("상태 전환 : Idle -> Move");

            //이동 애니메이션 전환하기
            anim.SetTrigger("IdleToMove");
    }

    private void Move0()
    {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.autoBraking = false;
            GoToNextWaypoint();
     }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;
        agent.destination = waypoints[destPoint].position;
        destPoint = (destPoint + 1) % waypoints.Length;
    }
}
