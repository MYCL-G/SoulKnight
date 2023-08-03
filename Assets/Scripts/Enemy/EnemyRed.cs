using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : Enemy
{
    //攻击范围
    public float attackHorizontal;
    public float attackVertical;
    new bool attackRange
    {
        get
        {
            Vector3 distance = playerTransform.position - transform.position;
            return Mathf.Abs(distance.x) < attackHorizontal && Mathf.Abs(distance.y) < attackVertical;
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        MoveToPlayer();
    }
    protected override void MoveToPlayer()
    {
        if (transform.position.x - playerTransform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
        // 如果玩家在攻击范围内，就攻击玩家
        if (attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
        // 否则就靠近玩家
        else if (!attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }
    protected override void AttackPlayer()
    {

    }
    public override void Hurt(int atk)
    {
        mUI.Inst.LifeHurt(transform.position + new Vector3(0, boxColl.size.y, 0), atk);
        nowHP -= atk;
        if (nowHP < 0)
        {
            Dead();
        }
    }
    protected override void Dead()
    {

    }
}
