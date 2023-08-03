using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;
    AnimatorStateInfo info;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.SetTrigger("boom");
    }
    public void EndToRemove()
    {
        mPool.Inst.AddToPool(name, gameObject);
    }
}
