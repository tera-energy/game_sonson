using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyStarRotate : MonoBehaviour
{
    [SerializeField] Transform rotationCenter;

    [SerializeField] float rotationRadius=2f,angularSpeed=2f;

    [SerializeField]
    float angle;

    [SerializeField]
    float posX, posY  = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MoveAround();
 
    }
    void MoveAround()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius/3;
        transform.position=new Vector2(posX, posY);
        angle=angle+Time.deltaTime*angularSpeed;
        if (angle >= 360f)
        {
            angle = 0;
        }
    }
}
