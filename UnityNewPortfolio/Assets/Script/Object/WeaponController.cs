using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // ���� ���� ���� Ȯ�ο�
    private int weaponCount = 0;

    // Update is called once per frame
    void Update()
    {
        // ���� ���� �� ����
        int currentCount = transform.childCount;

        // ���� ���� ���ο� �������� ���� ���� Ȯ�ο� if��
        if (currentCount != weaponCount)
        {
            for (int i = weaponCount; i < currentCount; i++)
            {
                Transform child = transform.GetChild(i);
                //�ڽ� ��ü�� ���� ���ݷ��� ����
                AddPowerToChild(child);
                //�ڽ� ��ü�� �ݶ��̴��� ����
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
