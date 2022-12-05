using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Video;

public class TrTitleScript : MonoBehaviour
{
    VideoPlayer _vidPlayer;
    public GameObject _rainbowSparkParticle;
    AsyncOperation async;

    private void Awake()
	{
        _vidPlayer = GetComponent<VideoPlayer>();
	}

	void Start()
    {
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        _vidPlayer.loopPointReached += yLoadNextScene;
        _vidPlayer.Play(); 
    }

    void Update() {
        //������ �÷��̵ǰ� 2.3�ʰ� ������
        //(���� ��Ȯ�� Ÿ�̹��� ���ؼ� _vidPlayer.time ��� _vidPlayer.frame�� ���)
        if(!_rainbowSparkParticle.activeSelf && _vidPlayer.time > 2.3f) {
            //���� ��ƼŬ�� ������ ������ ������ �ʴ´ٸ� �����÷��̾� ������Ʈ�� render mode ���� Camera Far Plane���� �����Ͻÿ�!
            _rainbowSparkParticle.SetActive(true);
		}
	}

    void yLoadNextScene(VideoPlayer vp) {
        TT.UtilDelayedFunc.zCreate(() => async.allowSceneActivation = true,1f);
	}

	private void OnDestroy() {
        _vidPlayer.loopPointReached -= yLoadNextScene;
        _vidPlayer = null;
	}
}
