using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatorDotween : MonoBehaviour
{
    public float minZ = -45f;
    public float maxZ = 45f;
    float currentZ = 0;

    public float speed=1f;
    // Start is called before the first frame update
    void Start()
    {
        //transform.DORotate(new Vector3(0f, 0f, -50f), 1f).SetLoops(-1,LoopType.Yoyo);
    }
    private void Update()
    {
        /*
        if (currentZ< maxZ)
        {
            currentZ+=Time.deltaTime*speed;
        }
        if (currentZ > maxZ)
        {
            currentZ -= Time.deltaTime * speed;
        }
        if (currentZ< minZ)
        {
            currentZ += Time.deltaTime * speed;
        }
        */
        transform.Rotate(0, 0, speed);
    }


}
