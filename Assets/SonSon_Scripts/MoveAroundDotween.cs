using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveAroundDotween : MonoBehaviour
{
    public Transform[] wp;
    Vector3 targetPos;
    
    int wpIndex=0;

    bool bRotating;
    int xdir = 1;
    public float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        //transform.DORotate(new Vector3(0, 0, 10), 1f).SetLoops(-1, LoopType.Yoyo);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        moveBird();
       
    }
  
    void moveBird()
    {
        if (transform.position == wp[0].position)
        {
            wpIndex = 0;
            xdir = 1;
            targetPos = wp[1].position;
        }
        if (transform.position == wp[1].position)
        {
            wpIndex = 1;
            xdir = -1;
            targetPos = wp[2].position;
        }
        if (transform.position == wp[2].position)
        {
            wpIndex = 2;
            xdir = 1;
            targetPos = wp[0].position;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
        transform.localScale = new Vector3(xdir, transform.localScale.y, transform.localScale.z);
        

    }
}
