using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Search;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float health;
    public bool isInvicible;
    public bool canMove;
    public bool isDead;

    Animator anim;
    EnemyTarget enTarget;
    public Rigidbody rigid;
    public float delta;

    List<Rigidbody> ragdollRigids = new List<Rigidbody>();
    List<Collider> ragdollColliders = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        anim = GetComponentInChildren<Animator>();
        enTarget= GetComponent<EnemyTarget>();
        enTarget.Init(anim);

        rigid = GetComponent<Rigidbody>();

        InitRagdoll();
    }

    void InitRagdoll()
    {
        Rigidbody[] rigs = GetComponentsInChildren<Rigidbody>();
        for(int i =0; i < rigs.Length; i++)
        {
            if (rigs[i] == rigid)
                continue;

            ragdollRigids.Add(rigs[i]);
            rigs[i].isKinematic = true;

            Collider col = rigs[i].gameObject.GetComponent<Collider>();
            col.isTrigger = true;
            ragdollColliders.Add(col);
        }
    }

    public void EnableRagdoll()
    {
        for(int i =0; i<ragdollRigids.Count; i++)
        {
            ragdollRigids[i].isKinematic=false;
            ragdollColliders[i].isTrigger = false;
        }

        Collider controllercollider = rigid.gameObject.GetComponent<Collider>();
        controllercollider.enabled = false;
        rigid.isKinematic = true;
    }

    IEnumerator CloseAnimator()
    {
        yield return new WaitForEndOfFrame();
        anim.enabled = false;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        delta= Time.deltaTime;
        canMove = anim.GetBool("canMove");
        
        if(health <= 0)
        {
            if(!isDead)
            {
                isDead= true;
                EnableRagdoll();
            }
        }
        
        if(isInvicible)
        {
            isInvicible = anim.GetBool("canMove");
        }
    }
    
    public void DoDamage(float v)
    {
        if (isInvicible)
            return;

        health -= v;
        isInvicible = true;
        anim.Play("damage_1");
    }
}
