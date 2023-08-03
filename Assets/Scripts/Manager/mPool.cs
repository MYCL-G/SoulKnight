using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mPool : MonoBehaviour
{
    static mPool inst;
    public static mPool Inst => inst;
    //主池位置
    Transform mainPool;
    //池内容字典
    Dictionary<string, List<GameObject>> poolDic = new Dictionary<string, List<GameObject>>();
    private void Awake()
    {
        inst = this;
        //创建主池
        mainPool = new GameObject("Pool").transform;
    }

    public void AddToPool(string name, GameObject obj)
    {
        //如果没有对应池位置
        if (!poolDic.ContainsKey(name))
        {
            poolDic[name] = new List<GameObject>();
        }
        obj.SetActive(false);
        obj.transform.SetParent(mainPool);
        poolDic[name].Add(obj);
    }
    public GameObject GetFromPool(string name)
    {
        //如果有对应池且有存量
        if (poolDic.ContainsKey(name) && poolDic[name].Count > 0)
        {
            //取件
            GameObject obj = poolDic[name][0];
            //从字典移除
            poolDic[name].RemoveAt(0);
            //复活
            obj.SetActive(true);
            //抛出池区
            obj.transform.SetParent(null);
            //返回使用
            return obj;
        }
        else //没有存量或池
        {
            //读取 创建 返回
            GameObject obj = Instantiate(Resources.Load<GameObject>("GameObject/" + name));
            obj.name = name;
            return obj;
        }
    }
}
