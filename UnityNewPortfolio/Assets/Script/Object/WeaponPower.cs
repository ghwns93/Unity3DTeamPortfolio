using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPower : MonoBehaviour
{
    // 플레이어 정보를 전달받을 스크립트
    public PlayerState P_stat;
    public Enemy_VS EV;

    public int power = 5;
    
    private void Start()
    {
        SetPower();    
    }
    
    void SetPower()
    {
        string name = gameObject.name;

        // 오브젝트의 이름에 따라 power값 재조정

        switch(name)
        {
            case "SM_sword_norse_03" :
                power= 10;
                break;
            case "SM_axe_norse_04":
                power = 20;
                break;
            case "SM_hammer_03":
                power = 15;
                break;

        }
        void OnTriggerEnter (Collider other)
        {
            EV.HitEnemy(power);
        }
    }

   


}
