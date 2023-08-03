using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, Interacts
{
    enum State
    {
        Close,
        Full,
        Empty
    }
    public Sprite closeSprite;
    public Sprite fullSprite;
    public Sprite emptySprite;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closeSprite;
    }

    public void Interact()
    {
        // 关闭交互提示
        ColseTip();
        // 随机开箱
        if (spriteRenderer.sprite == closeSprite)
        {
            if (Random.Range(0, 1) > 0.3)
            {
                spriteRenderer.sprite = fullSprite;
            }
            else
            {
                spriteRenderer.sprite = emptySprite;
            }
        }
        // 空箱结算
        if (spriteRenderer.sprite == emptySprite)
        {
            gameObject.tag = "Untagged";
            Destroy(this);
        }
        // 开箱
        if (spriteRenderer.sprite == fullSprite)
        {
            //TODO 获得宝箱奖励
            spriteRenderer.sprite = emptySprite;
        }

    }

    public void Tip()
    {
        // TODO 完成宝箱提示
    }

    public void ColseTip()
    {
        // TODO 关闭宝箱提示
    }
}