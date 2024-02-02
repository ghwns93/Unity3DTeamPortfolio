using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPower : MonoBehaviour
{
   
    Enemy_VS EV;
    Enemy_VA EA;
    PlayerController PCR;

    // 플레이어 정보를 전달받을 스크립트
    public int power = 5;
    
    private void Start()
    {
        PCR = FindObjectOfType<PlayerController>();

        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<Collider>();
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
            // Enemy_VS 스크립트가 있다면, hp를 감소시킵니다.
            else if (EA != null)
            {
                Debug.Log("충돌판정 체력감소");
                EV.HitEnemy(power);
            }
        }
    }
}
