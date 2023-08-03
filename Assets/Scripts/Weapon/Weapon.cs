using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : MonoBehaviour, Interacts
{
    [Header("Weapon")]
    protected Animator anim;
    protected BoxCollider2D boxColl;
    // 伤害
    public int ATK = 4;
    // 推击力度
    public float pushForce;
    // 冷却
    public float cooldown;
    // 武器等级/稀有度
    public int weaponLevel;
    // 是否在地上可交互
    public bool canInteracts;
    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
    }
    public virtual void Attack()
    {
    }
    public virtual void Tip()
    {
        if (canInteracts)
        {
            mUI.Inst.InteractTip(transform.position, name);
        }
    }
    public virtual void Interact()
    {
        if (canInteracts)
        {
            // 仍有空槽
            if (cPlayer.Inst.weapons.Count < cPlayer.Inst.weaponMax)
            {
                gameObject.tag = cPlayer.Inst.tag;
                transform.parent = cPlayer.Inst.weapon.transform.parent;
                transform.localPosition = Vector3.zero;
                canInteracts = false;
                gameObject.SetActive(false);
                cPlayer.Inst.weapons.Add(gameObject);
                boxColl.enabled = false;
            }
            else
            {
                transform.parent = cPlayer.Inst.weapon.transform.parent;
                transform.localPosition = Vector3.zero;

                GameObject weapon = cPlayer.Inst.weapon;
                weapon.tag = tag;
                weapon.transform.parent = null;
                weapon.GetComponent<Weapon>().canInteracts = true;
                weapon.GetComponent<Weapon>().boxColl.enabled = true;
                cPlayer.Inst.weapons.Remove(weapon);

                gameObject.tag = cPlayer.Inst.tag;
                canInteracts = false;
                cPlayer.Inst.weapon = gameObject;
                cPlayer.Inst.weapons.Add(gameObject);
                boxColl.enabled = false;
            }
            mUI.Inst.ColseInteractTip();
        }
    }
    public virtual void ColseTip()
    {
        if (canInteracts)
        {
            mUI.Inst.ColseInteractTip();
        }
    }
}
