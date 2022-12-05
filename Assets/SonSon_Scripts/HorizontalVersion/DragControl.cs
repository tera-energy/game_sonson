using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVE_DIR_STATUS
{
    none
    , up
        , down
}
public class DragControl : MonoBehaviour
{
    [SerializeField]
    GameObject movePosParent;

    Rigidbody2D theRB;
    Vector3 targetPos;
    Vector2 startTouchPosition;
    Vector2 currentPosition;
    MOVE_DIR_STATUS move_dir_status = MOVE_DIR_STATUS.none;
    public float moveSpeed = 1f;
    float dir = 0f;

    float deltaX;
    float deltaY;

    private void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //targetPos = transform.position;
        movePosParent.transform.position = transform.position;
        targetPos = movePosParent.transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        Drag_Move_relative_movetoward();

    }
    private void LateUpdate()
    {
        if (transform.position.y > GameController_HV.instance.maxBounds.y - 0.3f)
        {
            transform.position = new Vector3(transform.position.x, GameController_HV.instance.maxBounds.y - 0.3f, transform.position.z);
        }
        if (transform.position.y < GameController_HV.instance.minBounds.y + 0.3f)
        {
            transform.position = new Vector3(transform.position.x, GameController_HV.instance.minBounds.y + 0.3f, transform.position.z);
        }
    }

    void Drag_Move()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    targetPos = new Vector3(transform.position.x, touchPos.y, transform.position.z);
                    break;
                case TouchPhase.Moved:
                    targetPos = new Vector3(transform.position.x, touchPos.y, transform.position.z);
                    break;
                case TouchPhase.Ended:
                    //targetPos = new Vector3(transform.position.x, touchPos.y, transform.position.z);
                    targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    break;
            }
        }

    }
    void Drag_Move_relative_movetoward()
    {
        
        if (!PlayerControl_HV.instance.bAlive || !GameController_HV.instance.isPlay)
        {
            return;
        }
        if (Input.touchCount > 0)
        {

            var touch = Input.GetTouch(0);
            var touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = Input.GetTouch(0).position;
                    movePosParent.transform.position = new Vector3(movePosParent.transform.position.x, touchPos.y, movePosParent.transform.position.z);
                    targetPos = new Vector3(transform.position.x, touchPos.y, transform.position.z);
                    transform.SetParent(movePosParent.transform);
                    break;
                case TouchPhase.Moved:
                    currentPosition = Input.GetTouch(0).position;
                    var distance = currentPosition - startTouchPosition;
                    if (currentPosition.y > startTouchPosition.y)
                    {
                        move_dir_status = MOVE_DIR_STATUS.up;
                    }
                    else if (currentPosition.y < startTouchPosition.y)
                    {
                        move_dir_status = MOVE_DIR_STATUS.down;
                    }
                    else
                    {
                        move_dir_status = MOVE_DIR_STATUS.none;
                    }
                   
                    targetPos = new Vector3(movePosParent.transform.position.x, touchPos.y, movePosParent.transform.position.z);
                    break;
                case TouchPhase.Ended:
                    transform.SetParent(null);
                    move_dir_status = MOVE_DIR_STATUS.none;
                    targetPos = new Vector3(movePosParent.transform.position.x, movePosParent.transform.position.y, movePosParent.transform.position.z);
                    break;
            }
        }
#if !UNITY_EDITOR
        movePosParent.transform.position = Vector3.MoveTowards(movePosParent.transform.position, targetPos, moveSpeed * Time.deltaTime);
#endif

#if UNITY_EDITOR
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.W)){
            dir = Vector2.up;
        }
        if (Input.GetKey(KeyCode.S)){
            dir = Vector2.down;
        }
        transform.position += (Vector3)dir * 10 * Time.deltaTime;
#endif
    }

}
