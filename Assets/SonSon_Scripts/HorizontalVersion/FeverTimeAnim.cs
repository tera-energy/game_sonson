using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverTimeAnim : MonoBehaviour
{
    public FeverTimeAnim instance;
    public Animator animator;

    void Awake() {
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
    void FixedUpdate()
    {
        ShowFeverTime();
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
    void ShowFeverTime()
    {
        if (PlayerControl_HV.instance.currentRedBananaTimer < PlayerControl_HV.instance.maxRedbananaTimer)
        {
            animator.SetBool(MySonSonTags.Animator.fevertime, true);
            var nearEndTime = PlayerControl_HV.instance.maxRedbananaTimer * 0.9f;
            if (PlayerControl_HV.instance.currentRedBananaTimer >= nearEndTime)
            {
                animator.SetFloat(MySonSonTags.Animator.fevertime_speed, 5f);
            }
        }
        else
        {
            animator.SetBool(MySonSonTags.Animator.fevertime, false);
            animator.SetFloat(MySonSonTags.Animator.fevertime_speed, 1f);
        }
    }

}
