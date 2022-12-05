using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SV_ITEM_Score : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] Transform itemParent;
    // Start is called before the first frame update
    void Start()
    {
        MakeFakeData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeFakeData()
    {
        for (int i = 0; i < 10; i++)
        {
            var _item = GameObject.Instantiate(item, itemParent);
        }
    }
}
