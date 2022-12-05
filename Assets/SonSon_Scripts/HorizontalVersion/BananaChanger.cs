using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaChanger : MonoBehaviour
{
    public int minBanana = 5;
    public int maxBanana = 9;
    [SerializeField] float objMoverEnableSec = 0.1f;

    int howmany = 0;
    int backwardObjs = 0;
    GameObject obj;
    GameObject explosionObj;
    Vector3 pos;

    IEnumerator EnableObjectMover(GameObject obj)
    {
        yield return new WaitForSeconds(objMoverEnableSec);
        try
        {
            //var therb = obj.GetComponent<Rigidbody2D>();
            //therb.velocity = Vector2.zero;
            if (obj)
            {
                var objMover = obj.GetComponent<Object_Mover_HV>();
                if (objMover) { objMover.enabled = true; }
            }
            
        }
        catch { }
    }
    void BananaChange1(Collider2D other)
    {
        if (other.tag == MySonSonTags.Tags.Obstacle)
        {
            pos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            howmany = Random.Range(minBanana, maxBanana);
            backwardObjs = Random.Range(2, (int)(howmany / 2));
            for (int i = 0; i < howmany; i++)
            {
                if (i <= backwardObjs)
                {
                    obj = Instantiate(GeneratorController_HV_Throw.instance.banana[0]
                   , new Vector3(other.transform.position.x - 0.11f - 0.01f * i, other.transform.position.y + Random.Range(-0.1f, 0.1f), other.transform.position.z)
                   , Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
                    var objMover = obj.GetComponent<Object_Mover_HV>();
                    if (objMover) { objMover.enabled = true; }
                }
                else
                {
                    obj = Instantiate(GeneratorController_HV_Throw.instance.banana[0]
                    , new Vector3(other.transform.position.x - 0.1f, other.transform.position.y + Random.Range(-0.1f, 0.1f), other.transform.position.z)
                    , Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
                }
                
                Destroy(obj, 5f);
            }
            explosionObj = Instantiate(GeneratorController_HV_Throw.instance.explosionEffector
                , new Vector3(other.transform.position.x - 0.1f, other.transform.position.y, other.transform.position.z), Quaternion.identity);
            //StartCoroutine(BananaChange());
            Destroy(explosionObj, 0.2f);
            Destroy(other.gameObject);
        }
    }
    void BananaChange2(Collider2D other)
    {
        if (other.tag == MySonSonTags.Tags.Obstacle)
        {
            pos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            howmany = Random.Range(minBanana, maxBanana);
            for (int i = 0; i < howmany; i++)
            {

                obj = Instantiate(GeneratorController_HV_Throw.instance.banana[0]
               , new Vector3(other.transform.position.x +1.1f + 0.04f * i, other.transform.position.y, other.transform.position.z)
               , Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
                StartCoroutine(EnableObjectMover(obj));

                EnableObjectMover(obj);
                Destroy(obj, 5f);
            }
            explosionObj = Instantiate(GeneratorController_HV_Throw.instance.explosionEffector
                , new Vector3(other.transform.position.x + 1.1f, other.transform.position.y-0.1f, other.transform.position.z), Quaternion.identity);
            Destroy(explosionObj, 0.15f);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        BananaChange2(other);
    }


}
