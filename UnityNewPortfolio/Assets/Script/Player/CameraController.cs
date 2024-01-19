using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    GameObject originCamera;

    public Vector3 startPosition;
    public Vector3 startRotation;

    private float xRotate, xRotateMove, yRotate, yRotateMove;
    public float minXRotate, maxXRotate;
    public float rotateSpeed = 500.0f;

    private void Start()
    {
        originCamera = transform.parent.gameObject;

        if (originCamera != null)
        {
            transform.position = originCamera.transform.position + startPosition;
            transform.rotation = Quaternion.Euler(startRotation);
        }
    }

    private void Update()
    {
        if (originCamera != null)
        {
            xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            xRotate = xRotate + xRotateMove;
            yRotate = yRotate + yRotateMove;

            xRotate = Mathf.Clamp(xRotate, minXRotate, maxXRotate); // 위, 아래 고정

            transform.rotation = Quaternion.Euler(new Vector3(xRotate, yRotate, 0));
        }
    }
}
