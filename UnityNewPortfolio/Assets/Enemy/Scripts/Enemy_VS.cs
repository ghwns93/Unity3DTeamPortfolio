using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_VS : EnemyStat
{
    // 상태를 나타내는 enum 변수
    EnemyStat eState;
    PlayerState pStat;
    
    // Start is called before the first frame update
    void Start()
    {
        eState= new EnemyStat();

        eState.maxHp = LevelingStat(100 , pStat.Level);
        eState.power = LevelingStat(10.0f, pStat.Level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int LevelingStat(int num, int level)
    {
        num += level;
        return num;
    }

    float LevelingStat(float num, int level)
    {
        num += level;
        return num;

    }
}
