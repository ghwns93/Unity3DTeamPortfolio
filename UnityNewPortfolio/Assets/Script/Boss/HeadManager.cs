using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadManager : MonoBehaviour
{
     public Transform target; // ������Ʈ B�� Transform

    // Update is called once per frame
    void Update()
    {
        // target�� �ٶ󺸴� ȸ���� ����մϴ�.
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);

        // ȸ�� ���� ���Ϸ� ������ ��ȯ�մϴ�.
        Vector3 euler = lookRotation.eulerAngles;

        // Y�� ȸ���� �����մϴ� (-40 ~ 40��).
        euler.y = Mathf.Clamp(NormalizeAngle(euler.y), -40f, 40f);

        // X�� ȸ���� �����մϴ� (-30 ~ 30��).
        euler.x = Mathf.Clamp(NormalizeAngle(euler.x), -30f, 30f);

        // Z�� ȸ���� �������� �ʽ��ϴ� (�Ǵ� �ʿ��ϴٸ� 0���� ����).
        euler.z = 0;

        // ���ѵ� ���Ϸ� ������ ����Ͽ� ȸ���� �����մϴ�.
        transform.rotation = Quaternion.Euler(euler);
    }

    // ������ -180 ~ 180 ���� ���� ����ȭ�մϴ�.
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
