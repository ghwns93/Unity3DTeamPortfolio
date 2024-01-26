using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // 무기 소지 여부 확인용
    private int weaponCount = 0;

    // Update is called once per frame
    void Update()
    {
        // 무기 소지 시 증가
        int currentCount = transform.childCount;

        // 무기 소지 여부와 소지중인 무기 갯수 확인용 if문
        if (currentCount != weaponCount)
        {
            for (int i = weaponCount; i < currentCount; i++)
            {
                Transform child = transform.GetChild(i);
                //자식 개체에 무기 공격력을 설정
                AddPowerToChild(child);
                //자식 개체에 콜라이더를 설정
                AddColliderToChild(child);
            }
            weaponCount= currentCount;
        }
    }
    
    void AddPowerToChild(Transform child)
    {
        if(child.GetComponent<WeaponPower>() == null)
        {
            child.gameObject.AddComponent<WeaponPower>();
        }
    }


    void AddColliderToChild(Transform child)
    {
        if(child.GetComponent<Collider>() == null)
        {
            child.gameObject.AddComponent<Collider>();
        }    
    }
}
