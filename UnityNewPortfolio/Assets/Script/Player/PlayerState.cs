using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //�̱��� ����
    private static PlayerState playerInstance = null;

    public GameObject hitPrefab;

    // �� ������ ���޹��� ��ũ��Ʈ
    public Enemy_VS VS;
    public Enemy_VA VA;
    BossScript bs;

    [SerializeField]
    private float hp;

    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField]
    private float hpmax;

    public float HpMax
    {
        get { return hpmax; }
        set { hpmax = value; }
    }

    [SerializeField]
    private float stamina;

    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }

    [SerializeField]
    private float staminamax;

    public float StaminaMax
    {
        get { return staminamax; }
        set { staminamax = value; }
    }

    [SerializeField]
    private int level;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    [SerializeField]
    private int money;

    public int Money
    {
        get { return money; }
        set { money = value; }
    }


    void Awake()
    {
        if (playerInstance == null)
        {
            playerInstance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static PlayerState Instance
    {
        get
        {
            if (null == playerInstance) return null;
            return playerInstance;
        }
    }

    // �÷��̾�� �������� ���� �̺�Ʈ �Լ�

    public void PlayerHit()
    {
        if (VS != null)
        {
            VS.AttackAction();
        }
        else if (VA != null)
        {
            VA.AttackAction();
        }
        else if(bs != null)
        {
            bs.AttackAction();
        }
    }


    public void DamageAction(int damage)
    {
        float playerHp = Hp;

        // ���� ���ݷ¸�ŭ �÷��̾��� ü���� ��´�
        playerHp -= damage;

        Hp = playerHp;

        //Debug.Log(playerHp);

        // �÷��̾��� HP�� 0���� ũ�� �ǰ� ȿ�� ON
        if (playerHp > 0)
        {
            StartCoroutine(PlayHitEffect());
            //PlayParticleEffect();
        }
    }

    IEnumerator PlayHitEffect()
    {
        Debug.Log("�ǰ�����Ʈ ON");

        // 0.3�ʰ� ����Ѵ�
        yield return new WaitForSeconds(0.5f);
        PlayParticleEffect();

        Debug.Log("�ǰ�����Ʈ OFF");
    }

    void PlayParticleEffect()
    {
        // ��ƼŬ�� ������ ��ġ
        Vector3 spawnPosition = transform.position;

        // ��ƼŬ�� �����ϰ� ������ �Ҵ�
        GameObject particleObject = Instantiate((GameObject)hitPrefab, spawnPosition, Quaternion.identity);

        // ��ƼŬ �ý��� ������Ʈ ��������
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();

        // ��ƼŬ�� ����Ǵ� ���� ���
        float duration = particleSystem.main.duration;

        // �÷��̾� �ǰ���
        SoundManager.soundManager.SEPlay(SEType.PlayerHit);

        Destroy(particleObject, duration);
    }
}
