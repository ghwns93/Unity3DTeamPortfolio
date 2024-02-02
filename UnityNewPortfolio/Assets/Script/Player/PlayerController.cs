using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f;
    public float runSpeed = 2.0f;       // �޸��� �ӵ� (���)
    public float jumpPower = 10.0f;
    public float gravity = -10.0f;
    float yVelocity = 0;                // �������� �ӵ�

    private float nowSpeed; // ���� �ӵ�

    private bool isContinueComboAttack;
    private float vSpeed = 0.0f;
    internal bool isAttack = false;
    internal bool isDodge = false;
    internal bool isIdle = false;        // idle�ִϸ��̼� ������ȯ

    bool staminarecovery = false;

    public bool isUiOpen = false;

    public GameObject cameraOrigin;

    private CharacterController cc;
    private Transform tran;
    private Transform charaTran;
    private Animator animator;

    public bool questClear;

    Transform weaponPos;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        tran = GetComponent<Transform>();

        weaponPos = GameObject.Find("WeaponPos").transform;

        //animator = transform.GetChild(0).GetComponent<Animator>();
        animator = GetComponent<Animator>();
        charaTran = transform.GetChild(0).GetComponent<Transform>();

        WeaponslotController.Instance.FreshSlot();
    }

    void Update()
    {
        if (questClear)
        {
            animator.Play("QuestClear");
        }
        else
        {
            #region [ Normal Update ]

            Vector3 velocity = new Vector3(0f, 0f, 0f);

            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            float moveAct = moveX != 0 ? moveX : moveY;
            moveAct = Mathf.Abs(moveAct);

            if (!isAttack)
            {
                transform.forward =
                    new Vector3(
                        Camera.main.transform.forward.x,
                    transform.forward.y,
                    Camera.main.transform.forward.z
                    );
            }

            if (Input.GetButtonDown("Fire1") && !isUiOpen)
            {
                if (!isAttack)
                {
                    if (PlayerState.Instance.Stamina >= PlayerState.Instance.StaminaMax * 0.2f)
                    {
                        StopCoroutine(StaminaRecovery(0f));
                        staminarecovery = false;

                        PlayerState.Instance.Stamina -= PlayerState.Instance.StaminaMax * 0.2f;

                        if (PlayerState.Instance.Stamina < 0)
                            PlayerState.Instance.Stamina = 0;

                        moveAct = 0;

                        isAttack = true;

                        animator.SetTrigger("AttackTrigger");
                        animator.SetBool("AttackEnd", false);

                        Debug.Log("��������");

                        StartCoroutine(StaminaRecovery(2.0f));
                    }
                }
            }

            // ĳ���Ͱ� �ٴڿ� ������ ���
            if (cc.collisionFlags == CollisionFlags.Below)
            {
                // ĳ���� Y�� �ӵ��� 0���� �����Ѵ�
                yVelocity = 0;
            }

            // ���¹̳� ����
            if (staminarecovery)
            {
                PlayerState.Instance.Stamina += 5.0f * Time.deltaTime;

                if (PlayerState.Instance.Stamina >= PlayerState.Instance.StaminaMax)
                {
                    PlayerState.Instance.Stamina = PlayerState.Instance.StaminaMax;
                    staminarecovery = false;
                }
            }

            //�޸���
            if (Input.GetKey(KeyCode.LeftShift))
            {
                StopCoroutine(StaminaRecovery(0f));
                staminarecovery = false;
                if (PlayerState.Instance.Stamina > 0)
                {
                    nowSpeed = speed * runSpeed;
                    animator.SetBool("RunFoward", true);
                    PlayerState.Instance.Stamina -= 5 * Time.deltaTime;
                }
            }
            else
            {
                nowSpeed = speed;
                animator.SetBool("RunFoward", false);
                StartCoroutine(StaminaRecovery(1.0f));
            }

            // �����̽� �ٸ� �Է����� �� ������ ���� ���� ����
            if (Input.GetButtonDown("Jump") && !isDodge && !isUiOpen)
            {
                StopCoroutine(StaminaRecovery(0f));
                staminarecovery = false;
                if (PlayerState.Instance.Stamina >= PlayerState.Instance.StaminaMax * 0.1f)
                {
                    PlayerState.Instance.Stamina -= PlayerState.Instance.StaminaMax * 0.1f;

                    if (PlayerState.Instance.Stamina < 0)
                        PlayerState.Instance.Stamina = 0;
                }

                // ĳ���� Y�� �ӷ¿� �������� �����ϰ� ���� �����Ѵ�
                isDodge = true;

                animator.SetTrigger("JumpTrigger");

                nowSpeed *= 0.7f;

                StartCoroutine(StaminaRecovery(1.5f));
            }

            // ĳ���� ���� �ӵ��� �߷��� �����Ѵ�
            yVelocity += gravity * Time.deltaTime;

            if (!isAttack && !isUiOpen)
            {
                if (moveAct != 0)
                {
                    Vector3 lookForward = new Vector3(cameraOrigin.transform.forward.x, 0f, cameraOrigin.transform.forward.z).normalized;
                    Vector3 lookRight = new Vector3(cameraOrigin.transform.right.x, 0f, cameraOrigin.transform.right.z).normalized;
                    Vector3 moveDir = lookForward * moveY + lookRight * moveX;

                    transform.forward = lookForward;
                    if (!isDodge)
                    {
                        animator.SetBool("WalkFoward", true);
                    }
                    animator.SetBool("Idle", false);

                    velocity = moveDir * nowSpeed;

                }
                else
                {
                    if (!isDodge)
                    {
                        animator.SetBool("Idle", true);
                    }
                    animator.SetBool("WalkFoward", false);
                }
            }
            else if (isUiOpen)
            {
                animator.SetBool("WalkFoward", false);
                animator.SetBool("Idle", true);
            }

            velocity.y += yVelocity;

            cc.Move(velocity * Time.deltaTime);
            #endregion
        }
    }

    void AttackEnd()
    {
        isAttack = false;

        animator.SetBool("AttackEnd", true);

    }

    void JumpTime()
    {
        //���� �ִϸ��̼ǿ��� ����ݱ� ����� ������ ����.
        yVelocity = jumpPower;
    }

    void JumpDown()
    {
        //���� �ִϸ��̼� ����� ���� ���ɻ��·� ����.
        isDodge = false;
    }

    void CheckStartComboAttack()
    {
        // �޺� ������ ��� �Ǵ��� üũ�ϴ� boolean ����
        isContinueComboAttack = false;

        // �޺� ������ �̾����� �ʴ´ٸ�, �ڷ�ƾ�� �����ϱ� ���� IEnumerator ���� ���
        IEnumerator COR_CheckComboAttack = CheckComboAttack();

        StartCoroutine(COR_CheckComboAttack);
    }

    // �޺� ���� üũ ����
    void CheckEndComboAttack()
    {
        // boolean ������ false ���,
        if (!isContinueComboAttack)
        {
            AttackEnd();    // �ִϸ��̼��� �����Ų��.
        }
    }

    IEnumerator CheckComboAttack()
    {
        // ���� ��ư�� ���ȴ��� üũ
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        // ���ȴٸ�, boolean ������ True�� �ٲ۴�.
        isContinueComboAttack = true;
    }

    IEnumerator StaminaRecovery(float second)
    {
        yield return new WaitForSeconds(second);

        staminarecovery = true;

        yield break;
    }
}
