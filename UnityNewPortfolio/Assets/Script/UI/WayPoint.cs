using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Transform wayPoint;
    GameObject player;

    PlayerController playerController;
    IslandGameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        manager = transform.parent.parent.parent.GetComponent<IslandGameManager>();
    }

    public void WayPointMove()
    {
        if (player != null && wayPoint != null)
        {
            player.transform.position = wayPoint.position;

            if (playerController.isDodge == true) playerController.isDodge = false;
            if (playerController.isAttack == true) playerController.isAttack = false;

            manager.btnClicked = true;
        }
    }
}
