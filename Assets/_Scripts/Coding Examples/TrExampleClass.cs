using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrExampleClass : MonoBehaviour
{
    const float _ABC_DEF = 1f;
    static int _AbcDef;
    int _abcDef;
    [SerializeField] string _qweXyz;
    Vector3 _origPos; // ����(���ӿ�����Ʈ�� ó�� ��ġ�� ���ư� �� ��)�� �� �ʿ��� ������ �տ� orig(original)�� �ٿ��ּ���. 
    Coroutine _procCoroutine1;
    Coroutine _procCoroutine2;
    
    public SpriteRenderer _image; // �ܼ� ���۷����θ� ����� �� public ��밡��.
    [SerializeField] Transform _transform; // this ���ӿ�����Ʈ�� Transform�� ĳ���Ͽ� ���� ������ ���� �����̹Ƿ� private.
    
    TT.CallbackBool _onAbcDef;        // Delegate�̳� �̺�Ʈ ���� �������� on�� �տ� �־��ּ���.
    public event TT.CallbackInt _onAbcXyz;
    TrAbc_Def _trAbc_Def;

    // �޼ҵ� prefix ===> ������Ƽ:xAbcDef, �ۺ�&������Ʈ:zAbcDef, �����̺�:yAbcDef
    public int xAbcDef { get { return _AbcDef; } set { _AbcDef = value; } }
    public string xQweXyz { get { return _qweXyz; } }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // ����Ƽ �Լ�
    void Start() {
        _onAbcDef += yOnBool;
        _onAbcXyz += yOnRecieveResponse;
        _onAbcDef.Invoke(true);
        _onAbcXyz.Invoke(0);

        _procCoroutine1 = StartCoroutine(yProcCoroutine1(0));
        _procCoroutine2 = StartCoroutine(yProcCoroutine2(0));
    }


    void Update() {
        int abcDef = 0;
        abcDef++;
    }


    //=====================================================================================================
    // Ŭ���� �Լ�
    public void zFooMethod(string abcDef) {
        _trAbc_Def.zMethod1();
        _trAbc_Def.zzMethod2();
    }

    void yMooMethod(int abcDef) {
        StopCoroutine(_procCoroutine1);
        StopCoroutine(_procCoroutine2);
    }

    void yOnBool(bool isYes)    // �̺�Ʈ �ڵ鷯�� �޼ҵ�prefix �ڿ� On�� �ٿ��ּ���.
    {
        if(isYes) {

        } else {

        }
    }

    void yOnRecieveResponse(int code) {
        if(code == 0) {

        } else if(code == 1) {

        } else if(code == 2) {

        } else {

        }
    }

    IEnumerator yProcCoroutine1(int x)  // �ڷ�ƾ�� Proc��...
    {
        yield return null;
    }

    IEnumerator yProcCoroutine2(int y) {
        yield return null;
    }
}
