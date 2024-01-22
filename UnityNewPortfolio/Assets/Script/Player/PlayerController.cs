using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float runSpeed = 2.0f;       // �޸��� �ӵ� (���)
    public float jumpPower = 10.0f;
    public float gravity = -10.0f;
    float yVelocity = 0;                // �������� �ӵ�

    private float nowSpeed; // ���� �ӵ�

    private float vSpeed = 0.0f;
    internal bool isAttack = false;
    internal bool isJumping = false;
    internal bool isIdle = false;        // idle�ִϸ��̼� ������ȯ

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

        // ĳ���Ͱ� �ٴڿ� ������ ���
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            // ĳ���� Y�� �ӵ��� 0���� �����Ѵ�
            yVelocity = 0;
        }

        //�޸���
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

        // �����̽� �ٸ� �Է����� �� ������ ���� ���� ����
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // ĳ���� Y�� �ӷ¿� �������� �����ϰ� ���� �����Ѵ�
            isJumping = true;

            animator.SetTrigger("JumpTrigger");

            nowSpeed *= 0.7f;
        }

        // ĳ���� ���� �ӵ��� �߷��� �����Ѵ�
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
        //���� �ִϸ��̼ǿ��� ����ݱ� ����� ������ ����.
        yVelocity = jumpPower;
    }

    void JumpDown()
    {
        //���� �ִϸ��̼� ����� ���� ���ɻ��·� ����.
        isJumping = false;
    }
}
