using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    Rigidbody2D theRB;
    public float xdir = 1f;

    public float speed=0.1f;

    bool bDestroy;
    bool bRotating;

    private void Awake()
    {
        theRB=gameObject.GetComponent<Rigidbody2D>();  
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        moveBird();
        RotateBird();
        StartCoroutine(nameof(DestryObject));
    }
    private void moveBird()
    {
        if (!GameController.instance.isPlay)
        {
            return;
        }
        theRB.velocity = new Vector2(speed * xdir, theRB.velocity.y);
    }
    IEnumerator DestryObject()
    {
        if (!bDestroy && GameController.instance.isPlay)
        {
            bDestroy = true;
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
        
    }
    void RotateBird()
    {
        if (GetComponent<Renderer>().isVisible && GameController.instance.isPlay && !bRotating)
        {
            bRotating = true;
            if (xdir < 0)
            {
                transform.DORotate(new Vector3(0, 0, 45), 1f).SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                transform.DORotate(new Vector3(0, 0, -45), 1f).SetLoops(-1, LoopType.Yoyo);
            }
            
        }
    }

}
