using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class EnemyStat : MonoBehaviour
{
    protected int UnitKey;                     // 적 유닛 고유코드 및 드랍테이블 json용 코드
    protected string Unitname;                   // 적 유닛 이름
    
    [SerializeField]
    protected int maxHp;                      // 최대 체력
    [SerializeField]
    protected int hp;                               // 현재 체력
    [SerializeField]
    protected float power;                    // 적 데미지
    [SerializeField]
    protected float defence;                 // 적 방어력
    [SerializeField]
    protected float speed;                     // 적 속도
    [SerializeField]
    protected float sight;                       // 적 시야(플레이어 발견 시야)
    [SerializeField]
    protected float attackRange1;     // 적 사거리(원거리 및 근거리)
    [SerializeField]
    protected float attackRange2;     // 적 사거리(특수 공격 사거리)
    [SerializeField]
    protected float exp;                          // 적이 주는 경험치
    [SerializeField]
    protected float morale;                   // 적 사기

    protected bool isFinded = false;   // 플레이어 발견 상태 유무

     protected enum EnemyState
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

    protected int LevelingStat(int num, int level)
    {
        int leveling = num;

        if( level  < 20)
        {
            leveling = (num + ((level * 10) / 5 - ((level * 10) / 5) % 5));

            return leveling;
        }

        else if(level <= 50)
        {
            leveling = num + (((level * 10) / 5 - ((level * 10) / 5) % 5)*2);

            return leveling;
        }

        else if (50 < level)
        {
            leveling = num + (level * 5);
        }

        return leveling;
    }

    protected float LevelingStat(float num, int level)
    {
        float leveling = num;

        if ( level < 10)
        {
            return leveling;
        }
        else if (level < 20)
        {
            leveling = (level * 0.3f);

            return leveling;
        }

        else if (level <= 50)
        {
            leveling = num + (level * 0.5f );

            return leveling;
        }

        else if (50 < level)
        {
            leveling = num + (level * 0.7f);
        }

        return leveling;

    }
}
