using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : Weapon
{
    public List<GameObject> hurted = new List<GameObject>();

    public override void Attack()
    {
        if (cPlayer.Inst.cooldown < 0)
        {
            hurted.Clear();
            Hit(ATK);
            cPlayer.Inst.cooldown = cooldown;
        }
    }
    protected void Hit(int atk)
    {
        anim.SetTrigger("attack");
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && CompareTag("Player"))
            // 仅造成一次伤害
            if (!hurted.Contains(collision.gameObject))
            {
                collision.GetComponent<Enemy>().Hurt(ATK);
                // 将怪物击退
                Vector2 direction = collision.transform.position - transform.position;
                collision.GetComponent<Rigidbody2D>().AddForce(direction.normalized * pushForce, ForceMode2D.Impulse);
                hurted.Add(collision.gameObject);
            }
            else
            {
            }
        if (collision.CompareTag("Player") && CompareTag("Enemy"))
            // 仅造成一次伤害
            if (!hurted.Contains(collision.gameObject))
            {
                collision.GetComponent<cPlayer>().Hurt(ATK);
            }
            else
            {
            }
    }
}
