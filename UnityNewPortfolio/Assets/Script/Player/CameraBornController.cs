using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBornController : MonoBehaviour
{
    GameObject player;
    public float rotateSpeed = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        LookAround();
    }

    private void LookAround()
    {
        float x = (Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
        Vector3 camAngle = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(camAngle.x, camAngle.y + x, camAngle.z);
    }
}
