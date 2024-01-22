using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float runSpeed = 2.0f;       // 달리기 속도 (배속)
    public float jumpPower = 10.0f;
    public float gravity = -10.0f;
    float yVelocity = 0;                // 떨어지는 속도

    private float nowSpeed; // 최종 속도

    private float vSpeed = 0.0f;
    internal bool isAttack = false;
    internal bool isJumping = false;
    internal bool isIdle = false;        // idle애니메이션 상태전환

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
        #region [ Update ]
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
            // 캐릭터 Y축 속도를 0으로 설정한다
            yVelocity = 0;
        }

        //달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            nowSpeed = speed * runSpeed;
            animator.SetBool("RunFoward", true);
        }
        else
        {
            nowSpeed = speed;
            animator.SetBool("RunFoward", false);
        }

        // 스페이스 바를 입력했을 때 점프를 하지 않은 상태
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // 캐릭터 Y축 속력에 점프력을 적용하고 상태 변경한다
            isJumping = true;

            animator.SetTrigger("JumpTrigger");

            nowSpeed *= 0.7f;
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
                if (!isJumping)
                {
                    animator.SetBool("WalkFoward", true);
                }
                animator.SetBool("Idle", false);

                velocity = moveDir * nowSpeed;
            }
            else
            {
                if (!isJumping)
                {
                    animator.SetBool("Idle", true);
                }
                animator.SetBool("WalkFoward", false);
            }
        }

        velocity.y += yVelocity;

        cc.Move(velocity * Time.deltaTime);
        #endregion
    }

    void AttackEnd()
    {
        isAttack = false;
    }

    void JumpTime()
    {
        //점프 애니메이션에서 도움닫기 모션이 끝난후 점프.
        yVelocity = jumpPower;
    }

    void JumpDown()
    {
        //점프 애니메이션 종료시 점프 가능상태로 변경.
        isJumping = false;
    }
}
