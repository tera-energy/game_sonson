using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BzrFollow : MonoBehaviour
{
    [SerializeField] Transform[] routes;
    int routeToGo;
    float tParam;
    Vector2 objPosition;
    [SerializeField] float speedModifier;
    bool coroutineAllowed;
    float _beforeX;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByRoute(routeToGo));
        }
    }

    private IEnumerator GoByRoute(int routeNumber)
    {
        coroutineAllowed = false;
        var p0 = routes[routeNumber].GetChild(0).position;
        var p1 = routes[routeNumber].GetChild(1).position;
        var p2 = routes[routeNumber].GetChild(2).position;
        var p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            objPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;
            transform.position = objPosition;
            yield return new WaitForEndOfFrame();
        }
        tParam = 0f;
        routeToGo += 1;
        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }
        coroutineAllowed = true;

        if (transform.position.x < _beforeX)
            transform.localScale = new Vector3(1, 1, 1);
        //_sr.flipX = false;
        else if (transform.position.x > _beforeX)
            transform.localScale = new Vector3(-1, 1, 1);
        //_sr.flipX = true;
        _beforeX = transform.position.x;
    }
}
