using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController_Redbanana_Invincible : MonoBehaviour
{
    Vector2 minBounds;
    Vector2 maxBounds;
    [SerializeField] float paddingLeft = .3f;
    [SerializeField] float paddingRight = .3f;


    public GameObject[] Obastacle;
    public GameObject[] banana;
    [SerializeField] GameObject greenBanana;
    [SerializeField] GameObject redBanana;

    [SerializeField] float obstacleSpawnTimer = 3;
    float obstacleCurrentSpawnTimer;
    [SerializeField] float obstacleSpawnSpeed = 2;

    [SerializeField] float coinSpawnTimer = 3;
    float coinCurrentSpawnTimer;
    [SerializeField] float coinSpawnSpeed = 2;

    [SerializeField] float redBananaSpawnTimer = 4f;
    float redBananaCurrentSpawnTimer;
    [SerializeField] float redBananaSpawnSpeed = 1.5f;

    [SerializeField] float greenBananaSpawnTimer = 3;
    float greenBananaCurrentSpawnTimer;
    [SerializeField] float greenBananaSpawnSpeed = 2;


    void Start()
    {
        InitBounds();
    }

    void FixedUpdate()
    {
        //TryToGenerate();
        GenerateObstacle();
        GenerateRedBanana();
        GenerateBanana();
        GenerateGreenBanana();
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

            GameObject.Instantiate(Obastacle[Random.Range(0, Obastacle.Length - 1)], randPos, Quaternion.identity);
        }

    }

    void GenerateBanana()
    {
        if (!GameController.instance.isPlay)
        {
            return;
        }
        coinCurrentSpawnTimer += Time.deltaTime * coinSpawnSpeed;
        if (coinCurrentSpawnTimer >= coinSpawnTimer)
        {
            //coinSpawnSpeed += Time.deltaTime;
            coinCurrentSpawnTimer = 0;
            coinSpawnTimer = Random.Range(3f, 8f);
            var randPos = maxBounds;
            var randX = Random.Range(minBounds.x + paddingLeft, maxBounds.x + paddingRight);
            randPos.x = randX;

            GameObject.Instantiate(banana[Random.Range(0, banana.Length - 1)], randPos, Quaternion.identity);
        }

    }
    void GenerateGreenBanana()
    {
        if (!GameController.instance.isPlay)
        {
            return;
        }
        greenBananaCurrentSpawnTimer += Time.deltaTime * greenBananaSpawnSpeed;
        if (coinCurrentSpawnTimer >= coinSpawnTimer)
        {
            var randNum = Random.Range(0, 11);

            greenBananaSpawnSpeed += Time.deltaTime;
            greenBananaCurrentSpawnTimer = 0;
            greenBananaSpawnTimer = Random.Range(3f, 8f);
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
                GameObject.Instantiate(redBanana, randPos, Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
            }

        }

    }

}
