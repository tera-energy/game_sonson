using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectory : MonoBehaviour
{
    Rigidbody2D theRB;
    private void Awake()
    {
        theRB=GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Throw());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
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
    IEnumerator Throw()
    {
        yield return new WaitForSeconds(2f);
        Vector3 vo = CalculateVelocty(new Vector3(-6.87f, -0.5f,0), transform.position, 6f);
        theRB.velocity = vo;
    }

}
