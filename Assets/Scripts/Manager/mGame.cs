using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mGame : MonoBehaviour
{
    int maxHP;
    int maxMP;
    int maxDEF;
    GameObject weapon;
    int money;

    int gold;

    mGame inst;
    public mGame Inst => inst;
    private void Awake()
    {
        inst = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }
    void ChooseRole()
    {

    }
    void OpenNewGround()
    {
        Save();
        GameObject obj = new GameObject("Player");
        obj.tag = "Player";
        obj.AddComponent<cPlayer>();
    }
    void Save()
    {

    }
    void Load()
    {

    }
}
