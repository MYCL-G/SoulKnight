using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLaser : Gun
{
    bool isShooting;
    LineRenderer laser;
    protected override void Awake()
    {
        base.Awake();
        laser = muzzle.GetComponent<LineRenderer>();
    }
    public override void Attack()
    {
        isShooting = true;
        laser.enabled = true;
        anim.SetBool("shooting", isShooting);
        Fire();
    }
    protected override void Update()
    {
        if (!Input.GetButton("Fire1"))
        {
            isShooting = false;
            laser.enabled = false;
            anim.SetBool("shooting", isShooting);
        }
    }
    void Fire()
    {
        RaycastHit2D ray = Physics2D.Raycast(muzzle.position, cPlayer.Inst.direction, 30, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Enemy"));
        if (ray.collider != null)
        {
            laser.SetPosition(0, muzzle.position);
            laser.SetPosition(1, ray.point);
        }
        else
        {
            laser.SetPosition(0, muzzle.position);
            laser.SetPosition(1, muzzle.position + (Vector3)cPlayer.Inst.direction * 10);
        }
    }
}
