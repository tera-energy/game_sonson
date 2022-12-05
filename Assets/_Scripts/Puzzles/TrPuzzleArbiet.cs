using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;

public struct TrArbeit
{
    public GameObject _currCustomer; //��
    public int _customerMoney; //�� ��
    public int _productPrice; //����
    public GameObject _currProduct; //��ǰ
    public int _rndPrice; //������ ����
    public int _realPrice; //�ùٸ� ����
    public int _saveRndPrice; //������ ���� ����
}

public class TrPuzzleArbiet : MonoBehaviour
{

    [SerializeField] TT.enumDifficultyLevel _difficulty = TT.enumDifficultyLevel.Normal;
    public enum enumButtonInputOX { O, X }


    static TrPuzzleArbiet _instance;

    TrArbeit _info;
    [SerializeField] int[] currCusMoney;
    [SerializeField] Image Pos;
    [SerializeField] Image CustomerMoney;
    [SerializeField] Image[] ProductImage;

    [SerializeField] GameObject[] _cusPrefab;
    [SerializeField] GameObject[] _proPrefab;

    [SerializeField] TextMeshProUGUI _currCusMoney;
    [SerializeField] TextMeshProUGUI _currPosMoney;
    [SerializeField] TextMeshProUGUI[] ProductPriceText;

    [SerializeField] int[] _proPrice;

    [SerializeField] int _startTime = 3;
    [SerializeField] int _maxGameTime = 15;
    float _currTimer;
    float _currGameTime;
    int _currStartTime;
    int _currScore = 0;
    int productNum;
    int customerMoneyNum;

    bool _isGameStarted = false;

    public static TrPuzzleArbiet xInstance { get { return _instance; } }


    void yGameTimer()
    {
        if (_isGameStarted && _currGameTime >= 0)
        {

            _currGameTime -= Time.deltaTime;
            TrUI_PuzzleTimer.xInstance.zUpdateTimerBar(_maxGameTime, _currGameTime);
        }
    }

    IEnumerator yProcReadyGame()
    {
        TrUI_PuzzleArbeit.xInstance.zSetNotice(_currStartTime);

        while (_currStartTime > 0)
        {
            yield return new WaitForSeconds(1f);
            TrUI_PuzzleArbeit.xInstance.zSetNotice(--_currStartTime);
        }
        _isGameStarted = true;
        zSetGame();

    }
    //�� ����
    void ySetCustomer()
    {
        
        int rndCustomer = Random.Range(0, _cusPrefab.Length);
        _info._currCustomer = Instantiate(_cusPrefab[rndCustomer]);
        _info._currCustomer.transform.localPosition = new Vector3(-5f, 0.5f, 0);
        _info._currCustomer.transform.localScale = new Vector3(0.4f, 0.4f, 0);
    }

    //��ǰ ���� �� ��ǰ ����
    void ySetProduct()
    {

        //��ǰ ����
        int rndProduct = Random.Range(0, productNum);
        _info._currProduct = Instantiate(_proPrefab[rndProduct]);
        _info._currProduct.transform.localPosition = new Vector3(-5f, -1.5f, 0);
        _info._currProduct.transform.localScale = new Vector3(0.2f, 0.2f, 0);
        _info._productPrice = _proPrice[rndProduct];
        
        //���� �� ���� �� ��ǰ�� ���� ���
        int rndCusMoney = Random.Range(0, customerMoneyNum);
        _info._customerMoney = currCusMoney[rndCusMoney];
        _currCusMoney.text = _info._customerMoney.ToString();

        //�������� ���� �����ϰ� �� ����
        int rndProPrice = Random.Range(0, _proPrice.Length);
        _info._rndPrice = _proPrice[rndProPrice];
        
        //�����ϰ� �� ������ �����⿡ �Է�
        _currPosMoney.text = (_info._customerMoney - _info._rndPrice).ToString();
        _info._saveRndPrice = _info._customerMoney - _info._rndPrice; //�� ����
        
        //�ùٸ��� �� ��
         _info._realPrice = _info._customerMoney - _info._productPrice;
    }

