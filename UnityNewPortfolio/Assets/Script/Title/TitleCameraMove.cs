using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TitleCameraMove : MonoBehaviour
{
    public GameObject target;

    private float xRotateMove, yRotateMove;

    public float rotateSpeed = 5.0f;

    void Update()
    {
        xRotateMove = Time.deltaTime * rotateSpeed;

        Vector3 targetPosition = target.transform.position;

        transform.RotateAround(targetPosition, Vector3.up, xRotateMove);

        transform.LookAt(targetPosition);
    }
}
