using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RedBanaMode
{
    more_banana
        , invincible
}
public class PlayerControll_KeepDirection : MonoBehaviour
{
    public static PlayerControll_KeepDirection instance;
    private Rigidbody2D theRB;


    [Header("InitBound_to_camera")]
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
    public float dir = 0;

    public bool isGrounded;

    [Header("HP")]
    public int currentHp = 1;
    public int maxHp = 2;

    [Header("world space canvas")]
    [SerializeField] Transform playerWS_CanvasTransform;
    [SerializeField] Image invincibleUI_Bar;

    [Header("red banana status")]
    public float currentRedBananaTimer = 0f;
    public float maxRedbananaTimer = 3f;
    [SerializeField] float redbananaTimerSpeed = 1f;


    [Header("red banana mode")]
    public RedBanaMode redBannaMode = RedBanaMode.more_banana;

    private void Awake()
    {
        MakeInstance();
        theRB = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitBounds();
        currentRedBananaTimer = maxRedbananaTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            dir = -1;
        if (Input.GetKeyDown(KeyCode.D))
            dir = 1;
        MovePlayer();
        RedBananaTimer();


    }

    private void FixedUpdate()
    {
        theRB.velocity = new Vector2(speed * dir, theRB.velocity.y);
        keepPlayerHealthbar_Left_Filled();
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void MovePlayer()
    {
        if (!bAlive)
        {
            return;
        }

        Mathf.Clamp(dir, -1f, 1f);
        var tempScale = transform.localScale;
        if (dir > 0)
        {
            if (tempScale.x > 0)
            {
                tempScale.x = -tempScale.x;
            }
        }
        else if (dir < 0)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        if(transform.localScale!= tempScale)
        {
            TrAudio_UI.xInstance.zzPlaySFX(GameController.instance.switchDirectionClip);
        }
        transform.localScale = tempScale;

    }


    public void MovePlayerMobile(int horizontal_axis_raw)
    {
        dir = horizontal_axis_raw;
    }
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

    }
    void keepPlayerHealthbar_Left_Filled()
    {
        var tempS = playerWS_CanvasTransform.localScale;
        if (dir <= -1)
        {
            tempS.x = Mathf.Abs(tempS.x);
        }
        if (dir >= 1)
        {
            tempS.x = Mathf.Abs(tempS.x) * -1;
        }
        playerWS_CanvasTransform.localScale = tempS;
    }
    void RedBananaTimer()
    {
        if (currentRedBananaTimer >= maxRedbananaTimer)
        {
            return;
        }
        currentRedBananaTimer += Time.deltaTime * redbananaTimerSpeed;
        currentRedBananaTimer = Mathf.Clamp((currentRedBananaTimer), 0f, maxRedbananaTimer);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == MySonSonTags.Tags.LeftBounder)
        {
            dir = 1;
            TrAudio_UI.xInstance.zzPlaySFX(GameController.instance.switchDirectionClip);
        }
        if (other.gameObject.tag == MySonSonTags.Tags.RightBounder)
        {
            dir = -1;
            TrAudio_UI.xInstance.zzPlaySFX(GameController.instance.switchDirectionClip);
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == MySonSonTags.Tags.LeftBounder)
        {
            dir = 1;
        }
        if (other.gameObject.tag == MySonSonTags.Tags.RightBounder)
        {
            dir = -1;
        }
    }

}
