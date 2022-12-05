using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundImage : MonoBehaviour
{
    GameObject objectDestroyer;
    [SerializeField] GameObject startUI;
    // Start is called before the first frame update
    private void Awake()
    {
        objectDestroyer = GameObject.Find("GameObjectDestroyer");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveGroundImage();
    }
    void MoveGroundImage()
    {
        if (!GameController.instance.isPlay && startUI.activeSelf) { return; }
        transform.position = Vector2.Lerp(transform.position, objectDestroyer.transform.position, Time.deltaTime/20);

    }
}
