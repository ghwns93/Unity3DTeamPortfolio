using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoll : MonoBehaviour
{
    public float speed = 5f;
    public float destroyTime = 7f;
    
    private Transform player;
    private Vector3 targetDirection;

    public int power = 10;
        
    void Start()
    {
        // �÷��̾� ������Ʈ ã��
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Ÿ�� ���� ���, Y�� ��ġ�� 1.0f �߰�
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + 1.0f, player.position.z);
        targetDirection = (targetPosition - transform.position).normalized;

        // FireBall�� ȸ���� �����Ͽ� Ÿ���� �ٶ󺸵��� ��
        transform.LookAt(targetPosition);

        // �ڵ� �ı� Ÿ�̸� ����
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        // �÷��̾ ���� �̵�
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("���̾ ����");
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("���̾ �浹");

            player.GetComponent<PlayerState>().DamageAction(power);

            Destroy(gameObject);
        }
        else
        {

            Debug.Log("���̾ �ٸ� ������Ʈ �浹");
            // �ٸ� ������Ʈ�� �ε����� �ı�
            Destroy(gameObject);
        }
    }
}