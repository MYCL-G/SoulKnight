using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(BoxCollider2D))]
public class cPlayer : MonoBehaviour
{
    static cPlayer inst;
    public static cPlayer Inst => inst;

    // 生命
    public int maxHp;
    int nowHp;
    // 护甲
    public int maxDef;
    int nowDef;
    // 能量
    public int maxMp;
    int nowMp;

    // 武器上限
    public int weaponMax = 2;
    // 当前使用武器
    public GameObject weapon;
    // 当前所有武器
    public List<GameObject> weapons = new List<GameObject>();
    // 当前武器索引
    int weaponIndex = 0;
    // 当前武器是否是枪
    bool isGun;

    // 当前单局游戏币
    public int money;

    // 水平输入
    float xInput;
    // 垂直输入
    float yInput;
    // 滚轮输入
    float scrollAmount;

    // 武器切换冷却
    float weaponChangeTime = 0;

    // 武器公用冷却时间
    public float cooldown;
    // 冷却倍速
    public float cooldownMultiple = 1;
    // 鼠标方向
    public Vector2 direction;
    // 当前交互对象
    GameObject interactObject;
    // 受击事件
    UnityAction hurtAction;
    // 无敌时长
    float invincibleTime = 1;
    // 剩余无敌时间
    float nowInvincibleTime = 0;
    private void Awake()
    {
        inst = this;
        isGun = weapon.GetComponent<Gun>() != null;
        weapons.Add(weapon);
        hurtAction = () =>
        {
            nowInvincibleTime = invincibleTime;
            //mUI.Inst.;
        };
        nowHp = maxHp;
        nowDef = maxDef;
        nowMp = maxMp;
    }
    void Start()
    {
        mUI.Inst.PlayerUpdate(maxHp, nowHp, maxDef, nowDef, maxMp, nowMp);
    }
    void Update()
    {
        cooldown -= Time.deltaTime * cooldownMultiple;
        nowInvincibleTime -= Time.deltaTime;
        //输入
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        scrollAmount = Input.GetAxis("Mouse ScrollWheel");
        //左右翻转
        direction = (cCursor.Inst.transform.position - weapon.transform.position).normalized;
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        // 武器旋转
        WeaponForword();
        // 武器切换
        SwitchWeapon();
        // 物品交互
        Interactive();
        // 攻击
        if (Input.GetMouseButton(0))
        {
            weapon.GetComponent<Weapon>().Attack();
        }
    }
    private void FixedUpdate()
    {
        //根据输入移动
        if (xInput != 0 || yInput != 0)
        {
            transform.position += new Vector3(xInput, yInput, 0).normalized * (Time.deltaTime * 4);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interact"))
        {
            interactObject = collision.gameObject;
            interactObject.GetComponent<Interacts>().Tip();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactObject == collision.gameObject)
        {
            interactObject.GetComponent<Interacts>().ColseTip();
            interactObject = null;
        }
    }
    // 武器切换
    void SwitchWeapon()
    {
        weaponChangeTime -= Time.deltaTime;
        if (scrollAmount != 0)
        {
            if (weaponChangeTime < 0)
            {
                weaponChangeTime = 0.5f;
                weapons[weaponIndex].gameObject.SetActive(false);
                if (scrollAmount > 0)
                {
                    if (weaponIndex == 0)
                        weaponIndex = weapons.Count - 1;
                    else
                        weaponIndex -= 1;
                }
                else if (scrollAmount < 0)
                {
                    if (weaponIndex == weapons.Count - 1)
                        weaponIndex = 0;
                    else
                        weaponIndex += 1;
                }
                weapons[weaponIndex].gameObject.SetActive(true);
                weapon = weapons[weaponIndex].gameObject;
                if (!weapon.CompareTag(tag))
                    weapon.tag = tag;
                isGun = weapon.GetComponent<Gun>() != null;
            }
        }
    }
    // 调整武器朝向
    void WeaponForword()
    {
        if (isGun)
        {
            if (direction.x > 0)
            {
                weapon.transform.localScale = Vector3.one;

            }
            else if (direction.x < 0)
            {
                weapon.transform.localScale = new Vector3(-1, -1, 1);
            }
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            weapon.transform.localScale = Vector3.one;
        }
    }
    // 物品交互
    void Interactive()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactObject != null)
        {
            interactObject.GetComponent<Interacts>().Interact();
            interactObject = null;
        }
    }
    public void Hurt(int atk)
    {
        if (nowInvincibleTime > 0)
        {

        }
        else
        {
            if (nowDef > atk)
            {
                nowDef -= atk;
                mUI.Inst.PlayerUpdateDef(maxDef, nowDef);
            }
            else
            {
                nowHp -= (atk - nowDef);
                nowDef = 0;
                mUI.Inst.PlayerUpdateDef(maxDef, nowDef);
                mUI.Inst.PlayerUpdateHp(maxHp, nowHp);
            }

            hurtAction();
            if (nowHp < 0)
                Dead();
        }
    }
    void Dead()
    {

    }
}
