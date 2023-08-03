using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [Header("Gun")]
    // 子弹预制体
    public GameObject bulletPrefab;
    // 弹壳预制体
    public GameObject cartridgePrefab;
    // 每次子弹发射量
    public int bulletNum = 1;
    // 多子弹散射角度
    public float spreadAngle;
    protected List<Bullet> bullets;
    // 子弹发射点
    protected Transform muzzle;
    // 抛壳点
    protected Transform magazine;
    //子弹偏移范围
    public float randomShifting = 5;
    // 随机偏移值
    protected float shifting;
    protected override void Awake()
    {
        base.Awake();
        muzzle = transform.Find("muzzle");
        magazine = transform.Find("magazine");
    }
    protected virtual void Update()
    {

    }
    public override void Attack()
    {
        if (cPlayer.Inst.cooldown < 0)
        {
            Fire(ATK);
            cPlayer.Inst.cooldown = cooldown;
        }
    }
    protected virtual void Fire(int atk)
    {
        // 动画
        anim.SetTrigger("fire");

        // 单发子弹
        if (bulletNum == 1)
        {
            GameObject bullet = mPool.Inst.GetFromPool(bulletPrefab.name);
            bullet.transform.position = muzzle.position;
            bullet.tag = tag;
            bullet.GetComponent<Bullet>().atk = atk;
            // 计算角度
            float angle = Mathf.Atan2(cPlayer.Inst.direction.y, cPlayer.Inst.direction.x) * Mathf.Rad2Deg;
            // 随机偏移
            shifting = Random.Range(-randomShifting, randomShifting);
            // 子弹方向
            bullet.transform.rotation = Quaternion.AngleAxis(angle + shifting, Vector3.forward);
            bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(shifting, Vector3.forward) * cPlayer.Inst.direction);
        }
        // 散射子弹
        else
        {
            int median = bulletNum / 2;
            float startingAngle = (bulletNum % 2 == 1) ? 0f : spreadAngle / 2f;
            bullets = new List<Bullet>();
            for (int i = 0; i < bulletNum; i++)
            {
                GameObject bullet = mPool.Inst.GetFromPool(bulletPrefab.name);
                bullet.transform.position = muzzle.position;
                bullet.tag = tag;
                bullet.GetComponent<Bullet>().atk = atk;

                float angleOffset = spreadAngle * (i - median) + startingAngle;
                shifting = Random.Range(-randomShifting, randomShifting);
                Quaternion rotation = Quaternion.AngleAxis(angleOffset + shifting, Vector3.forward);
                bullet.GetComponent<Bullet>().SetSpeed(rotation * cPlayer.Inst.direction);
                bullets.Add(bullet.GetComponent<Bullet>());
            }
        }

        // 抛壳
        if (magazine != null)
        {
            mPool.Inst.GetFromPool(cartridgePrefab.name).transform.position = magazine.position;
        }

    }

}
