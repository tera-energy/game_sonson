using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Mover_HV : MonoBehaviour
{
    private Rigidbody2D theRB;
    int dir = -1;
    public float speed = 5f;

    public bool bObstacle;
    public bool bMountain;

    private void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveObject();
    }
    void MoveObject()
    {
        if (!GameController_HV.instance.isPlay)
        {
            return;
        }
        if (bObstacle)
        {
            theRB.velocity = new Vector2(speed * dir * GameController_HV.instance.obstacleAddSpeed * GameController_HV.instance.overallSpeed, theRB.velocity.y);
        }
        else if (bMountain)
        {
            var obstacleAddSpeed = GameController_HV.instance.obstacleAddSpeed/2;
            if (obstacleAddSpeed < 1)
            {
                obstacleAddSpeed=1f;
            }
            
            theRB.velocity = new Vector2(speed * dir * obstacleAddSpeed * GameController_HV.instance.overallSpeed, theRB.velocity.y);
        }
        else
        {
            theRB.velocity = new Vector2(speed * dir * GameController_HV.instance.overallSpeed, theRB.velocity.y);
        }
    }

}
