using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRocket : Bullet
{
    [Header("BulletRocket")]
    public float lerp;
    Vector3 targetPos;
    Vector3 direction;
    bool arrived;
    public void SetTarget()
    {
        arrived = false;
        targetPos = cCursor.Inst.transform.position;
    }
    private void FixedUpdate()
    {
        direction = (targetPos - transform.position).normalized;
        if (!arrived)
        {
            transform.right = Vector3.Slerp(transform.right, direction, lerp / Vector2.Distance(transform.position, targetPos));
            rb.velocity = transform.right * speed;
        }
        if (Vector2.Distance(transform.position, targetPos) < 1 && !arrived)
        {
            arrived = true;
        }
    }
}
