

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
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



    void Start()
    {
        InitBounds();
    }

    void FixedUpdate()
    {
        //TryToGenerate();
        //GenerateObstacle();
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
            obstacleSpawnSpeed += (Time.deltaTime*2);
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
        coinCurrentSpawnTimer += Time.deltaTime * coinSpawnSpeed;
        if (coinCurrentSpawnTimer >= coinSpawnTimer)
        {
            var randNum = Random.Range(0, 11);
            
            coinSpawnSpeed += Time.deltaTime;
            coinCurrentSpawnTimer = 0;
            coinSpawnTimer = Random.Range(3f, 8f);
            var randPos = maxBounds;
            var randX = Random.Range(minBounds.x + paddingLeft, maxBounds.x + paddingRight);
            randPos.x = randX;
            if (randNum >= 8)
            {
                GameObject.Instantiate(greenBanana, randPos, Quaternion.identity);
            }
            
        }

    }

}

