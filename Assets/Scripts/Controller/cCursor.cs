using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCursor : MonoBehaviour
{
    static cCursor inst;
    public static cCursor Inst { get { return inst; } }
    Vector2 mousePos;
    private void Awake()
    {
        inst = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
