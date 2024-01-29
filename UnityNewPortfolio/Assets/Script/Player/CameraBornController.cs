using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.ScrollRect;

public class CameraBornController : MonoBehaviour
{
    GameObject player;
    public float rotateSpeed = 500.0f;

    PlayerController playerController;
    public bool isUiOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerBody");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        isUiOpen = playerController.isUiOpen;

        Vector3 refVec = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref refVec, 0.02f);
    }

    private void LateUpdate()
    {
        if (!isUiOpen)
        {
            LookAround();
        }
    }

    private void LookAround()
    {
        float x = (Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
        Vector3 camAngle = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(new Vector3(camAngle.x, camAngle.y + x, camAngle.z));
    }
}
