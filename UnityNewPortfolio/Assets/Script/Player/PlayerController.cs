using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpPower = 10.0f;
    public float gravity = -10.0f;
    float yVelocity = 0;                // �������� �ӵ�

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

        // ĳ���Ͱ� �ٴڿ� ������ ���
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            // ĳ���� Y�� �ӵ��� 0���� �����Ѵ�
            yVelocity = 0;
        }

        // �����̽� �ٸ� �Է����� �� ������ ���� ���� ����
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // ĳ���� Y�� �ӷ¿� �������� �����ϰ� ���� �����Ѵ�
            yVelocity = jumpPower;
            isJumping = true;
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
