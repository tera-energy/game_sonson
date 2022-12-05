using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpForce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var theRB=GetComponent<Rigidbody2D>();
        theRB.AddForce(new Vector3(10, 10, 0),ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
