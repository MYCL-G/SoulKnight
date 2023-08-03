using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun98k : Gun
{
    protected override void Fire(int atk)
    {
        anim.SetTrigger("fire");
        RaycastHit2D ray = Physics2D.Raycast(muzzle.position, cPlayer.Inst.direction, 30);
        GameObject bullet = mPool.Inst.GetFromPool(bulletPrefab.name);
        LineRenderer line = bullet.GetComponent<LineRenderer>();
        line.SetPosition(0, muzzle.position);
        line.SetPosition(1, ray.point);

        if (magazine != null)
        {
            mPool.Inst.GetFromPool(cartridgePrefab.name).transform.position = magazine.position;
        }
    }
}
