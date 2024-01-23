using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class EnemyStat : MonoBehaviour
{
    public int UnitKey;                     // 적 유닛 고유코드 및 드랍테이블 json용 코드
    public string name;                   // 적 유닛 이름
    public int maxHp;                      // 최대 체력
    public int hp;                               // 현재 체력
    public float power;                    // 적 데미지
    public float defence;                 // 적 방어력
    public float speed;                     // 적 속도
    public float sight;                       // 적 시야(플레이어 발견 시야)
    public float attackRange1;     // 적 사거리(원거리 및 근거리)
    public float attackRange2;     // 적 사거리(특수 공격 사거리)
    public float exp;                          // 적이 주는 경험치
    public float morale;                   // 적 사기

    public bool isFinded = false;   // 플레이어 발견 상태 유무

    // 체력 슬라이더 바
    public Slider hpSlider;

    // 플레이어 위치
    Transform player;

    // 적 캐릭터 컨트롤러
    CharacterController cc;

    // 누적 시간
    float currentTime = 0.0f;

    // 딜레이
    float attackDelay;

    // 적 초기 위치
    Vector3 originPos;
    Quaternion originRot;

    // 애니메이터 컴포넌트
    Animator anim;

    // 내비게이션 메쉬 컴포넌트
    NavMeshAgent agent;

    enum EnemyState
    {
        Idle,                           // 기본 상태
        Patrol,                       // 순찰 상태
        Move0,                     // 이동 상태
        Move1,                     // 특수 이동 상태(점프 혹은 앉은 채로 이동)
        Find,                          // 플레이어 발견 시 발동 모션
        Attack0,                   // 메인 공격
        Attack1,                   // 특수 공격1
        Attack2,                   // 특수 공격2
        Attack3,                   // 특수 공격3
        Guard,                      // 방어 모션
        AttackDelay,          // 공격 대기 상태(원거리 시 활매김 상태)
        Reload,                     // 재장전
        Return,                     // 비전투 상태로 복귀
        Damaged,               // 데미지 입었을 때
        LowMorale,             // 상당히 큰 데미지를 입었을 때 (도주 및 특수 상태)
        Die                             // 사망
    }

}
