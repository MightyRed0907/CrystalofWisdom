using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    public float outlineThickness = 0.25f;
    public float repeatDelay = 5f;

    private Tween outlineTween;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.materialForRendering.SetFloat("_OutlineWidth", 0f);
        AnimateOutlineThickness();
    }

    void AnimateOutlineThickness()
    {
        outlineTween = DOTween.To(() => text.materialForRendering.GetFloat("_OutlineWidth"), x => text.materialForRendering.SetFloat("_OutlineWidth", x), outlineThickness, 1f).SetEase(Ease.OutQuad).OnComplete(ResetOutlineThickness).SetDelay(1.5f);
    }

    void ResetOutlineThickness()
    {
        outlineTween = DOTween.To(() => text.materialForRendering.GetFloat("_OutlineWidth"), x => text.materialForRendering.SetFloat("_OutlineWidth", x), 0f, 1f).SetEase(Ease.OutQuad).OnComplete(AnimateOutlineThickness).SetDelay(repeatDelay);
    }

    void OnDisable()
    {
        if (outlineTween != null)
        {
            outlineTween.Kill();
        }
    }
}
