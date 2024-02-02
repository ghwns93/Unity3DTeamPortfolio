using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPower : MonoBehaviour
{
    public GameObject hitPaticle;

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

            PlayParticleEffect();
        }
    }

    void PlayParticleEffect()
    {
        // 파티클을 생성할 위치
        Vector3 spawnPosition = transform.position;

        // 파티클을 생성하고 변수에 할당
        GameObject particleObject = Instantiate((GameObject)hitPaticle, spawnPosition, Quaternion.identity);

        // 파티클 시스템 컴포넌트 가져오기
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();

        // 파티클이 재생되는 동안 대기
        float duration = particleSystem.main.duration;

        Destroy(particleObject, duration);
    }
}
