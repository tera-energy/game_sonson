using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControl_Faster : MonoBehaviour
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
        if (canvasMoveBtn && PlayerControl_HV.instance.ctrlMode == Control_Mode.Swipe)
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

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
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

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            var distance = endTouchPosition.y - startTouchPosition.y;
            var adjstedDistance = (distance * 1f) * 0.01f;
            if (endTouchPosition.y > startTouchPosition.y)
            {
                Debug.Log("## up swipe");

                dir = 1;
                if (prevDir != 0 && prevDir == dir)
                {
                    speed += 1;
                }
                else if (prevDir != 0 && prevDir != dir)
                {
                    speed = 1;
                    speed += 5;
                }
                else
                {
                    speed = 1;
                    speed += 5;
                }

                prevDir = dir;
                outputText.text = $"UP : {adjstedDistance}";
            }
            if (endTouchPosition.y < startTouchPosition.y)
            {
                Debug.Log("## down swipe");

                dir = -1;
                if (prevDir != 0 && prevDir == dir)
                {
                    speed -= 1;
                }
                else if (prevDir != 0 && prevDir != dir)
                {
                    speed = -1;
                    speed += -5;
                }
                else
                {
                    speed = -1;
                    speed += -5;
                }

                prevDir = dir;
                outputText.text = $"DOWN : {adjstedDistance}";
            }

        }

    }
}
