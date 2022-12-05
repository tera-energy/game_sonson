using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainPosChanger : MonoBehaviour
{
    public Transform mountain1;
    public Transform mountain2;
    public Vector2 mountain2Pos = new Vector2(24f, -2.75f);

    private void FixedUpdate()
    {
        if(mountain1 && mountain1.position.x <= -22f)
        {
            mountain1.position = mountain2Pos;
        }
        if (mountain2 && mountain2.position.x <= -22f)
        {
            mountain2.position = mountain2Pos;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == MySonSonTags.Tags.Mountain)
        {
            var mountainObj = other.transform.parent.gameObject;
            mountainObj.transform.position = mountain2Pos;
        }
    }
}
