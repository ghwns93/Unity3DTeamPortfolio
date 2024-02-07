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
        // 항상 카메라의 방향과 일치시킨다
        transform.forward = target.forward;
    }
}

