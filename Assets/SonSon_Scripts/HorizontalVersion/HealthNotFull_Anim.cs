using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNotFull_Anim : MonoBehaviour
{
    public HealthNotFull_Anim instance;
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        MakeInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        ShowHealthNotFull();
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    void ShowHealthNotFull()
    {
        if (PlayerControl_HV.instance.currentRedBananaTimer < PlayerControl_HV.instance.maxRedbananaTimer)
        {
            animator.SetBool(MySonSonTags.Animator.health_not_full, false);
        }
        else if(PlayerControl_HV.instance.currentHp<= 10)
        {
            animator.SetBool(MySonSonTags.Animator.health_not_full, true);
        }
        else
        {
            animator.SetBool(MySonSonTags.Animator.health_not_full, false);
        }
    }

}
