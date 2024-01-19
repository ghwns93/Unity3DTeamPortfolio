using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpPower = 10.0f;
    public float gravity = -10.0f;
    float yVelocity = 0;                // 떨어지는 속도

    private float vSpeed = 0.0f;
    private bool isAttack = false;
    private bool isJumping = false;

    public GameObject cameraOrigin;

    private CharacterController cc;
    private Transform tran;
    private Transform charaTran;

    private Animator animator;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        tran = GetComponent<Transform>();

        //animator = transform.GetChild(0).GetComponent<Animator>();
        animator = GetComponent<Animator>();
        charaTran = transform.GetChild(0).GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 velocity = new Vector3(0f, 0f, 0f);

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        float moveAct = moveX != 0 ? moveX : moveY;
        moveAct = Mathf.Abs(moveAct);

        if (Input.GetButtonDown("Fire1") && !isAttack)
        {
            moveAct = 0;

            isAttack = true;

            animator.SetTrigger("AttackTrigger");
        }

        // 캐릭터가 바닥에 착지한 경우
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            // 캐릭터 Y축 속도를 0으로 설정한다
            yVelocity = 0;
        }

        // 스페이스 바를 입력했을 때 점프를 하지 않은 상태
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // 캐릭터 Y축 속력에 점프력을 적용하고 상태 변경한다
            yVelocity = jumpPower;
            isJumping = true;
        }

        // 캐릭터 수직 속도에 중력을 적용한다
        yVelocity += gravity * Time.deltaTime;

        if (!isAttack)
        {
            if (moveAct != 0)
            {
                Vector3 lookForward = new Vector3(cameraOrigin.transform.forward.x, 0f, cameraOrigin.transform.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraOrigin.transform.right.x, 0f, cameraOrigin.transform. right.z).normalized;
                Vector3 moveDir = lookForward * moveY + lookRight * moveX;

                transform.forward = lookForward;

                velocity = moveDir * speed;
            }
        }

        velocity.y += yVelocity;

        cc.Move(velocity * Time.deltaTime);
    }

    void AttackEnd()
    {
        isAttack = false;

        animator.Play("Idle");

        Debug.Log("AttackEnd");
    }
}
