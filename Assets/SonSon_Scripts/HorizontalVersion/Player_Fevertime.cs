using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fevertime : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject bananaChanger;
    public GameObject boosterEffect;

    void Awake()
    {
        animator = GetComponent<Animator>();
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
        DanceFeverTime();
    }
    void DanceFeverTime()
    {
        if (PlayerControl_HV.instance.currentRedBananaTimer < PlayerControl_HV.instance.maxRedbananaTimer)
        {
            animator.SetBool(MySonSonTags.Animator.fevertime, true);
            if (bananaChanger)
            {
                bananaChanger.SetActive(true);
            }
            GameController_HV.instance.overallSpeed = 2f;
            if (boosterEffect)
            {
                boosterEffect.SetActive(true);
            }
            var nearEndTime = PlayerControl_HV.instance.maxRedbananaTimer * 0.9f;
            if (PlayerControl_HV.instance.currentRedBananaTimer >= nearEndTime)
            {
                animator.SetBool(MySonSonTags.Animator.blink, true);
            }
        }
        else
        {
            animator.SetBool(MySonSonTags.Animator.fevertime, false);
            animator.SetBool(MySonSonTags.Animator.blink, false);
            if (bananaChanger)
            {
                bananaChanger.SetActive(false);
            }
            GameController_HV.instance.overallSpeed = 1f;
            if (boosterEffect)
            {
                boosterEffect.SetActive(false);
            }
            TrAudio_Music.xInstance.ResumeBGM();
        }
    }

}
