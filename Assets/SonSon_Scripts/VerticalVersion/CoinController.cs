
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public GameObject particleCoinCollect;

    private void Awake()
    {
    }



    void Start()
    {

    }
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        Rotate();
    }
    private void Rotate()
    {
        transform.Rotate(Vector3.forward * -30 * Time.fixedDeltaTime * 10f);
    }

    private void MakeParticle()
    {
        GameObject particle = GameObject.Instantiate(particleCoinCollect
                , new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z)
                , Quaternion.identity);
        var coinParticle = particle.GetComponent<ParticleSystem>();
        if (coinParticle != null)
        {
            coinParticle.Play();
        }
        Destroy(particle, 1f);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            MakeParticle();
            Kill();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            MakeParticle();
            Kill();
        }
       
    }

}
