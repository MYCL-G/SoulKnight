using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mUI : MonoBehaviour
{
    static mUI inst;
    public static mUI Inst => inst;

    public RectTransform screenUI;
    public RectTransform worldUI;
    [Header("受伤UI")]
    public GameObject hurtObj;
    public float fadeDuration = 2f;
    public float riseSpeed = 1f;
    [Header("交互UI")]
    public GameObject interactUI;
    TextMeshProUGUI interactText;
    [Header("玩家UI")]
    public RectTransform leftUp;
    RectTransform hp;
    TextMeshProUGUI textPlayerHp;
    Image imgPlayerHp;
    RectTransform def;
    TextMeshProUGUI textPlayerDef;
    Image imgPlayerDef;
    RectTransform mp;
    TextMeshProUGUI textPlayerMp;
    Image imgPlayerMp;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        inst = this;

        hp = leftUp.Find("HP") as RectTransform;
        textPlayerHp = hp.Find("hpText").GetComponent<TextMeshProUGUI>();
        imgPlayerHp = hp.Find("nowHP").GetComponent<Image>();

        def = leftUp.Find("DEF") as RectTransform;
        textPlayerDef = def.Find("defText").GetComponent<TextMeshProUGUI>();
        imgPlayerDef = def.Find("nowDEF").GetComponent<Image>();

        mp = leftUp.Find("MP") as RectTransform;
        textPlayerMp = mp.Find("mpText").GetComponent<TextMeshProUGUI>();
        imgPlayerMp = mp.Find("nowMP").GetComponent<Image>();

        interactText = interactUI.GetComponentInChildren<TextMeshProUGUI>();
    }
    // 提示UI
    public void InteractTip(Vector3 pos, string str, Color color = new Color())
    {
        if (color == new Color(0, 0, 0, 0))
            color = Color.white;
        interactText.color = color;
        interactText.text = str;
        Vector3 point = pos + Vector3.up;
        interactUI.GetComponent<RectTransform>().position = point;
        interactUI.SetActive(true);
    }
    public void ColseInteractTip()
    {
        interactUI.SetActive(false);
    }
    // 生物受伤飘字
    public void LifeHurt(Vector3 pos, int damage)
    {
        GameObject newHurtObj = Instantiate(hurtObj, Vector3.zero, Quaternion.identity, screenUI);
        print(newHurtObj.name);
        TextMeshProUGUI damageText = newHurtObj.GetComponentInChildren<TextMeshProUGUI>();
        damageText.text = damage.ToString();
        Vector3 point = Camera.main.WorldToScreenPoint(pos);
        newHurtObj.GetComponent<RectTransform>().position = point;
    }
    // 更新玩家数据
    public void PlayerUpdate(int maxHp, int nowHp, int maxDef, int nowDef, int maxMp, int nowMp)
    {
        PlayerUpdateHp(maxHp, nowHp);
        PlayerUpdateDef(maxDef, nowDef);
        PlayerUpdateMp(maxMp, nowMp);
    }
    // 更新玩家血量
    public void PlayerUpdateHp(int maxHp, int nowHp)
    {
        textPlayerHp.text = nowHp + "/" + maxHp;
        imgPlayerHp.rectTransform.localScale = new Vector3(nowHp / maxHp, 1, 1);
    }
    // 更新玩家护甲
    public void PlayerUpdateDef(int maxDef, int nowDef)
    {
        textPlayerDef.text = maxDef + "/" + nowDef;
        imgPlayerDef.rectTransform.localScale = new Vector3(maxDef / nowDef, 1, 1);
    }
    // 更新玩家能量
    public void PlayerUpdateMp(int maxMp, int nowMp)
    {
        textPlayerMp.text = maxMp + "/" + nowMp;
        imgPlayerMp.rectTransform.localScale = new Vector3(maxMp / nowMp, 1, 1);
    }
}
