using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Rigidbody2D theRB;

    float width;

    float speed = 5f;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        theRB=GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        width = boxCollider.size.x*0.2f;
        theRB.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= width)
        {
            Reposition();
        }
    }
    void Reposition()
    {
        var vector = new Vector2(width * 2f, 0);
        transform.position =(Vector2) transform.position-vector;
    }

}
