using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPower : MonoBehaviour
{
   
    Enemy_VS EV;
    PlayerController PCR;

    // 플레이어 정보를 전달받을 스크립트
    public int power = 5;
    
    private void Start()
    {
        SetPower();
        PCR = FindObjectOfType<PlayerController>();
    }


    void SetPower()
    {
        string name = gameObject.name;

        switch (name)
        {
            case "SM_sword_norse_03(Clone)" :
                power= 10;
                Debug.Log("[칼 장착] 무기데미지 : " + power);
                break;
            case "SM_axe_norse_04(Clone)":
                power = 20;
                Debug.Log("[도끼 장착] 무기데미지 : " + power);
                break;
            case "SM_hammer_03(Clone)":
                power = 15;
                Debug.Log("[오함마 장착] 무기데미지 : " + power);
                break;

        }
     }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌검사");
        // 충돌한 오브젝트가 enemy 태그를 가지고 있는지 확인합니다.
        if (other.gameObject.CompareTag("Enemy") && PCR.isAttack )
        {
            // Enemy_VS 스크립트를 찾습니다.
            EV = other.gameObject.GetComponent<Enemy_VS>();
            Debug.Log("충돌판정");

            // Enemy_VS 스크립트가 있다면, hp를 감소시킵니다.
            if (EV != null)
            {
                Debug.Log("충돌판정 체력감소");
                EV.HitEnemy(power);
            }
        }
    }
}
