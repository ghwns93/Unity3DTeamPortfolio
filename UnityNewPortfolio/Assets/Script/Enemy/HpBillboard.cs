using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBillboard : MonoBehaviour
{
    Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        // �׻� ī�޶��� ����� ��ġ��Ų��
        transform.forward = target.forward;
    }
}

