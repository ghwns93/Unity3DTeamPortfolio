using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f;
    public float runSpeed = 2.0f;       // 달리기 속도 (배속)
    public float jumpPower = 10.0f;
    public float gravity = -10.0f;
    float yVelocity = 0;                // 떨어지는 속도

    private float nowSpeed; // 최종 속도

    private bool isContinueComboAttack;
    private float vSpeed = 0.0f;
    internal bool isAttack = false;
    internal bool isDodge = false;
    internal bool isIdle = false;        // idle애니메이션 상태전환

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

                        Debug.Log("공격판정");

                        StartCoroutine(StaminaRecovery(2.0f));
                    }
                }
            }

            // 캐릭터가 바닥에 착지한 경우
            if (cc.collisionFlags == CollisionFlags.Below)
            {
                // 캐릭터 Y축 속도를 0으로 설정한다
                yVelocity = 0;
            }

            // 스태미나 관련
            if (staminarecovery)
            {
                PlayerState.Instance.Stamina += 5.0f * Time.deltaTime;

                if (PlayerState.Instance.Stamina >= PlayerState.Instance.StaminaMax)
                {
                    PlayerState.Instance.Stamina = PlayerState.Instance.StaminaMax;
                    staminarecovery = false;
                }
            }

            //달리기
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

            // 스페이스 바를 입력했을 때 점프를 하지 않은 상태
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

                // 캐릭터 Y축 속력에 점프력을 적용하고 상태 변경한다
                isDodge = true;

                animator.SetTrigger("JumpTrigger");

                nowSpeed *= 0.7f;

                StartCoroutine(StaminaRecovery(1.5f));
            }

            // 캐릭터 수직 속도에 중력을 적용한다
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
        //점프 애니메이션에서 도움닫기 모션이 끝난후 점프.
        yVelocity = jumpPower;
    }

    void JumpDown()
    {
        //점프 애니메이션 종료시 점프 가능상태로 변경.
        isDodge = false;
    }

    void CheckStartComboAttack()
    {
        // 콤보 공격이 계속 되는지 체크하는 boolean 변수
        isContinueComboAttack = false;

        // 콤보 어택이 이어지지 않는다면, 코루틴을 종료하기 위해 IEnumerator 변수 사용
        IEnumerator COR_CheckComboAttack = CheckComboAttack();

        StartCoroutine(COR_CheckComboAttack);
    }

    // 콤보 어택 체크 종료
    void CheckEndComboAttack()
    {
        // boolean 변수가 false 라면,
        if (!isContinueComboAttack)
        {
            AttackEnd();    // 애니메이션을 종료시킨다.
        }
    }

    IEnumerator CheckComboAttack()
    {
        // 공격 버튼이 눌렸는지 체크
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        // 눌렸다면, boolean 변수를 True로 바꾼다.
        isContinueComboAttack = true;
    }

    IEnumerator StaminaRecovery(float second)
    {
        yield return new WaitForSeconds(second);

        staminarecovery = true;

        yield break;
    }
}
