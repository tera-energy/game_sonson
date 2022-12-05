using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrUI_TutorialFocus : MonoBehaviour
{
    static TrUI_TutorialFocus _instance;

    public GameObject _goTutorial;
    public RectTransform _rectEventMaskParent; // ����ũ�� �θ� ��ü(anchormin, anchormax ������ ����)
    public RectTransform _rectEventMask; // ����ũ�� ��ġ
    public RectTransform _rectEventFinger; // �հ��� ��ġ
    public Button _btnUnmask;

    void OnDestroy()
    {
        _btnUnmask.GetComponent<Button>()
            .onClick
            .RemoveAllListeners();
    }

    public static TrUI_TutorialFocus xInstance { get { return _instance; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void yResetDomainCodes(){
        _instance = null;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
