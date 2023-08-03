using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRocket : Gun
{
    protected override void Fire(int atk)
    {
        int median = bulletNum / 2;
        for (int i = 0; i < bulletNum; i++)
        {
            GameObject bullet = mPool.Inst.GetFromPool(bulletPrefab.name);
            bullet.transform.position = muzzle.position;
            bullet.tag = tag;
            if (bulletNum % 2 == 1)
            {
                bullet.transform.right = Quaternion.AngleAxis(spreadAngle * (i - median), Vector3.forward) * cPlayer.Inst.direction;
            }
            else
            {
                bullet.transform.right = Quaternion.AngleAxis(spreadAngle * (i - median) + spreadAngle / 2, Vector3.forward) * cPlayer.Inst.direction;
            }
            bullet.GetComponent<BulletRocket>().SetTarget();
        }
        //base.Fire(tag);
        //foreach (Bullet bullet in bullets)
        //{
        //    (bullet as BulletRocket).SetTarget();
        //}
    }
}
