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
        // 플레이어 오브젝트 찾기
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // 타겟 방향 계산, Y축 위치에 1.0f 추가
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + 1.0f, player.position.z);
        targetDirection = (targetPosition - transform.position).normalized;

        // FireBall의 회전값 조정하여 타겟을 바라보도록 함
        transform.LookAt(targetPosition);

        // 자동 파괴 타이머 설정
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        // 플레이어를 향해 이동
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("파이어볼 판정");
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("파이어볼 충돌");

            player.GetComponent<PlayerState>().DamageAction(power);

            Destroy(gameObject);
        }
        else
        {

            Debug.Log("파이어볼 다른 오브젝트 충돌");
            // 다른 오브젝트와 부딪히면 파괴
            Destroy(gameObject);
        }
    }
}