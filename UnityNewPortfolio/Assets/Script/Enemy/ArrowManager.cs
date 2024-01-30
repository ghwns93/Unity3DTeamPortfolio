using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public float m_Speed = 2000.0f;
    public Transform m_Tip = null;

    Rigidbody m_Rigidbody= null;
    bool m_isStopped = true;
    Vector3 m_LastPosition = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody= GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        if (m_isStopped) 
            return;

        //rotate
        m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Rigidbody.velocity, transform.up));

        //collision
        if (Physics.Linecast(m_LastPosition, m_Tip.position))
        {
            Stop();
        }

        m_LastPosition = m_Tip.position;
    }

    private void Stop()
    {
        m_isStopped = true;
        m_Rigidbody.useGravity = false;

    }

    public void Fire(float pullValue)
    {
        m_isStopped = false;
        transform.parent = null;

        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.AddForce(transform.forward * (pullValue * m_Speed));

        Destroy(gameObject, 5.0f);
    }

}