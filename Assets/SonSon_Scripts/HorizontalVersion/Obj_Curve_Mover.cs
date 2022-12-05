using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Curve_Mover : MonoBehaviour
{
    public Rigidbody2D theRB;
    int dir = -1;
    public float speed = 12f;
    public float gravity = 0.1f;
    public float gravyAddAjuster = 1.5f;

    private void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //theRB.gravityScale = gravity;
    }
    private void Update()
    {
        gravity += Time.deltaTime / (gravyAddAjuster/ GameController_HV.instance.obstacleAddSpeed);
        gravity = Mathf.Clamp(gravity, 0f, 1f);
        theRB.gravityScale = gravity;
    }

    private void FixedUpdate()
    {
        MoveObject();
        
    }
    void MoveObject()
    {
        //theRB.velocity = new Vector2(speed * dir * GameController_HV.instance.overallSpeed, theRB.velocity.y);
        var obstacleAddSpeed = GameController_HV.instance.obstacleAddSpeed * .6f;
        if (obstacleAddSpeed < 1)
        {
            obstacleAddSpeed = 1f;
        }
        theRB.velocity = new Vector2(speed * dir * obstacleAddSpeed, theRB.velocity.y);
    }
}
