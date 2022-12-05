using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController_HV_Throw : MonoBehaviour
{
    public static GeneratorController_HV_Throw instance;

    Vector2 minBounds;
    Vector2 maxBounds;
    [SerializeField] float paddingLeft = .3f;
    [SerializeField] float paddingRight = .3f;


    public GameObject[] Obastacle;
    public GameObject[] banana;
    public GameObject[] cloud;
    [SerializeField] GameObject greenBanana;
    [SerializeField] GameObject redBanana;

    [SerializeField] float obstacleMinSpawnTimer = 1f;
    [SerializeField] float obstacleMaxSpawnTimer = 4f;
    [SerializeField] float obstacleSpawnTimer = 4f;
    float obstacleCurrentSpawnTimer;
    [SerializeField] float obstacleSpawnSpeed = 2;
    float obstacleFasterTimer;
    public float obstacleFasterTimer_treshold = 60f;
    public float obstacleFasterTimer_value = 0.1f;
    public float obstacleFasterTimer_min_value1 = 0.1f, obstacleFasterTimer_min_value2 = .8f;
    float prevObsY = 0f;

    [SerializeField] float coinMinSpawnTimer = 1f;
    [SerializeField] float coinMaxSpawnTimer = 4f;
    [SerializeField] float coinSpawnTimer = 3;
    float coinCurrentSpawnTimer;
    [SerializeField] float coinSpawnSpeed = 2;

    [SerializeField] float redBananaMinSpawnTimer = 10f;
    [SerializeField] float redBananaMaxSpawnTimer = 20f;
    [SerializeField] float redBananaSpawnTimer = 3;
    float redBananaCurrentSpawnTimer;
    [SerializeField] float redBananaSpawnSpeed = 1;

    [SerializeField] float greenBananaMinSpawnTimer = 10f;
    [SerializeField] float greenBananaMaxSpawnTimer = 15f;
    [SerializeField] float greenBananaSpawnTimer = 3;
    float greenBananaCurrentSpawnTimer;
    [SerializeField] float greenBananaSpawnSpeed = 1;

    [SerializeField] float cloudSpawnTimer = 3;
    float cloudCurrentSpawnTimer;
    [SerializeField] float cloudSpawnSpeed = 2;

    float generateMinX, generate_UpY_MinX, above_half_Y;

    [Header("explosion È¿°ú °´Ã¼")]
    public GameObject explosionEffector;

    [SerializeField]
    float additionalSpawnDelay = 0.4f;
    private void Awake()
    {
        MakeInstance();
        InitBounds();
        obstacleFasterTimer = Time.time + obstacleFasterTimer_treshold;
    }

    // Start is called before the first frame update
    void Start()
    {
        obstacleCurrentSpawnTimer = obstacleMaxSpawnTimer;
        generateMinX = Camera.main.ViewportToWorldPoint(new Vector2(0.6f, 0f)).x;
        generate_UpY_MinX = Camera.main.ViewportToWorldPoint(new Vector2(0.75f, 1f)).x;
        above_half_Y = Camera.main.ViewportToWorldPoint(new Vector2(1f, 0.6f)).y;
    }
    private void Update()
    {
        if (Time.time > obstacleFasterTimer)
        {
            obstacleMinSpawnTimer -= obstacleFasterTimer_value;
            if (obstacleMinSpawnTimer < obstacleFasterTimer_min_value1)
            {
                obstacleMinSpawnTimer = obstacleFasterTimer_min_value1;
            }
            obstacleMaxSpawnTimer -= obstacleFasterTimer_value;
            if (obstacleMaxSpawnTimer < obstacleFasterTimer_min_value2)
            {
                obstacleMaxSpawnTimer = obstacleFasterTimer_min_value2;
            }
            obstacleFasterTimer = Time.time + obstacleFasterTimer_treshold;
        }
    }

    void FixedUpdate()
    {
        GenerateObstacle();

        GenerateBanana();
        //GenerateGreenBanana();
        GenerateRedBanana();

        //TestGenerate();
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
    void GenerateObstacle()
    {
        if (!GameController_HV.instance.isPlay)
        {
            return;
        }
        obstacleCurrentSpawnTimer += Time.deltaTime * obstacleSpawnSpeed;
        if (obstacleCurrentSpawnTimer >= obstacleSpawnTimer)
        {
            obstacleSpawnTimer = Random.Range(obstacleMinSpawnTimer, obstacleMaxSpawnTimer);
            //obstacleSpawnSpeed += (Time.deltaTime * 1);
            obstacleCurrentSpawnTimer = 0;
            var randPos = transform.position;
            //var xPos = minBounds.x - 1f;
            var xPos = maxBounds.x + 1f;
            var randY = Random.Range(minBounds.y + paddingLeft, maxBounds.y - paddingRight);
            randPos.y = randY;
            randPos.x = xPos;

            if (prevObsY != 0f && Mathf.Round(prevObsY) == Mathf.Round(randPos.y))
            {
                randPos.y += 1.5f;
                if (randPos.y > maxBounds.y - paddingRight)
                {
                    randPos.y = minBounds.y + paddingLeft;
                }
            }

            var obj = GameObject.Instantiate(Obastacle[Random.Range(0, Obastacle.Length - 1)], randPos, Quaternion.identity);
            var objScale = obj.transform.localScale;
            //objScale *= Random.Range(0.5f, 1f);
            obj.transform.localScale = objScale;
            prevObsY = randPos.y;
            Destroy(obj, 5f);
        }

    }
    void TestGenerate()
    {
        coinCurrentSpawnTimer += Time.deltaTime * coinSpawnSpeed;

        if (coinCurrentSpawnTimer >= coinSpawnTimer)
        {
            coinSpawnTimer = Random.Range(coinMinSpawnTimer, coinMaxSpawnTimer);

            coinCurrentSpawnTimer = 0;
            var randPos = transform.position;
            //randPos.x = Camera.main.ViewportToWorldPoint(new Vector2(1f, .8f)).x;
            //randPos.y = Camera.main.ViewportToWorldPoint(new Vector2(1f, .8f)).y;
            randPos.x = Random.Range(generate_UpY_MinX, maxBounds.x + 2f);

            randPos.y = Random.Range(above_half_Y, maxBounds.y);
            if (randPos.x < maxBounds.x - 0.1f && randPos.y < maxBounds.y)
            {

                if (Random.Range(0, 2) == 0)
                {
                    randPos.y = maxBounds.y;
                }
                else
                {
                    randPos.x = maxBounds.x;
                }

            }


            var targetPos = Camera.main.ViewportToWorldPoint(new Vector2(0.2f, 0.2f));

            var obj = Instantiate(banana[Random.Range(0, banana.Length - 1)], randPos, Quaternion.identity);
            var objRB = obj.GetComponent<Rigidbody2D>();
            if (objRB)
            {

                var vel = CalculateProjectoryVelocty(origin: obj.transform.position,
                    target: targetPos
                    , time: 1f);
                //objRB.velocity = vel;
            }
            var objCurver = obj.GetComponent<Obj_Curve_Mover>();
            objCurver.enabled = true;
            objCurver.speed = Random.Range(12f, 15f);
            objCurver.gravity = Random.Range(0.4f, 0.6f);
            if (randPos.x >= maxBounds.x)
            {
                objCurver.gravity = 0.05f;
            }
            //objCurver.gravyAddAjuster = Random.Range(3f, 4f);
            Destroy(obj, 5f);
        }
    }
    void GenerateBanana()
    {
        if (!GameController_HV.instance.isPlay)
        {
            return;
        }

        coinCurrentSpawnTimer += Time.deltaTime * coinSpawnSpeed;

        if (coinCurrentSpawnTimer >= coinSpawnTimer)
        {
            coinSpawnTimer = Random.Range(coinMinSpawnTimer, coinMaxSpawnTimer);

            coinCurrentSpawnTimer = 0;

            var randPos = transform.position;
            randPos.y = minBounds.y;
            randPos.x = Random.Range(generateMinX, maxBounds.x + 2f);
            var downY_or_upY = Random.Range(0, 2);

            // downY parabola
            if (downY_or_upY <= 0)
            {
                if (randPos.x >= maxBounds.x)
                {
                    randPos.y = Random.Range(minBounds.y, Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)).y);
                }
            }
            // up y curve
            else
            {
                randPos.x = Random.Range(generate_UpY_MinX, maxBounds.x + 2f);
                randPos.y = Random.Range(above_half_Y, maxBounds.y);
                if (randPos.x < maxBounds.x - 0.1f && randPos.y < maxBounds.y)
                {

                    if (Random.Range(0, 4) == 0)
                    {
                        randPos.y = maxBounds.y;
                    }
                    else
                    {
                        randPos.x = maxBounds.x;
                    }

                }
            }


            var obj = Instantiate(banana[Random.Range(0, banana.Length - 1)], randPos, Quaternion.identity);
            if (downY_or_upY <= 0)
            {
                var objRB = obj.GetComponent<Rigidbody2D>();
                var vel = CalculateProjectoryVelocty(origin: obj.transform.position,
                    target: new Vector3(PlayerControl_HV.instance.transform.position.x
                    , Random.Range(minBounds.y + 1f, maxBounds.y - 1.6f), 0)
                    , time: Random.Range(1f, 2f));
                objRB.velocity = vel;
            }
            else
            {
                var objCurver = obj.GetComponent<Obj_Curve_Mover>();
                objCurver.enabled = true;
                objCurver.speed = Random.Range(12f, 15f);
                objCurver.gravity = Random.Range(0.4f, 0.6f);
                if (randPos.x >= maxBounds.x)
                {
                    objCurver.gravity = 0.05f;
                }
            }
            Destroy(obj, 5f);

            var multipleThrowChance = Random.Range(0, 2);
            if (multipleThrowChance >= 0)
            {
                var forceGreenBanana = Random.Range(0, 2);

                var thirdObj = Random.Range(0, 2);
                var fourthObj = Random.Range(0, 2);


                StartCoroutine(GenerateAdditionalItem(additionalSpawnDelay));

                if (thirdObj > 0)
                {
                    if (fourthObj <= 0 && forceGreenBanana > 0)
                    {
                        StartCoroutine(GenerateAdditionalItem(additionalSpawnDelay * 2, forceGreenBanana));
                    }
                    else
                    {
                        StartCoroutine(GenerateAdditionalItem(additionalSpawnDelay * 2));
                    }
                }
                if (thirdObj > 0 && fourthObj > 0)
                {
                    StartCoroutine(GenerateAdditionalItem(additionalSpawnDelay * 3, forceGreenBanana));
                }

            }

        }

    }

    void GenerateGreenBanana()
    {
        if (!GameController_HV.instance.isPlay)
        {
            return;
        }
        greenBananaCurrentSpawnTimer += Time.deltaTime * greenBananaSpawnSpeed;
        if (greenBananaCurrentSpawnTimer >= greenBananaSpawnTimer)
        {
            greenBananaSpawnTimer = Random.Range(greenBananaMinSpawnTimer, greenBananaMaxSpawnTimer);

            greenBananaCurrentSpawnTimer = 0;

            var randPos = transform.position;
            randPos.y = minBounds.y;
            randPos.x = Random.Range(generateMinX, maxBounds.x + 2f);
            var downY_or_upY = Random.Range(0, 2);

            // downY parabola
            if (downY_or_upY <= 0)
            {
                if (randPos.x >= maxBounds.x)
                {
                    randPos.y = Random.Range(minBounds.y, Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)).y);
                }
            }
            // up y curve
            else
            {
                randPos.x = Random.Range(generate_UpY_MinX, maxBounds.x + 2f);
                randPos.y = Random.Range(above_half_Y, maxBounds.y);
                if (randPos.x < maxBounds.x - 0.1f && randPos.y < maxBounds.y)
                {

                    if (Random.Range(0, 4) == 0)
                    {
                        randPos.y = maxBounds.y;
                    }
                    else
                    {
                        randPos.x = maxBounds.x;
                    }

                }
            }


            var obj = Instantiate(greenBanana, randPos, Quaternion.identity);
            if (downY_or_upY <= 0)
            {
                var objRB = obj.GetComponent<Rigidbody2D>();
                var vel = CalculateProjectoryVelocty(origin: obj.transform.position,
                    target: new Vector3(PlayerControl_HV.instance.transform.position.x
                    , Random.Range(minBounds.y + 1f, maxBounds.y - 1.6f), 0)
                    , time: Random.Range(1f, 2f));
                objRB.velocity = vel;
            }
            else
            {
                var objCurver = obj.GetComponent<Obj_Curve_Mover>();
                objCurver.enabled = true;
                objCurver.speed = Random.Range(12f, 15f);
                objCurver.gravity = Random.Range(0.4f, 0.6f);
                if (randPos.x >= maxBounds.x)
                {
                    objCurver.gravity = 0.05f;
                }
            }
            Destroy(obj, 5f);
            var multipleThrowChance = Random.Range(0, 4);

            /*
            if (multipleThrowChance >= 2)
            {
                StartCoroutine(GenerateAdditionalItem());
            }
            */

        }

    }
    void GenerateRedBanana()
    {
        if (!GameController_HV.instance.isPlay)
        {
            return;
        }
        if (PlayerControl_HV.instance.currentRedBananaTimer < PlayerControl_HV.instance.maxRedbananaTimer)
        {
            return;
        }
        redBananaCurrentSpawnTimer += Time.deltaTime * redBananaSpawnSpeed;
        if (redBananaCurrentSpawnTimer >= redBananaSpawnTimer)
        {
            redBananaSpawnTimer = Random.Range(redBananaMinSpawnTimer, redBananaMaxSpawnTimer);

            redBananaCurrentSpawnTimer = 0;

            var randPos = transform.position;
            randPos.y = minBounds.y;
            randPos.x = Random.Range(generateMinX, maxBounds.x + 2f);
            var downY_or_upY = Random.Range(0, 2);

            // downY parabola
            if (downY_or_upY <= 0)
            {
                if (randPos.x >= maxBounds.x)
                {
                    randPos.y = Random.Range(minBounds.y, Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)).y);
                }
            }
            // up y curve
            else
            {
                randPos.x = Random.Range(generate_UpY_MinX, maxBounds.x + 2f);
                randPos.y = Random.Range(above_half_Y, maxBounds.y);
                if (randPos.x < maxBounds.x - 0.1f && randPos.y < maxBounds.y)
                {

                    if (Random.Range(0, 4) == 0)
                    {
                        randPos.y = maxBounds.y;
                    }
                    else
                    {
                        randPos.x = maxBounds.x;
                    }

                }
            }


            var obj = Instantiate(redBanana, randPos, Quaternion.identity);
            if (downY_or_upY <= 0)
            {
                var objRB = obj.GetComponent<Rigidbody2D>();
                var vel = CalculateProjectoryVelocty(origin: obj.transform.position,
                    target: new Vector3(PlayerControl_HV.instance.transform.position.x
                    , Random.Range(minBounds.y + 1f, maxBounds.y - 1.6f), 0)
                    , time: Random.Range(1f, 2f));
                objRB.velocity = vel;
            }
            else
            {
                var objCurver = obj.GetComponent<Obj_Curve_Mover>();
                objCurver.enabled = true;
                objCurver.speed = Random.Range(12f, 15f);
                objCurver.gravity = Random.Range(0.4f, 0.6f);
                if (randPos.x >= maxBounds.x)
                {
                    objCurver.gravity = 0.05f;
                }
            }
            Destroy(obj, 5f);

        }

    }
    IEnumerator GenerateAdditionalItem(float waitTime = .5f, int forceGreenBanana = 0)
    {
        yield return new WaitForSeconds(waitTime);
        /*
        randPos.x += 2f;
        randPos.x = Random.Range(randPos.x, maxBounds.x + 2f);
        randPos.y = minBounds.y;

        if (randPos.x >= maxBounds.x)
        {
            randPos.y = Random.Range(minBounds.y, Camera.main.ViewportToWorldPoint(new Vector2(0, 0.45f)).y);
        }
        var itemChance = Random.Range(0, 5);
        if (itemChance >= 4)
        {
            obj2 = GameObject.Instantiate(greenBanana, randPos, Quaternion.identity);
        }
        else
        {
            obj2 = GameObject.Instantiate(banana[Random.Range(0, banana.Length - 1)], randPos, Quaternion.identity);
        }
        var objRB2 = obj2.GetComponent<Rigidbody2D>();
        if (objRB2)
        {
            var vel = CalculateProjectoryVelocty(origin: obj2.transform.position,
                target: new Vector3(PlayerControl_HV.instance.transform.position.x
                , Random.Range(minBounds.y + 1f, maxBounds.y - 1.6f), 0)
                , time: Random.Range(1f, 2f));
            objRB2.velocity = vel;
        }
        */

        var randPos = transform.position;
        randPos.y = minBounds.y;
        randPos.x = Random.Range(generateMinX, maxBounds.x + 2f);
        var downY_or_upY = Random.Range(0, 2);

        // downY parabola
        if (downY_or_upY <= 0)
        {
            if (randPos.x >= maxBounds.x)
            {
                randPos.y = Random.Range(minBounds.y, Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)).y);
            }
        }
        // up y curve
        else
        {
            randPos.x = Random.Range(generate_UpY_MinX, maxBounds.x + 2f);
            randPos.y = Random.Range(above_half_Y, maxBounds.y);
            if (randPos.x < maxBounds.x - 0.1f && randPos.y < maxBounds.y)
            {

                if (Random.Range(0, 4) == 0)
                {
                    randPos.y = maxBounds.y;
                }
                else
                {
                    randPos.x = maxBounds.x;
                }

            }
        }

        /*
        // ¿¹Àü¿¡ ¸¸µç º¹¼þ¾Æ ³ª¿Ã È®·ü
        var itemChance = Random.Range(0, 5);
        if (itemChance >= 4)
        {
            obj2 = GameObject.Instantiate(greenBanana, randPos, Quaternion.identity);
        }
        else
        {
            obj2 = GameObject.Instantiate(banana[Random.Range(0, banana.Length - 1)], randPos, Quaternion.identity);
        }
        */

        var obj2 = GameObject.Instantiate(forceGreenBanana > 0 ? greenBanana : banana[0], randPos, Quaternion.identity);

        if (downY_or_upY <= 0)
        {
            var objRB = obj2.GetComponent<Rigidbody2D>();
            var vel = CalculateProjectoryVelocty(origin: obj2.transform.position,
                target: new Vector3(PlayerControl_HV.instance.transform.position.x
                , Random.Range(minBounds.y + 1f, maxBounds.y - 1.6f), 0)
                , time: Random.Range(1f, 2f));
            objRB.velocity = vel;
        }
        else
        {
            var objCurver = obj2.GetComponent<Obj_Curve_Mover>();
            objCurver.enabled = true;
            objCurver.speed = Random.Range(12f, 15f);
            objCurver.gravity = Random.Range(0.4f, 0.6f);
            if (randPos.x >= maxBounds.x)
            {
                objCurver.gravity = 0.05f;
            }
        }
        Destroy(obj2, 5f);

    }
    Vector3 CalculateProjectoryVelocty(Vector3 target, Vector3 origin, float time)
    {
        var obstacleAddSpeed = GameController_HV.instance.obstacleAddSpeed * 0.6f;
        if (obstacleAddSpeed < 1)
        {
            obstacleAddSpeed = 1f;
        }
        time /= obstacleAddSpeed;

        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz / time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

}
