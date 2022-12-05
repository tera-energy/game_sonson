using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Animator _anim;



    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_anim)
        {
            _anim.SetTrigger("IsClickDown");
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

}
