using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScalerDoTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var originalScale = transform.localScale;
        transform.localScale = Vector3.one*Random.Range(0.01f,0.02f);
        var targetSacle = originalScale.x*1.1f;
        transform.DOScale(new Vector3(targetSacle, targetSacle, targetSacle),Random.Range(1.2f,1.5f)).SetLoops(-1,LoopType.Yoyo);
    }

    
}
