using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController_HV : MonoBehaviour
{
    public static GeneratorController_HV instance;

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

    [SerializeField] float coinSpawnTimer = 3;
    float coinCurrentSpawnTimer;
    [SerializeField] float coinSpawnSpeed = 2;

    [SerializeField] float redBananaSpawnTimer = 3;
    float redBananaCurrentSpawnTimer;
    [SerializeField] float redBananaSpawnSpeed = 2;

    [SerializeField] float greenBananaSpawnTimer = 3;
    float greenBananaCurrentSpawnTimer;
    [SerializeField] float greenBananaSpawnSpeed = 2;

    [SerializeField] float cloudSpawnTimer = 3;
    float cloudCurrentSpawnTimer;
    [SerializeField] float cloudSpawnSpeed = 2;




    private void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        InitBounds();
    }

    void FixedUpdate()
    {
        //GenerateObstacle();
        //GenerateRedBanana();
        GenerateBanana();
        //GenerateGreenBanana();
        //GenerateCloud();
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
            obstacleSpawnSpeed += (Time.deltaTime * 1);
            obstacleCurrentSpawnTimer = 0;
            var randPos = transform.position;
            var randY = Random.Range(minBounds.y + paddingLeft, maxBounds.y + paddingRight);
            randPos.y = randY;

            var obj = GameObject.Instantiate(Obastacle[Random.Range(0, Obastacle.Length - 1)], randPos, Quaternion.identity);
            var objScale = obj.transform.localScale;
            objScale *= Random.Range(0.5f, 1f);
            obj.transform.localScale = objScale;
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
            var randNum = 1f;

            randNum = Random.Range(0, 11);

            coinSpawnSpeed += Time.deltaTime;
            coinCurrentSpawnTimer = 0;
            //coinSpawnTimer = Random.Range(3f, 8f);
            var randPos = transform.position;
            var randY = Random.Range(minBounds.y + paddingLeft, maxBounds.y + paddingRight);
            randPos.y = randY;
            if (randNum >= 6)
            {
                GameObject.Instantiate(banana[Random.Range(0, banana.Length - 1)], randPos, Quaternion.identity);
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
            var randNum = Random.Range(0, 11);

            greenBananaSpawnSpeed += Time.deltaTime;
            greenBananaCurrentSpawnTimer = 0;
            //greenBananaSpawnTimer = Random.Range(3f, 8f);
            var randPos = transform.position;
            var randY = Random.Range(minBounds.y + paddingLeft, maxBounds.y + paddingRight);
            randPos.y = randY;
            if (randNum >= 9)
            {
                GameObject.Instantiate(greenBanana, randPos, Quaternion.identity);
            }

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
            var randNum = Random.Range(0, 11);

            redBananaCurrentSpawnTimer = 0;
            //redBananaSpawnTimer = Random.Range(3f, 8f);
            var randPos = transform.position;
            var randY = Random.Range(minBounds.y + paddingLeft, maxBounds.y + paddingRight);
            randPos.y = randY;
            if (randNum >= 9)
            {
                GameObject.Instantiate(redBanana, randPos, Quaternion.identity);
            }

        }

    }
    void GenerateCloud()
    {
        if (!GameController_HV.instance.isPlay)
        {
            return;
        }
        cloudCurrentSpawnTimer += Time.deltaTime * cloudSpawnSpeed;
        if (cloudCurrentSpawnTimer >= cloudSpawnTimer)
        {
            var randNum = Random.Range(0, 11);

            cloudCurrentSpawnTimer = 0;
            //redBananaSpawnTimer = Random.Range(3f, 8f);
            var randPos = maxBounds;
            var randX = Random.Range(minBounds.x, maxBounds.x);
            randPos.x = randX;
            if (randNum >= 5)
            {
                var obj = GameObject.Instantiate(cloud[Random.Range(0, cloud.Length - 1)], randPos, Quaternion.identity);
            }

        }
    }


}
