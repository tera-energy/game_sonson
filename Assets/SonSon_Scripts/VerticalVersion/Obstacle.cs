using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody2D theRB;

    //Components
    private GameController gameController;


    public GameObject particleSystemExplosion;

    private float width;
    private bool isRotateLeft;

    #region Standart system methods

    void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

    }
    private void Start()
    {
        //ApplySettings();
    }
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        //Rotate();
    }

    #endregion
  

    private void Rotate()
    {
        if (gameController.isPlay) {
            transform.Rotate(Vector3.forward * (isRotateLeft ? 30 : -30) * Time.fixedDeltaTime * 10f);
        }
    }


    #region State

    private void ApplySettings()
    {
        isRotateLeft = Random.Range(0, 2) == 0;

        //Set random position x
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f));

        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        Vector3 size = renderer.bounds.size;
        float width = size.x * transform.localScale.x;

        float positionXBorderRight = stageDimensions.x - width / 2f;
        float positionXBorderLeft = -positionXBorderRight;
        float positionX = Random.Range(positionXBorderLeft, positionXBorderRight);

        transform.position = new Vector3(positionX, transform.position.y, transform.position.z);

    }

    private void Kill()
    {

        GameObject particle = GameObject.Instantiate(particleSystemExplosion
                , new Vector3(transform.position.x+0.3f, transform.position.y, transform.position.z)
                , Quaternion.identity);
        /*
        newParticleSystem.transform.localScale= new Vector3(newParticleSystem.transform.localScale.x*1.5f
            , newParticleSystem.transform.localScale.y*1.5f
            , newParticleSystem.transform.localScale.z*1.5f);
        */

        Destroy(gameObject);
        Destroy(particle,3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == MySonSonTags.Tags.Player)
        {
            Kill();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == MySonSonTags.Tags.Player)
        {
            Kill();
        }
    }


    #endregion
}
