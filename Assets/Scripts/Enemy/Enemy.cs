using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    protected int nowHP;
    public int maxHP = 10;

    protected Animator anim;

    [Header("移动参数")]
    public float moveSpeed = 3.0f;
    [Header("攻击参数")]
    // 攻击伤害
    public int attackDamage = 10;
    //攻击范围
    public float attackRange = 2.0f;
    // 攻击冷却
    public float attackCooldown = 2.0f;
    // 上一次使用的时间
    public float lastAttackTime = -Mathf.Infinity;

    //玩家位置
    protected Transform playerTransform;

    protected BoxCollider2D boxColl;
    protected CapsuleCollider2D capsuleColl;
    protected Rigidbody2D rb;

    public GameObject weapon;

    protected bool dead = false;
    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
        capsuleColl = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        nowHP = maxHP;
    }
    protected virtual void Start()
    {
        playerTransform = cPlayer.Inst.transform;
    }
    protected virtual void Update()
    {
        if (dead) return;
        MoveToPlayer();
    }
    protected virtual void MoveToPlayer()
    {
        // 计算怪物和玩家之间的距离
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        // 翻转
        if (transform.position.x - playerTransform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
        // 靠近玩家
        if (distanceToPlayer > attackRange)
        {
            anim.SetBool("move", true);
            Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
            rb.AddForce(moveDirection * moveSpeed * Time.deltaTime);
        }
        if (Time.time - lastAttackTime >= attackCooldown && distanceToPlayer <= attackRange)
        {
            anim.SetBool("move", false);
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }
    protected virtual void AttackPlayer()
    {
        //weapon.GetComponent<Weapon>().Attack();
    }
    public virtual void Hurt(int atk)
    {
        mUI.Inst.LifeHurt(transform.position + new Vector3(0, boxColl.size.y, 0), atk);
        nowHP -= atk;
        if (nowHP < 0)
        {
            dead = true;
            Dead();
        }
    }
    protected virtual void Dead()
    {
        anim.SetBool("dead", true);
        boxColl.enabled = false;
        capsuleColl.enabled = false;
        rb.drag = 100;
    }
}
