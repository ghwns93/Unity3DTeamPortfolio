using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class EnemyStat : MonoBehaviour
{
    protected int UnitKey;                     // �� ���� �����ڵ� �� ������̺� json�� �ڵ�
    protected string Unitname;                   // �� ���� �̸�
        
    protected int maxHp;                      // �ִ� ü��
    protected int hp;                           // ���� ü��

    public int Hp
            {
            get { return hp; }
            set{ hp = value; }
            }
    
    protected int power;                    // �� ������
    protected float defence;                 // �� ����
    protected float speed;                     // �� �ӵ�
    protected float sight;                       // �� �þ�(�÷��̾� �߰� �þ�)
    protected float lostSight;                       // �� �ҽ� �þ�(�÷��̾� �߰� �þ�)
    protected float attackRange1;     // �� ��Ÿ�(���Ÿ� �� �ٰŸ�)
    protected float attackRange2;     // �� ��Ÿ�(Ư�� ���� ��Ÿ�)
    protected float exp;                          // ���� �ִ� ����ġ
    protected float morale;                   // �� ���

    protected bool isFinded = false;   // �÷��̾� �߰� ���� ����

     protected enum EnemyState
    {
        Idle,                           // �⺻ ����
        Patrol,                       // ���� ����
        Move0,                     // �̵� ����
        Move1,                     // Ư�� �̵� ����(���� Ȥ�� ���� ä�� �̵�)
        Find,                          // �÷��̾� �߰� �� �ߵ� ���
        Lost,                          // �÷��̾� �ҽǽ� �ߵ� ���
        Attack0,                   // ���� ����
        Attack1,                   // Ư�� ����1
        Attack2,                   // Ư�� ����2
        Attack3,                   // Ư�� ����3
        Guard,                      // ��� ���
        AttackDelay,          // ���� ��� ����(���Ÿ� �� Ȱ�ű� ����)
        Reload,                     // ������
        Return,                     // ������ ���·� ����
        Damaged,               // ������ �Ծ��� ��
        LowMorale,             // ����� ū �������� �Ծ��� �� (���� �� Ư�� ����)
        Die                             // ���
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