    public void zButtonInput(enumButtonInputOX input)
    {
        if(input == enumButtonInputOX.O)
        {
            
            if (_info._realPrice == _info._saveRndPrice)
            {

                _currScore += 5;
                if (_currScore <= 0) _currScore = 0;
                TrUI_PuzzleArbeit.xInstance.zSetNotice(-3);
                TrUI_PuzzleArbeit.xInstance.zSetScore(_currScore);
            }
            if (_info._realPrice != _info._saveRndPrice)
            {
                
                TrUI_PuzzleArbeit.xInstance.zSetNotice(-2);
                TrUI_PuzzleArbeit.xInstance.zSetScore(--_currScore);
            }
        }
        //���� ����
        else if (input == enumButtonInputOX.X)
        {
            if (_info._realPrice == _info._saveRndPrice)
            {

                _currScore -= 5;
                if (_currScore <= 0) _currScore = 0;
                TrUI_PuzzleArbeit.xInstance.zSetNotice(-2);
                TrUI_PuzzleArbeit.xInstance.zSetScore(_currScore);

            }

            if(_info._realPrice != _info._saveRndPrice)
            {
                TrUI_PuzzleArbeit.xInstance.zSetNotice(-3);
                TrUI_PuzzleArbeit.xInstance.zSetScore(++_currScore);
            }
        }
        StartCoroutine(yResetGame());
    }
    
    IEnumerator yResetGame()
    {
        
        yield return new WaitForSeconds(0.1f);
        //������ �ؽ�Ʈ �ʱ�ȭ
        _currPosMoney.text = "";
        //�� �����հ� �ؽ�Ʈ �ʱ�ȭ
       _info._currCustomer.gameObject.SetActive(false);
        _currCusMoney.text = "";
        //��ǰ �ʱ�ȭ
        _info._currProduct.gameObject.SetActive(false);

        zSetGame();
    }

    void zSetGame()
    {
        Pos.gameObject.SetActive(true);
        CustomerMoney.gameObject.SetActive(true);

        ySetCustomer();
        ySetProduct();        
                         
    }

    void yOnDifficultyChange(int nanido)
    {
        switch (nanido)
        {
            case 1:
                //��ǰ 2��, �� ���� �ִ� �� 300
                ProductImage[2].gameObject.SetActive(false);
                ProductPriceText[2].gameObject.SetActive(false);
                ProductImage[3].gameObject.SetActive(false);
                ProductPriceText[3].gameObject.SetActive(false);
                
                productNum = 2;
                customerMoneyNum = 1;
                
                break;

            case 2:
                //��ǰ 2��, �� ���� �ִ� �� 500
                ProductImage[2].gameObject.SetActive(false);
                ProductPriceText[2].gameObject.SetActive(false);
                ProductImage[3].gameObject.SetActive(false);
                ProductPriceText[3].gameObject.SetActive(false);

                productNum = 2;
                customerMoneyNum = 2;

                break;

            case 3:
                //��ǰ 3�� �� ���� �ִ� �� 700
                ProductImage[2].gameObject.SetActive(true);
                ProductPriceText[2].gameObject.SetActive(true);

                productNum = 3;
                customerMoneyNum = 3;

                break;
            case 4:
                //��ǰ 4��, �� ���� �ִ� �� 700
                ProductImage[3].gameObject.SetActive(true);
                ProductPriceText[3].gameObject.SetActive(true);

                productNum = 4;
                customerMoneyNum = 3;

                break;

            case 5:
                //��ǰ 4��, �� ���� �ִ� �� 1000
                ProductImage[3].gameObject.SetActive(true);
                ProductPriceText[3].gameObject.SetActive(true);

                productNum = 4;
                customerMoneyNum = 5;
                break;

        }
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
    // Start is called before the first frame update
    void Start()
    {

        productNum = 3;
        customerMoneyNum = 3;
        TrUI_DifficultyMenu.xInstance.zSetMenuIndex((int)_difficulty - 1);
        TrUI_DifficultyMenu.xInstance._onDifficultySelected += yOnDifficultyChange;
        StartCoroutine(yProcReadyGame());
    }

    // Update is called once per frame
    void Update()
    {
        yGameTimer();


    }
}

