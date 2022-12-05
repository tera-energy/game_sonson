using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Control_Mode
{
    Btn_Control
        , Swipe
}
public class PlayerControl_HV : MonoBehaviour
{
    public static PlayerControl_HV instance;
    private Rigidbody2D theRB;
    //public float coll_y_size;

    [SerializeField] Image healthbar;

    [Header("InitBound_to_camera")]
    Vector2 minBounds;
    Vector2 maxBounds;
    [SerializeField] float paddingLeft = .3f;
    [SerializeField] float paddingRight = .3f;

    public float speed = 5f;

    public bool bAlive = true;

    bool isUpDown;
    bool isDownDown;
    public float dir = 0;

    [Header("HP")]
    public float currentHp = 1;
    public float maxHp = 30;
    public float hpForceMinusValue = 0.1f;
    public float hpForceMinusPenaltyValue = 1f;
    public float hpForceMinusTreshold = 1f;
    float hpForceMinusTimer;

    [SerializeField] Image _imgMode;

	[SerializeField] Sprite _spChallenege;
    [SerializeField] Sprite _spTrain;

    [Header("red banana status")]
    public float currentRedBananaTimer = 0f;
    public float maxRedbananaTimer = 3f;
    [SerializeField] float redbananaTimerSpeed = 1f;

    public Control_Mode ctrlMode = Control_Mode.Swipe;

    private void Awake()
    {
        MakeInstance();
        theRB = GetComponent<Rigidbody2D>();
        //coll_y_size = GetComponent<BoxCollider2D>().size.y;
        InitBounds();
        transform.position = new Vector3(minBounds.x+3.5f, transform.position.y, transform.position.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager._type == TT.enumGameType.Train)
            _imgMode.sprite = _spTrain;
        else if (GameManager._type == TT.enumGameType.Challenge)
            _imgMode.sprite = _spChallenege;
        _imgMode.SetNativeSize();
        currentRedBananaTimer = maxRedbananaTimer;
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            ArrowBtnDown(true);
        if (Input.GetKeyDown(KeyCode.S))
            ArrowBtnDown(false);
        if (Input.GetKeyUp(KeyCode.W))
            ArrowBtnUp(true);
        if (Input.GetKeyUp(KeyCode.S))
            ArrowBtnUp(false);
        RedBananaTimer();
        ForceMinusHp();

    }

    private void FixedUpdate()
    {
/*        if (bAlive && GameController_HV.instance.isPlay && ctrlMode==Control_Mode.Btn_Control)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, speed * dir);
        }*/

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


    public void ArrowBtnDown(bool isUp)
    {
        TrAudio_UI.xInstance.zPlaySFX(clip: GameController_HV.instance._moveClip, delay: 0f);
        if (isUp)
        {
            isUpDown = true;
            GameObject.Find("UpArrowImage")?.GetComponent<Animator>()?.SetTrigger("IsClickDown");

            dir = 1;
        }
        else
        {
            isDownDown = true;
            GameObject.Find("DownArrowImage")?.GetComponent<Animator>()?.SetTrigger("IsClickDown");

            dir = -1;
        }

    }

    public void ArrowBtnUp(bool isUp)
    {
        if (isUp)
        {
            isUpDown = false;
            if (isDownDown)
            {
                dir = -1;
                return;
            }
        }
        else
        {
            isDownDown = false;
            if (isUpDown)
            {
                dir = 1;
                return;
            }
        }
        dir = 0;
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
    void ForceMinusHp()
    {
        if (!GameController_HV.instance.isPlay || !PlayerControl_HV.instance.bAlive)
        {
            return;
        }

            
        if (PlayerControl_HV.instance.currentRedBananaTimer >= PlayerControl_HV.instance.maxRedbananaTimer)
        {
            currentHp -= hpForceMinusValue * hpForceMinusPenaltyValue * Time.deltaTime;
            healthbar.fillAmount = currentHp / maxHp;
        }

        if (currentHp <= 0)
        {
            currentHp = 0;
            PlayerCollisionDetect_HV.instance.Kill();
        }
        
    }


}
