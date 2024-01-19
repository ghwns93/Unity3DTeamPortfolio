using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    public Vector3 startPosition;
    public Vector3 startRotation;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;

        if (player != null)
        {
            transform.position = player.transform.position + startPosition;
            transform.rotation = Quaternion.Euler(startRotation);
        }
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + startPosition;
            transform.rotation = Quaternion.Euler(startRotation);
        }
    }
}
