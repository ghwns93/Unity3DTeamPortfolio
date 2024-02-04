using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadManager : MonoBehaviour
{
     public Transform target; // 오브젝트 B의 Transform

    // Update is called once per frame
    void Update()
    {
        // target을 바라보는 회전을 계산합니다.
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);

        // 회전 값을 오일러 각도로 변환합니다.
        Vector3 euler = lookRotation.eulerAngles;

        // Y축 회전을 제한합니다 (-40 ~ 40도).
        euler.y = Mathf.Clamp(NormalizeAngle(euler.y), -40f, 40f);

        // X축 회전을 제한합니다 (-30 ~ 30도).
        euler.x = Mathf.Clamp(NormalizeAngle(euler.x), -30f, 30f);

        // Z축 회전은 제한하지 않습니다 (또는 필요하다면 0으로 설정).
        euler.z = 0;

        // 제한된 오일러 각도를 사용하여 회전을 적용합니다.
        transform.rotation = Quaternion.Euler(euler);
    }

    // 각도를 -180 ~ 180 범위 내로 정규화합니다.
    private float NormalizeAngle(float angle)
    {
        while (angle > 180f)
        {
            angle -= 360f;
        }
        while (angle < -180f)
        {
            angle += 360f;
        }
        return angle;
    }
}
