using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControl : MonoBehaviour
{
    private Rigidbody2D theRB;
    float dir = 0f;
    float prevDir = 0f;
    float speed = 0f;
    public float speedFriction = 1f;

    public Text outputText;

    Vector2 startTouchPosition;
    Vector2 currentPosition;
    Vector2 endTouchPosition;
    bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    public GameObject canvasMoveBtn;

    private void Awake()
    {
        theRB = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(canvasMoveBtn && PlayerControl_HV.instance.ctrlMode == Control_Mode.Swipe)
        {
            canvasMoveBtn.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = 1;
            if (prevDir != 0 && prevDir == dir)
            {
                speed += 1;
            }
            else if (prevDir != 0 && prevDir != dir)
            {
                speed = 3;
            }
            else
            {
                speed = 3;
            }
            prevDir = dir;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dir = -1;
            if (prevDir != 0 && prevDir == dir)
            {
                speed -= 1;
            }
            else if (prevDir != 0 && prevDir != dir)
            {
                speed = -3;
            }
            else
            {
                speed = -3;
            }
            prevDir = dir;
        }

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) { 
            Swipe();
        }

        var absSpeed = Mathf.Abs(speed);
        absSpeed -= Time.deltaTime * speedFriction;
        absSpeed = Mathf.Clamp(absSpeed, 0f, 10f);
        speed = absSpeed * dir;
    }
    private void FixedUpdate()
    {
        if (PlayerControl_HV.instance.bAlive && GameController_HV.instance.isPlay && PlayerControl_HV.instance.ctrlMode == Control_Mode.Swipe)
            theRB.velocity = new Vector2(theRB.velocity.x, speed);
    }
    public void Swipe()
    {
        //Input.touchCount > 0 &&
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            var distance = currentPosition - startTouchPosition;
            /* moving */
            if (!stopTouch)
            {
                if (distance.x < -swipeRange)
                {
                    Debug.Log("## left swipe");
                    stopTouch = true;
                }
                else if (distance.x > swipeRange)
                {
                    Debug.Log("## right swipe");
                    stopTouch = true;
                }
                else if (distance.y > swipeRange)
                {
                    Debug.Log("## up swipe");

                    dir = 1;
                    if (prevDir != 0 && prevDir == dir)
                    {
                        speed += 1;
                    }
                    else if (prevDir != 0 && prevDir != dir)
                    {
                        speed = 3;
                    }
                    else
                    {
                        speed = 3;
                    }
                    prevDir = dir;
                    stopTouch = true;
                }
                else if (distance.y < -swipeRange)
                {
                    Debug.Log("## down swipe");

                    dir = -1;
                    if (prevDir != 0 && prevDir == dir)
                    {
                        speed -= 1;
                    }
                    else if (prevDir != 0 && prevDir != dir)
                    {
                        speed = -3;
                    }
                    else
                    {
                        speed = -3;
                    }
                    prevDir = dir;
                    stopTouch = true;
                }

            }
            /* moving END */

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            var distance = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(distance.x) < tapRange && Mathf.Abs(distance.y) < tapRange)
            {
                Debug.Log("## tap swipe");
            }

        }

    }
}
