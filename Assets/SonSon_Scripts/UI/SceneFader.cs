using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    [SerializeField] RectTransform fader;

    // Start is called before the first frame update
    void Start()
    {
        if (fader)
        {
            fader.gameObject.SetActive(true);
            // ALPHA
            LeanTween.alpha(fader, 1, 0);
            LeanTween.alpha(fader, 0, 1f).setOnComplete(() =>
            {
                fader.gameObject.SetActive(false);
            });

            // SCALE
            /*
            LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
            LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
                fader.gameObject.SetActive(false);
            });
            */
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
