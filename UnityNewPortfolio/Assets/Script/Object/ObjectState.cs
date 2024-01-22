using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : MonoBehaviour
{
    public string name;

    private void Start()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 10);

        Vector3 groundPoint = new Vector3((float)hit.point.x, (float)hit.point.y, (float)hit.point.z);

        transform.position = groundPoint;
    }
}
