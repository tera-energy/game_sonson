using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Rigidbody2D theRB;

    float mobile_horizontal_axis;

    Vector2 minBounds;
    Vector2 maxBounds;
    [SerializeField] float paddingLeft = .3f;
    [SerializeField] float paddingRight = .3f;

    public float speed = 5f;

    [SerializeField]
    private Transform groundCheckPosition;
    [SerializeField]
    private LayerMask groundLayer;

    public bool bAlive = true;

    bool isRightDown;
    bool isLeftDown;
    int dir = 0;

    public bool isGrounded;

    private void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitBounds();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ArrowBtnDown(false);
        if (Input.GetKeyDown(KeyCode.D))
            ArrowBtnDown(true);
        if (Input.GetKeyUp(KeyCode.A))
            ArrowBtnUp(false);
        if (Input.GetKeyUp(KeyCode.D))
            ArrowBtnUp(true);
        CheckIfGrounded();
        ClampPlayerWithinScreen();
    }

    private void FixedUpdate()
    {
        Debug.Log(dir);
        theRB.velocity = new Vector2(speed * dir, theRB.velocity.y);
        //MovePlayer();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        Debug.Log("## minBounds : " + minBounds);
        Debug.Log("## maxBounds : " + maxBounds);
    }

    void MovePlayer()
    {
        if (!bAlive)
        {
            return;
        }
        float h = Input.GetAxisRaw("Horizontal");
        h += mobile_horizontal_axis;

        theRB.velocity = new Vector2(speed * dir, theRB.velocity.y);
        if (h > 0)
        {
            var tempScale = transform.localScale;
            if (tempScale.x > 0)
            {
                tempScale.x = -tempScale.x;
            }
            transform.localScale = tempScale;

        }
        else if (h < 0)
        {
//            theRB.velocity = new Vector2(-speed, theRB.velocity.y);
            var tempScale = transform.localScale;
            tempScale.x = Mathf.Abs(tempScale.x);
            
            transform.localScale = tempScale;

        }
        else
        {
  //          theRB.velocity = new Vector2(0f, theRB.velocity.y);
        }
        

    }
    void ClampPlayerWithinScreen()
    {
        Vector2 newPos = transform.position;
        newPos.x = Mathf.Clamp(transform.position.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        transform.position = newPos;
    }

    public void ArrowBtnDown(bool isRight){
        if (isRight)
        {
            isRightDown = true;
            var tempScale = transform.localScale;
            tempScale.x = Mathf.Abs(tempScale.x);

            transform.localScale = tempScale;
            dir = 1;
        }
        else
        {
            isLeftDown = true;
            var tempScale = transform.localScale;
            if (tempScale.x > 0)
            {
                tempScale.x = -tempScale.x;
            }
            transform.localScale = tempScale;
            dir = -1;
        }
    }

    public void ArrowBtnUp(bool isRight){
        if (isRight)
        {
            isRightDown = false;
            if (isLeftDown)
            {
                dir = -1;
                return;
            }
        }
        else
        {
            isLeftDown = false;
            if (isRightDown)
            {
                dir = 1;
                return;
            }
        }
        dir = 0;
    }

    public void MovePlayerMobile(int horizontal_axis_raw)
    {
        mobile_horizontal_axis = horizontal_axis_raw;
        Mathf.Clamp(mobile_horizontal_axis, -1f, 1f);

    }
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

    }

}
