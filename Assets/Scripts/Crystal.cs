using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private Vector3 startScale;

    private Tween outlineTween;
    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
        StartCoroutine(RepeatAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator RepeatAnimation()
    {
        while(true)
        {
            outlineTween = transform.DOScale(startScale * 1.05f, 1f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1f);
            outlineTween = transform.DOScale(startScale, 1f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1f);

            outlineTween = transform.DOScale(startScale * 1.03f, 0.7f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.7f);
            outlineTween = transform.DOScale(startScale, 0.7f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.7f);

            yield return new WaitForSeconds(8f);
        }  
    }

    void OnDisable()
    {
        if (outlineTween != null)
        {
            outlineTween.Kill();
        }
    }
}
