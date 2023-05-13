using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    private Vector3 startPosition;

    private Tween outlineTween;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        // Move the background up for 5 seconds
        StartCoroutine(RepeatAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator RepeatAnimation()
    {
        while (true)
        {
            // Move the background up for 90 seconds
            outlineTween = transform.DOMoveY(startPosition.y + 100f,90f)
                .SetEase(Ease.Linear);

            yield return new WaitForSeconds(60f);

            // Move the background back down for 90 seconds
            outlineTween = transform.DOMoveY(startPosition.y, 90f)
                .SetEase(Ease.Linear);

            yield return new WaitForSeconds(90f);
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
