using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSonSonHop : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheckPosition;
    [SerializeField]
    private Transform LCheckPosition;
    [SerializeField]
    private Transform RCheckPosition;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField] SpriteRenderer sonsonSR;
    Rigidbody2D theRB;

    public bool isGrounded;
    public bool isLeftBordred;
    public bool isRightBordred;

    public float xdir = 2.5f;
    public float maxHopTimer = 3f;
    public float currentHopTimer;
    public float hopSpeed = 1f;

    private void Awake()
    {
        theRB=GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        Hop();
    }
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);
        isLeftBordred = Physics2D.Raycast(LCheckPosition.position, Vector2.left, 0.1f, groundLayer);
        isRightBordred = Physics2D.Raycast(RCheckPosition.position, Vector2.right, 0.1f, groundLayer);

    }
    void Hop()
    {
        //var validX =new float[] { -3f, -2.5f, 2.5f,3f };
        currentHopTimer += Time.deltaTime * hopSpeed;
        if(currentHopTimer >= maxHopTimer && isGrounded)
        {
            currentHopTimer = 0;
            //var _x = validX[Random.Range(0, validX.Length)];
            var _y = Random.Range(4f, 4.5f);

            if (isLeftBordred)
            {
                xdir = Mathf.Abs(xdir);
            }
            if (isRightBordred)
            {
                xdir = Mathf.Abs(xdir) * -1;
            }
            var flipped = sonsonSR.flipX;
            if (xdir < 0f)
            {
                if (!flipped)
                {
                    sonsonSR.flipX = true;
                }

            }
            else
            {
                if (flipped)
                {
                    sonsonSR.flipX = false;
                }
            }
            theRB.AddForce(new Vector2(xdir, _y),ForceMode2D.Impulse);
        }
    }

}
