using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRawImageScroller_HV : MonoBehaviour
{
    public static CanvasRawImageScroller_HV instance;
    [SerializeField] RawImage _img;
    public float _x = 0f, _y = 0f;
    public float maxScrollSpeed = 0.5f;

    private void Awake()
    {
        MakeInstance();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ScrollBG();
    }
    private void FixedUpdate()
    {
        ScrollSpeedChanger();
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
    void ScrollBG()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
    }
    void ScrollSpeedChanger()
    {
        if (!GameController_HV.instance.isPlay)
        {
            return;
        }
        if (_x > maxScrollSpeed)
        {
            _x += Time.deltaTime;
        }
        else if (_x < maxScrollSpeed)
        {
            _x -= Time.deltaTime;
        }

    }
}
