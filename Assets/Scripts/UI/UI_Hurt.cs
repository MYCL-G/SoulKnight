using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hurt : MonoBehaviour
{
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= 0.01f;
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + 1, rectTransform.rotation.z);
            yield return null;
        }
    }
}
