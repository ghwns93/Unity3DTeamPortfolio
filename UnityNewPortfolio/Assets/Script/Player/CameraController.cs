using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    GameObject originCamera;
    Camera camera;

    public Vector3 startPosition;
    public Vector3 startRotation;

    private float xRotate, xRotateMove, yRotate, yRotateMove;
    public float minXRotate, maxXRotate;
    public float fovRate = 1.0f;

    CameraBornController cbc;
    private float rotateSpeed;

    private void Start()
    {
        originCamera = transform.parent.gameObject;
        camera = GetComponent<Camera>();

        if (originCamera != null)
        {
            transform.position = originCamera.transform.position + startPosition;
            transform.rotation = Quaternion.Euler(startRotation);

            cbc = originCamera.GetComponent<CameraBornController>();
            rotateSpeed = cbc.rotateSpeed;
        }
    }

    private void LateUpdate()
    {
        if (originCamera != null && !cbc.isUiOpen)
        {
            xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            xRotate = xRotate + xRotateMove;
            yRotate = yRotate + yRotateMove;

            xRotate = Mathf.Clamp(xRotate, minXRotate, maxXRotate); // 위, 아래 고정

            transform.rotation = Quaternion.Euler(new Vector3(xRotate, yRotate, 0));

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            camera.fieldOfView += (-scroll * fovRate);
            if (camera.fieldOfView > 90.0f) camera.fieldOfView = 90.0f;
            else if (camera.fieldOfView < 40.0f) camera.fieldOfView = 40.0f;
        }
    }
}
