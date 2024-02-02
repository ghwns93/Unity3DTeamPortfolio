using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPower : MonoBehaviour
{
   
    Enemy_VS EV;
    Enemy_VA EA;
    PlayerController PCR;

    // �÷��̾� ������ ���޹��� ��ũ��Ʈ
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
            // Enemy_VS ��ũ��Ʈ�� �ִٸ�, hp�� ���ҽ�ŵ�ϴ�.
            else if (EA != null)
            {
                Debug.Log("�浹���� ü�°���");
                EV.HitEnemy(power);
            }
        }
    }
}
