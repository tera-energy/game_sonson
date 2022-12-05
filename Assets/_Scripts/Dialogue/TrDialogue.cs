using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrDialogue{
    [Header("��� ġ�� ĳ���� �̸�")]
    public string _name;

    [Header("��� ����")]
    public string[] _contexts;

    [Header("�̹��� ��ȣ")]
    public int[] _numImage;

    [Header("�̺�Ʈ ����")]
    public char[] _event;
}


[System.Serializable]
public class TrDialogueEvent{
    public string _name;

    public TrDialogueClickEvent[] _clickEvent;

    public Vector2 _line;
    public TrDialogue[] _dialogues;
}

[System.Serializable]
public class TrDialogueClickEvent
{
    public bool _isUI;
    public bool _needClick;

    public Transform _trTarget;
    public Vector3 _addMaskPos;

    public RectTransform _rectParentTarget;
    public RectTransform _rectTarget;

    public Vector3 _sizeMask;

    public int _eventClickNumber;
}