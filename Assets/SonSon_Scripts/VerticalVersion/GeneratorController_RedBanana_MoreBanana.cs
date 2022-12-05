using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController_RedBanana_MoreBanana : MonoBehaviour
{
    public static GeneratorController_RedBanana_MoreBanana instance;

    Vector2 minBounds;
    Vector2 maxBounds;
    [SerializeField] float paddingLeft = .3f;
    [SerializeField] float paddingRight = .3f;


    public GameObject[] Obastacle;
    public GameObject[] banana;
    public GameObject[] cloud;
    [SerializeField] GameObject greenBanana;
    [SerializeField] GameObject redBanana;

    [SerializeField] float obstacleSpawnTimer = 3;
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

    [Header("Bird Spawn Points")]
    [SerializeField] GameObject bird;
    [SerializeField] Transform leftBirdSpawnPoint;
    [SerializeField] Transform rightBirdSpawnPoint;
    [SerializeField] float birdSpawnTimer = 3;
    float birdCurrentSpawnTimer;
    [SerializeField] float birdSpawnSpeed = 2;
    


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
        GenerateObstacle();
        GenerateRedBanana();
        GenerateBanana();
        GenerateGreenBanana();
        GenerateCloud();
        GenerateBirds();
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
        if (!GameController.instance.isPlay)
        {
            return;
        }
        obstacleCurrentSpawnTimer += Time.deltaTime * obstacleSpawnSpeed;
        if (obstacleCurrentSpawnTimer >= obstacleSpawnTimer)
        {
            obstacleSpawnSpeed += (Time.deltaTime * 1);
            obstacleCurrentSpawnTimer = 0;
            obstacleSpawnTimer = Random.Range(2f, 4f);
            var randPos = maxBounds;
            var randX = Random.Range(minBounds.x + paddingLeft, maxBounds.x + paddingRight);
            randPos.x = randX;
            if (PlayerControll_KeepDirection.instance.currentRedBananaTimer<PlayerControll_KeepDirection.instance.maxRedbananaTimer)
            {
                GameObject.Instantiate(banana[Random.Range(0, banana.Length - 1)], randPos, Quaternion.identity);
            }
            else { 
            GameObject.Instantiate(Obastacle[Random.Range(0, Obastacle.Length - 1)], randPos, Quaternion.identity);
            }
        }

    }
    
    void GenerateBanana()
    {
        if (!GameController.instance.isPlay)
        {
            return;
        }
        if (PlayerControll_KeepDirection.instance.currentRedBananaTimer < PlayerControll_KeepDirection.instance.maxRedbananaTimer)
        {
            coinCurrentSpawnTimer += Time.deltaTime * coinSpawnSpeed*8;
        }
        else
        {
            coinCurrentSpawnTimer += Time.deltaTime * coinSpawnSpeed;
        }
        if (coinCurrentSpawnTimer >= coinSpawnTimer)
        {
            var randNum = 1f;
            if (PlayerControll_KeepDirection.instance.currentRedBananaTimer < PlayerControll_KeepDirection.instance.maxRedbananaTimer)
            {
                randNum= Random.Range(6, 11);
            }
            else
            {
                randNum = Random.Range(0, 11);
            }
                
            coinSpawnSpeed += Time.deltaTime;
            coinCurrentSpawnTimer = 0;
            //coinSpawnTimer = Random.Range(3f, 8f);
            var randPos = maxBounds;
            var randX = Random.Range(minBounds.x + paddingLeft, maxBounds.x + paddingRight);
            randPos.x = randX;
            if (randNum >= 6) { 
                GameObject.Instantiate(banana[Random.Range(0, banana.Length - 1)], randPos, Quaternion.identity);
            }
        }

    }
    void GenerateGreenBanana()
    {
        if (!GameController.instance.isPlay)
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
            var randPos = maxBounds;
            var randX = Random.Range(minBounds.x + paddingLeft, maxBounds.x + paddingRight);
            randPos.x = randX;
            if (randNum >= 8)
            {
                GameObject.Instantiate(greenBanana, randPos, Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
            }

        }

    }
    void GenerateRedBanana()
    {
        if (!GameController.instance.isPlay)
        {
            return;
        }
        redBananaCurrentSpawnTimer += Time.deltaTime * redBananaSpawnSpeed;
        if (redBananaCurrentSpawnTimer >= redBananaSpawnTimer)
        {
            var randNum = Random.Range(0, 11);

            redBananaCurrentSpawnTimer = 0;
            //redBananaSpawnTimer = Random.Range(3f, 8f);
            var randPos = maxBounds;
            var randX = Random.Range(minBounds.x + paddingLeft, maxBounds.x + paddingRight);
            randPos.x = randX;
            if (randNum >= 8)
            {
                GameObject.Instantiate(redBanana, randPos, Quaternion.Euler(0,0,Random.Range(0f,359f)));
            }

        }

    }
    void GenerateCloud()
    {
        if (!GameController.instance.isPlay)
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
                var obj=GameObject.Instantiate(cloud[Random.Range(0, cloud.Length - 1)], randPos, Quaternion.identity);
            }

        }
    }
    void GenerateBirds()
    {
        if (!GameController.instance.isPlay)
        {
            return;
        }
        birdCurrentSpawnTimer += Time.deltaTime * birdSpawnSpeed;
        if (birdCurrentSpawnTimer >= birdSpawnTimer)
        {
            var randNum = Random.Range(0, 11);

            birdCurrentSpawnTimer = 0;

            var randNum2= Random.Range(0, 2);
            var spawnTransform = new Vector3();
            if (randNum2 > 0)
            {
                // left bird spawnpoint
                spawnTransform = leftBirdSpawnPoint.position;
            }
            else
            {
                // right bird spawnpoint
                spawnTransform = rightBirdSpawnPoint.position;
            }
            var tempY = GameController.instance.maxBounds.y*Random.Range(0.1f,0.9f);
            spawnTransform.y += tempY;
            
            if (randNum >= 3)
            {
                var _bird=GameObject.Instantiate(bird, spawnTransform, Quaternion.identity);
                if (randNum2 > 0)
                {
                    // left bird spawnpoint
                }
                else
                {
                    // right bird spawnpoint
                    var birdComponent=_bird.GetComponent<Bird>();
                    birdComponent.xdir *= -1;
                    var tempXScale = birdComponent.transform.localScale;
                    tempXScale.x *= -1;
                    birdComponent.transform.localScale = tempXScale;
                }
            }

        }
    }

}
