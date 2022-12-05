using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrUI_PuzzleTimer : MonoBehaviour
{
	static TrUI_PuzzleTimer _instance;
	public TextMeshProUGUI _txtTimer;
    public Image _timerBar, _clock;
	float _origFontSize;

	float _tictokTime;
	float _waitTime = 1f;


	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public static TrUI_PuzzleTimer xInstance { get { return _instance; } }

	void ySetTimerColor(float r, float g, float b, float a=1){
		Color color = new Color(r, g, b, 1);
		_timerBar.color = color;
		_txtTimer.color = color;
	}

	void ySetTimerColor(Color col)
	{
		Color color = col;
		_timerBar.color = color;
		_txtTimer.color = color;
	}

	public void zUpdateTimerBar(int timeMax, float currRemainTime) {
		if (currRemainTime < 0) currRemainTime = 0;
		float fillRate = currRemainTime / timeMax;
		_timerBar.fillAmount = fillRate;

		//���� �ð��� ���� Ÿ�ӹ��� ������ �ٲ�.
		if (fillRate > .6f) {
			ySetTimerColor(0.5f, 0.8f, 0, 1);
		} else if (fillRate > 0.3f) {
			ySetTimerColor(TT.zSetColor(TT.enumTrRainbowColor.YELLOW));
		} else {
			ySetTimerColor(1, 0, 0, 1);
		}

		_txtTimer.text = ((int)currRemainTime).ToString();
	}

	//================================================================================================================

	// ����Ƽ �޴����� Edit>Project Settings>Editor �׸��� �ϴܿ� enter play mode �� üũ�ǵǰ� reload domain�� üũ�����ǰ�
	// �����Ǿ� �ִٸ� ����Ƽ ��ü�� ������ ��� static������ ���� ���� �������־�� ��.
	// Ư�� static delegate/event�� ������ ����̳� static �����ͱ����� ����� �� �ſ� �����ؾ� ��.
	// ��, �̰��� �����ͻ��� ���Ǹ� ���Ѱ��̹Ƿ� �������� ������� �������.
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void yResetDomainCodes() {
		_instance = null;
	}

	void Awake() {
		if(_instance == null) {
			_instance = this;
			//_origFontSize = _txtTimer.fontSize;
		} else {
			Destroy(gameObject);
		}
		_timerBar.fillAmount = 1;
	}
}
