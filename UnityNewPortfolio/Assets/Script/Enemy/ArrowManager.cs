using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    Enemy_VA VA;

    [HideInInspector]
    public Transform target;
    float speed = 20f;
    public GameObject hitEffect;


    private void Start()
    {
        if (target)
            transform.LookAt(target);

    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            VA.AttackAction();
        }
    }
}