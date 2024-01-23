using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class EnemyStat : MonoBehaviour
{
    public int UnitKey;                     // �� ���� �����ڵ� �� ������̺� json�� �ڵ�
    public string name;                   // �� ���� �̸�
    public int maxHp;                      // �ִ� ü��
    public int hp;                               // ���� ü��
    public float power;                    // �� ������
    public float defence;                 // �� ����
    public float speed;                     // �� �ӵ�
    public float sight;                       // �� �þ�(�÷��̾� �߰� �þ�)
    public float attackRange1;     // �� ��Ÿ�(���Ÿ� �� �ٰŸ�)
    public float attackRange2;     // �� ��Ÿ�(Ư�� ���� ��Ÿ�)
    public float exp;                          // ���� �ִ� ����ġ
    public float morale;                   // �� ���

    public bool isFinded = false;   // �÷��̾� �߰� ���� ����

    // ü�� �����̴� ��
    public Slider hpSlider;

    // �÷��̾� ��ġ
    Transform player;

    // �� ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    // ���� �ð�
    float currentTime = 0.0f;

    // ������
    float attackDelay;

    // �� �ʱ� ��ġ
    Vector3 originPos;
    Quaternion originRot;

    // �ִϸ����� ������Ʈ
    Animator anim;

    // ������̼� �޽� ������Ʈ
    NavMeshAgent agent;

    enum EnemyState
    {
        Idle,                           // �⺻ ����
        Patrol,                       // ���� ����
        Move0,                     // �̵� ����
        Move1,                     // Ư�� �̵� ����(���� Ȥ�� ���� ä�� �̵�)
        Find,                          // �÷��̾� �߰� �� �ߵ� ���
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

}
