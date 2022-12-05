using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos_And_Vel
{
    public Vector3 randPos;
    public int rand_left_or_right;
}
public class Main_Menu_Generator : MonoBehaviour
{
    Vector2 minBounds;
    Vector2 maxBounds;
    float mlX, mrX;
    [SerializeField] float paddingLeft = .3f;
    [SerializeField] float paddingRight = .3f;


    public GameObject[] banana;
    [SerializeField] GameObject greenBanana;
    [SerializeField] GameObject redBanana;



    [SerializeField] float coinMinSpawnTimer = 1f;
    [SerializeField] float coinMaxSpawnTimer = 4f;
    [SerializeField] float coinSpawnTimer = 1;
    float coinCurrentSpawnTimer;
    [SerializeField] float coinSpawnSpeed = 1;

    [SerializeField] float redBananaMinSpawnTimer = 1f;
    [SerializeField] float redBananaMaxSpawnTimer = 4f;
    [SerializeField] float redBananaSpawnTimer = 2;
    float redBananaCurrentSpawnTimer;
    [SerializeField] float redBananaSpawnSpeed = 1;

    [SerializeField] float greenBananaMinSpawnTimer = 1f;
    [SerializeField] float greenBananaMaxSpawnTimer = 4f;
    [SerializeField] float greenBananaSpawnTimer = 3;
    float greenBananaCurrentSpawnTimer;
    [SerializeField] float greenBananaSpawnSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        mlX=Camera.main.ViewportToWorldPoint(new Vector2(0.3f, 0f)).x;
        mrX=Camera.main.ViewportToWorldPoint(new Vector2(0.7f, 0f)).x;
        InitBounds();
    }

    void FixedUpdate()
    {
        //GenerateRedBanana();
        GenerateBanana();
        GenerateGreenBanana();
    }
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    void GenerateBanana()
    {
        coinCurrentSpawnTimer += Time.deltaTime * coinSpawnSpeed;

        if (coinCurrentSpawnTimer >= coinSpawnTimer)
        {
            coinSpawnTimer = Random.Range(coinMinSpawnTimer, coinMaxSpawnTimer);

            coinCurrentSpawnTimer = 0;

            var randPos_and_vel=GenerateCommonCode(banana[Random.Range(0,banana.Length)]);
            var multipleThrowChance = Random.Range(0, 4);
            if (multipleThrowChance >= 2)
            {
                //StartCoroutine(GenerateAdditionalItem(randPos: randPos_and_vel.randPos, rand_left_or_right: randPos_and_vel.rand_left_or_right));
            }
        }

    }
    void GenerateGreenBanana()
    {
        greenBananaCurrentSpawnTimer += Time.deltaTime * greenBananaSpawnSpeed;
        if (greenBananaCurrentSpawnTimer >= greenBananaSpawnTimer)
        {
            greenBananaSpawnTimer = Random.Range(greenBananaMinSpawnTimer, greenBananaMaxSpawnTimer);

            greenBananaCurrentSpawnTimer = 0;

            var randPos_and_vel = GenerateCommonCode(banana[Random.Range(0, banana.Length)]);
            var multipleThrowChance = Random.Range(0, 4);

            if (multipleThrowChance >= 2)
            {
                //StartCoroutine(GenerateAdditionalItem(randPos: randPos_and_vel.randPos, rand_left_or_right: randPos_and_vel.rand_left_or_right));
            }
        }

    }
    void GenerateRedBanana()
    {
        redBananaCurrentSpawnTimer += Time.deltaTime * redBananaSpawnSpeed;
        if (redBananaCurrentSpawnTimer >= redBananaSpawnTimer)
        {
            redBananaSpawnTimer = Random.Range(redBananaMinSpawnTimer, redBananaMaxSpawnTimer);

            redBananaCurrentSpawnTimer = 0;

            GenerateCommonCode(redBanana);

        }

    }
    IEnumerator GenerateAdditionalItem(Vector3 randPos,int rand_left_or_right)
    {
        yield return new WaitForSeconds(0.5f);
        var objRandNum = Random.Range(0, 3);
        var vel = new Vector3(0, 0, 0);
        var obj2 = new GameObject();
        if (objRandNum == 0)
        {
            obj2 = Instantiate(banana[Random.Range(0,banana.Length-1)], randPos, Quaternion.identity);
        }
        else if (objRandNum == 1)
        {
            obj2 = Instantiate(greenBanana, randPos, Quaternion.identity);
        }
        else
        {
            obj2 = Instantiate(redBanana, randPos, Quaternion.identity);
        }

        var objRB = obj2.GetComponent<Rigidbody2D>();

        if (objRB)
        {
            if (rand_left_or_right < 1)
            {
                vel = CalculateProjectoryVelocty(origin: obj2.transform.position,
                target: new Vector3(Random.Range(mrX, maxBounds.x)
                , Random.Range(minBounds.y, maxBounds.y - 1.6f), 0)
                , time: Random.Range(1f, 2f));
            }
            else
            {
                vel = CalculateProjectoryVelocty(origin: obj2.transform.position,
                target: new Vector3(Random.Range(minBounds.x, mlX)
                , Random.Range(minBounds.y, maxBounds.y - 1.6f), 0)
                , time: Random.Range(1f, 2f));
            }

            objRB.velocity = vel;
        }
        objRB.velocity = vel;
        Destroy(obj2, 5f);

    }
    Vector3 CalculateProjectoryVelocty(Vector3 target, Vector3 origin, float time)
    {
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
    Pos_And_Vel GenerateCommonCode(GameObject objSpawn)
    {
        var vel = new Vector3(0, 0, 0);
        var randPos = transform.position;
        var rand_left_or_right = Random.Range(0, 2);
        if (rand_left_or_right < 1)
        {
            randPos.x = Random.Range(minBounds.x - 1f, mlX);
        }
        else
        {
            randPos.x = Random.Range(mrX, maxBounds.x + 1f);
        }

        randPos.y = minBounds.y;
        if (randPos.x >= maxBounds.x || randPos.x <= minBounds.x)
        {
            //randPos.y = Random.Range(minBounds.y, Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)).y);
        }

        var obj = Instantiate(objSpawn, randPos, Quaternion.identity);
        var objRB = obj.GetComponent<Rigidbody2D>();
        var capsuleColl=obj.GetComponent<CapsuleCollider2D>();
        if(capsuleColl != null)
        {
            capsuleColl.isTrigger = true;
        }
        var circleColl = obj.GetComponent<CircleCollider2D>();
        if (circleColl != null)
        {
            circleColl.isTrigger = true;
        }
        if (objRB)
        {
            if (rand_left_or_right < 1)
            {
                vel = CalculateProjectoryVelocty(origin: obj.transform.position,
                target: new Vector3(Random.Range(mrX-2f, maxBounds.x-2f)
                //, Random.Range(Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)).y, maxBounds.y - 1.6f)
                ,0
                , 0)
                , time: Random.Range(2f, 2.4f));
            }
            else
            {
                vel = CalculateProjectoryVelocty(origin: obj.transform.position,
                target: new Vector3(Random.Range(minBounds.x+2f, mlX+2f)
                //, Random.Range(Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)).y, maxBounds.y - 1.6f)
                ,0
                , 0)
                , time: Random.Range(2f, 2.4f));
            }

            objRB.velocity = vel;
        }
        Destroy(obj, 5f);
        return new Pos_And_Vel { randPos=randPos, rand_left_or_right = rand_left_or_right };
    }

}
