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


    // Start is called before the first frame update
    void Start()
    {


        /*
        [[UnitKey]]
        
        첫째 자리 0 - 마을 NPC류
        첫째 자리 1 - 숲 필드(현재 숲만 구현)

        둘째 자리 0 - 비공격형
        둘째 자리 1 - 선공형
        둘째 자리 2 - 비선공형

        셋째 자리 0 - 무공격형
        셋째 자리 1 - 근거리형
        셋째 자리 2 - 중거리형
        셋째 자리 3 - 원거리형
        셋째 자리 4 - 복합형(근거리, 원거리 모두 사용)

        넷째 자리 0 - 순수 동물
        넷째 자리 1 - 인간형
        넷째 자리 2 - 몬스터형
        넷째 자리 3 - 보스 몬스터
        넷째 자리 4 - 단순 NPC

        다섯째 자리 1 - 유닛 제작 순서

        [키 코드 예시]
        ex) 숲에서 나오는 선공형 근거리 인간(검과 방패) - 11111
        ex) 숲에서 나오는 선공형 근거리 인간(한손 검) - 11112
        ex) 숲에서 나오는 선공형 근거리 인간(창) - 11211
        ex) 마을에서 나오는 비공격 동물 - 00001
        ex) 마을에서 나오는 비공격 상인 NPC - 00041
        ex) 숲에서 나오는 비선공 복합형 개미몬스터 - 12421
         
         */

        Unitname = "바이킹 약탈자";
        UnitKey = 11111;  
        maxHp = LevelingStat(100 , pStat.Level);
        hp = LevelingStat(100 , pStat.Level);
        power = LevelingStat(10.0f, pStat.Level);
        defence = LevelingStat(5.0f, pStat.Level);
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
                Patrol();
                break;
            case EnemyState.Move0:
                Move0();
                break;
            case EnemyState.Move1:
                Move1();
                break;
            case EnemyState.Find:
                Find();
                break;
            case EnemyState.Attack0:
                Attack0();
                break;
            case EnemyState.Attack1:
                Attack1();
                break;
            case EnemyState.Attack2:
                Attack2();
                break;
            case EnemyState.Attack3:
                Attack3();
                break;
            case EnemyState.Guard:
                Guard();
                break;
            case EnemyState.AttackDelay:
                AttackDelay();
                break;
            case EnemyState.Reload:
                Reload();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.LowMorale:
                LowMorale();
                break;
            case EnemyState.Die:
                Die();
                break;
        }

        // 현재 HP를 슬라이더의 value에 반영한다
        hpSlider.value = (float)hp / (float)maxHp;
    }

    private void Die()
    {
    }

    private void LowMorale()
    {
    }

    private void Damaged()
    {
    }

    private void Return()
    {
    }

    private void Reload()
    {
    }

    private void AttackDelay()
    {
    }

    private void Guard()
    {
    }

    private void Attack3()
    {
    }

    private void Attack2()
    {
    }

    private void Attack1()
    {
    }

    private void Attack0()
    {
    }

    private void Find()
    {
    }

    private void Move1()
    {
    }

    private void Move0()
    {
    }

    private void Patrol()
    {
    }

    private void Idle()
    {
        // 만약 플레이어와의 거리가 지정한 값보다 적을 경우 Move 상태로 전환
        if (Vector3.Distance(transform.position, player.position) < 0)
        {
            /*
            // enum 변수의 상태 전환
            m_State = EnemyState.Move;
            print("상태 전환 : Idle -> Move");

            // 이동 애니메이션 전환하기
            anim.SetTrigger("IdleToMove");
             */
        }
    }
}
