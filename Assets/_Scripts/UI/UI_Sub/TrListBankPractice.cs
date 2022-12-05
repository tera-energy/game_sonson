using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AirFishLab.ScrollingList;
using UnityEngine.Events;

[System.Serializable]
public class trScrollContentPractice {
	[SerializeField] TrSO_PuzzleData _puzzleData;

	public TrSO_PuzzleData xPuzzleData { get { return _puzzleData; } }
}



public class TrListBankPractice : BaseListBank
{
    // class ���� ScriptableObject�� Ȱ���ϴ°��� ���� ��. --> SO���� �̺�Ʈ ����� �ȵǹǷ� �׳� Ŭ���� ���.
    [SerializeField] trScrollContentPractice[] _contents;
	trScrollContentPractice[] _origContents;

	private void Awake() {
		_origContents = new trScrollContentPractice[_contents.Length];
		_contents.CopyTo(_origContents, 0);
	}


	public void zSetListContents(trScrollContentPractice[] newContents) {
		_contents = newContents;
	}

	//�̰� ȣ�� �� �� �ش� CircularScrollingList�� ������Ʈ.Refresh()�� �ؾ� ������ ȿ���� �ݿ���
	public void zResetToOriginalContents() {
		_contents = new trScrollContentPractice[_origContents.Length];
		_origContents.CopyTo(_contents, 0);
	}

	public trScrollContentPractice[] zGetAllListContents() {
		return _contents;
	}

    public override object GetListContent(int index) {
		return _contents[index];
    }

    public override int GetListLength() {
        return _contents.Length;
    }
}
