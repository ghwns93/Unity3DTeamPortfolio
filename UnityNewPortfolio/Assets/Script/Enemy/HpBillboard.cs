using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBillboard : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        // �׻� ī�޶��� ����� ��ġ��Ų��
        transform.forward = target.forward;
    }
}

