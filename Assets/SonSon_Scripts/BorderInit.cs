using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderInit : MonoBehaviour
{
    [Header("InitBound_to_camera")]
    public Vector2 minBounds;
    public Vector2 maxBounds;
    [SerializeField] public float paddingLeft = .3f;
    [SerializeField] public float paddingRight = .3f;
    public float paddingBottom = 1f;

    [Header("Left, Right Bounder")]
    [SerializeField] GameObject topBoundObject;
    [SerializeField] GameObject bottomBoundObject;
    [SerializeField] GameObject leftBoundObject;
    [SerializeField] GameObject rightBoundObject;

    // Start is called before the first frame update
    void Start()
    {
        InitBounds();
        var tempVL = new Vector2(minBounds.x - paddingLeft, 0);
        if (leftBoundObject != null)
        {
            leftBoundObject.transform.position = tempVL;
        }
        var tempVR = new Vector2(maxBounds.x + paddingRight, 0);
        if (rightBoundObject != null)
        {
            rightBoundObject.transform.position = tempVR;
        }
        var tempVT = new Vector2(0, maxBounds.y - paddingLeft);
        if (topBoundObject != null)
        {
            topBoundObject.transform.position = tempVT;
        }
        var tempVB = new Vector2(0, minBounds.y);
        if (bottomBoundObject != null)
        {
            bottomBoundObject.transform.position = tempVB;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

}
