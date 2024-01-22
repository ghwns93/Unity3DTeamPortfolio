using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        //항상 카메라의 방향과 일치
        transform.forward = target.forward;
    }
}
