using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cartridge : MonoBehaviour
{
    public float speed;
    public float stopTime = 0.5f;
    public float fadeSpeed = 0.01f;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b);
        float shifting = Random.Range(-30, 30);
        rb.velocity = Quaternion.AngleAxis(shifting, Vector3.forward) * Vector3.up * speed;
        StartCoroutine(Stop());
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(stopTime);
        rb.velocity = Vector2.zero;
        //rb.gravityScale = 0;
        while (sprite.color.a > 0)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - fadeSpeed);
            yield return new WaitForFixedUpdate();
        }
        mPool.Inst.AddToPool("cartridge", gameObject);
    }
}
