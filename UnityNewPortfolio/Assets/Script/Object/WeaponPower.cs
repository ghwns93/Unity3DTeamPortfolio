using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPower : MonoBehaviour
{
   
    Enemy_VS EV;
    PlayerController PCR;

    // �÷��̾� ������ ���޹��� ��ũ��Ʈ
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
                Debug.Log("[Į ����] ���ⵥ���� : " + power);
                break;
            case "SM_axe_norse_04(Clone)":
                power = 20;
                Debug.Log("[���� ����] ���ⵥ���� : " + power);
                break;
            case "SM_hammer_03(Clone)":
                power = 15;
                Debug.Log("[���Ը� ����] ���ⵥ���� : " + power);
                break;

        }
     }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹�˻�");
        // �浹�� ������Ʈ�� enemy �±׸� ������ �ִ��� Ȯ���մϴ�.
        if (other.gameObject.CompareTag("Enemy") && PCR.isAttack )
        {
            // Enemy_VS ��ũ��Ʈ�� ã���ϴ�.
            EV = other.gameObject.GetComponent<Enemy_VS>();
            Debug.Log("�浹����");

            // Enemy_VS ��ũ��Ʈ�� �ִٸ�, hp�� ���ҽ�ŵ�ϴ�.
            if (EV != null)
            {
                Debug.Log("�浹���� ü�°���");
                EV.HitEnemy(power);
            }
        }
    }
}
