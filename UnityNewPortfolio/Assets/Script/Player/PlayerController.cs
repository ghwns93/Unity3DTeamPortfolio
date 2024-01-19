using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpPower = 10.0f;
    public float gravity = 10.0f;

    private float vSpeed = 0.0f;

    private enum LookPoint
    {
        Forward = 0,
        RightForward = 1,
        Right = 2,
        RightBack = 3,
        Back = 4,
        LeftBack = 5,
        Left = 6,
        LeftForward = 7
    }

    private CharacterController cc;
    private Transform tran;
    private LookPoint beforePoint;
    private LookPoint currntPoint;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        tran = GetComponent<Transform>();
        currntPoint = LookPoint.Forward;
        beforePoint = LookPoint.Forward;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        float moveAct = moveX != 0 ? moveX : moveY;
        moveAct = Mathf.Abs(moveAct);

        bool isAttack = false;

        if (Input.GetButtonDown("Fire1"))
        {
            moveAct = 0;

            isAttack = true;
        }

        if (!isAttack)
        {
            if (moveAct != 0)
            {
                Vector3 move = transform.forward * moveAct;

                Vector3 velocity = move.normalized * speed;

                if (moveX > 0 && moveY == 0) currntPoint = LookPoint.Right;
                else if (moveX < 0 && moveY == 0) currntPoint = LookPoint.Left;
                else if (moveY > 0 && moveX == 0) currntPoint = LookPoint.Forward;
                else if (moveY < 0 && moveX == 0) currntPoint = LookPoint.Back;
                else if (moveX > 0 && moveY > 0) currntPoint = LookPoint.RightForward;
                else if (moveX > 0 && moveY < 0) currntPoint = LookPoint.RightBack;
                else if (moveX < 0 && moveY < 0) currntPoint = LookPoint.LeftBack;
                else if (moveX < 0 && moveY > 0) currntPoint = LookPoint.LeftForward;

                SetPoint();
                cc.Move(velocity * Time.deltaTime);
            }
        }
    }

    void SetPoint()
    {
        if(currntPoint != beforePoint)
        {
            beforePoint = currntPoint;

            float angle = 45.0f * (float)currntPoint;

            //정면 일때는 살짝 틀어주기(그래야 이쁨)
            if (angle == 0.0f) angle = 3.0f;

            Debug.Log(angle);

            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
