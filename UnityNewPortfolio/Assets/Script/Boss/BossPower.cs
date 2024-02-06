using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPower : MonoBehaviour
{
    public BossScript bs; // �ν����Ϳ��� �Ҵ��� ���� �ְ�, Awake �Ǵ� Start���� �ڵ����� ã�� ���� �ֽ��ϴ�.

    public float pushBackForce = 5f;
    public float pushBackTime = 0.5f;
    private bool isPushingBack = false;
    private Vector3 pushDirection;
    private float pushBackTimer;

    private void Awake()
    {

        // bs = GetComponent<BossScript>();

        // GameObject bossObject = GameObject.FindWithTag("BossTag"); // "BossTag"�� Boss ������Ʈ�� �±��Դϴ�.
        // if (bossObject != null) {
        //     bs = bossObject.GetComponent<BossScript>();
        // }
    }

    private void Update()
    {
        if (isPushingBack)
        {
            pushBackTimer -= Time.deltaTime;
            if (pushBackTimer <= 0)
            {
                isPushingBack = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            // bs�� null�� �ƴ��� Ȯ���ϰ� AttackAction �޼��带 ȣ���մϴ�.
            if (bs != null)
            {
                bs.AttackAction();
            }
            else
            {
                Destroy(this);
            }

            if (controller != null && !isPushingBack)
            {
                pushDirection = (other.transform.position - transform.position).normalized;
                pushDirection.y = 0;
                isPushingBack = true;
                pushBackTimer = pushBackTime;

                StartCoroutine(PushBack(controller, pushDirection, pushBackForce, pushBackTime));
            }
        }
    }

    private IEnumerator PushBack(CharacterController controller, Vector3 direction, float force, float duration)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            controller.Move(direction * force * Time.deltaTime);
            yield return null;
        }
    }
}