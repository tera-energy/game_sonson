using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController_HV : MonoBehaviour
{
    public static GameController_HV instance;

    [Header("SonSon save game data")]
    public GameData gameData;
    const string data_Path = "/GameData.dat";

    public int coins = 0;
    public int bananaCnt = 0;
    [SerializeField] TextMeshProUGUI coinUI_Text;


    [Header("Game values")]
    public bool isPlay = false;
    private bool isWaitOnPause = false;
    public float overallSpeed = 1f;
    public float obstacleAddSpeedTimer;
    public float obstacleAddSpeedTimer_treshold = 60f;
    public float obstacleAddSpeed = 1f;
    public float obstacleAddSpeedMult = 1.3f;
    public float obstacleAddSpeedMax = 3f;

    [Header("speed effect in sonson")]
    float effectSimuleSpeed = 1.2f;
    public ParticleSystem speedEffect;
    public ParticleSystem points;
    public ParticleSystem trails;
    public ParticleSystem[] winds;

    public int score = 0;

    [Header("Audio")]
    public bool increaseScoreSound = false;
    public bool increaseCoinsSound = true;

    [Header("UI")]
    [SerializeField] GameObject fader;
    [SerializeField] GameObject startUI;
    [SerializeField] GameObject startImageUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] public GameObject gameoverUI;
    public GameObject gameOverImageUI;

    [SerializeField] GameObject readyUI;
    [SerializeField] GameObject goUI;

    [Header("InitBound_to_camera")]
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float bounder_coll_y_size;
    public float paddingBorder = .3f;

    [Header("Left, Right Bounder")]
    [SerializeField] GameObject topBoundObject;
    [SerializeField] GameObject bottomBoundObject;
    [SerializeField] GameObject mountainPosChanger;


    [Header("Audio Clips")]
    public AudioClip switchDirectionClip;
    public AudioClip hitByObstacleClip;
    public AudioClip eatBananaClip;
    public AudioClip _showSFX;
    public AudioClip _moveClip;
    public AudioClip _gameover;

    private void Awake()
    {
        MakeInstance();
        InitBounds();
        obstacleAddSpeedTimer = Time.time + obstacleAddSpeedTimer_treshold;
    }


    void Start()
    {
        isPlay = false;
        startUI.SetActive(true);
        InitilizeGameData();

        var tempVL = new Vector2(minBounds.x, 0);
        var tempVR = new Vector2(maxBounds.x, 0);
        if (mountainPosChanger != null)
        {
            mountainPosChanger.transform.position = tempVL;
        }
        var tempVT = new Vector2(0, maxBounds.y);
        if (topBoundObject != null)
        {
            bounder_coll_y_size = topBoundObject.GetComponent<BoxCollider2D>().size.y;
            topBoundObject.transform.position = new Vector2(tempVT.x, tempVT.y + (bounder_coll_y_size / 2));
        }
        var tempVB = new Vector2(0, minBounds.y);
        if (bottomBoundObject != null)
        {
            bounder_coll_y_size = bottomBoundObject.GetComponent<BoxCollider2D>().size.y;
            bottomBoundObject.transform.position = new Vector2(tempVB.x, tempVB.y - (bounder_coll_y_size / 2));
        }

        Time.timeScale = 1f;
        coins = 0;

        if (fader)
        {
            fader.SetActive(true);

            startUI.SetActive(true);
            var cg = fader.GetComponent<CanvasGroup>().DOFade(0, 1.5f).OnComplete(() =>
            {
                StartCoroutine(nameof(StartGame));
            });
        }

    }

    private void Update()
    {
        ObstacleAddSpeed();
        CheckSpeedEffect();
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

    IEnumerator StartGame()
    {
        // 기존 무무에서쓰던 ready go
        /*
        TrAudio_UI.xInstance.zzPlay_Ready(0);
        readyUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        readyUI.SetActive(false);
        startUI.SetActive(false);

        TrAudio_UI.xInstance.zzPlay_Ready(1);
        goUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        goUI.SetActive(false);
        isPlay = true;
        isWaitOnPause = false;
        */


        // 손손 ppt에 있는 start
        TrAudio_UI.xInstance.zzPlay_Ready(0);
        yield return new WaitForSeconds(1.1f);
        TrAudio_UI.xInstance.zzPlay_Ready(1);
        startImageUI.SetActive(true);
        startImageUI.transform.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 1.1f);
        //startImageUI.transform.DOPunchRotation(new Vector3(5f, 5f, 5f), 1.1f);
        yield return new WaitForSeconds(1.1f);
        startUI.SetActive(false);
        isPlay = true;
        isWaitOnPause = false;

        TrAudio_Music.xInstance.zzPlayMain(0.1f);

    }


    /// <summary>
    /// Call when game is over. To save game process
    /// </summary>


    public void IncreaseCoindBy(int value)
    {
        coins += (value * 10);
        coinUI_Text.text = $" {coins}P";

    }
    void ObstacleAddSpeed()
    {
        if (!GameController_HV.instance.isPlay || !PlayerControl_HV.instance.bAlive)
        {
            return;
        }
        if (Time.time > obstacleAddSpeedTimer)
        {
            PlayerControl_HV.instance.hpForceMinusPenaltyValue++;
            if (PlayerControl_HV.instance.hpForceMinusPenaltyValue > 6)
            {
                PlayerControl_HV.instance.hpForceMinusPenaltyValue = 6;
            }
            obstacleAddSpeed *= obstacleAddSpeedMult;
            if (obstacleAddSpeed > obstacleAddSpeedMax)
            {
                obstacleAddSpeed = obstacleAddSpeedMax;
            }
            /*
            // 게임이 빨라질 때마다 스피드 이펙트도 빨라지도록 하는 코드.
            if (effectSimuleSpeed < 3.5)
            {
                effectSimuleSpeed += .5f;
                var point_particle_main = points.main;
                point_particle_main.simulationSpeed = effectSimuleSpeed;
                var trail_particle_main = trails.main;
                trail_particle_main.simulationSpeed = effectSimuleSpeed;
            }
            */
            speedEffect.Play(true);
            trails.Play(true);
            Invoke(nameof(StopTrailEffect),2f);
            for(int i = 0; i < winds.Length; i++)
            {
               StartCoroutine(_PlayWindEffect(i,(i*0.1f)*3));
            }
            obstacleAddSpeedTimer = Time.time + obstacleAddSpeedTimer_treshold;

        }
    }
    public void CheckSpeedEffect()
    {
        if (PlayerControl_HV.instance.currentRedBananaTimer < PlayerControl_HV.instance.maxRedbananaTimer
            && speedEffect.isPlaying)
        {
            speedEffect.Stop();
            trails.Stop();
        }
       
    }
    public void StopTrailEffect()
    {
        trails.Stop();
    }
    IEnumerator _PlayWindEffect(int windIndex,float waitForSec)
    {
        yield return new WaitForSeconds(waitForSec);
        winds[windIndex].Play(true);
    }

    public void PauseOn()
    {
        if (isPlay)
        {
            TrAudio_UI.xInstance.zPlaySFX(_showSFX, 0f);
            pauseUI.SetActive(true);
            isPlay = false;
            isWaitOnPause = true;
            Time.timeScale = 0f;
            TrAudio_Music.xInstance.PauseAllKindBGMs();
        }
    }

    public void PauseOff()
    {
        if (isWaitOnPause)
        {
            TrAudio_UI.xInstance.zPlaySFX(_showSFX, 0f);
            pauseUI.SetActive(false);
            isPlay = true;
            isWaitOnPause = false;
            Time.timeScale = 1f;
            TrAudio_Music.xInstance.ResumeAllKindBGMs();
        }
    }
    public void OnRestart(){
        if (GameManager._type == TT.enumGameType.Train){
            yRestartEffect();
        }
        else if (GameManager._type == TT.enumGameType.Challenge){
            StartCoroutine(StaminaManager.xInstance.zCheckStamina(yRestartEffect));
        }
    }

    void yRestartEffect(){
        TrAudio_UI.xInstance.zPlaySFX(_showSFX, 0f);
        pauseUI.SetActive(false);
        isPlay = false;
        isWaitOnPause = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnToMain()
    {
        Time.timeScale = 1f;
        if (isWaitOnPause)
        {
            TrAudio_UI.xInstance.zPlaySFX(_showSFX, 0f);
            pauseUI.SetActive(false);
            isPlay = false;
            isWaitOnPause = true;
            var cg = fader.GetComponent<CanvasGroup>().DOFade(1, 2f).OnComplete(() =>
            {
                SceneManager.LoadScene(0);
            });

        }
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    public void SaveGameData()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + data_Path);
            if (gameData != null)
            {
                gameData.currentCoins = coins;
                bf.Serialize(file, gameData);
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
    public void LoadGameData()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + data_Path, FileMode.Open);
            gameData = (GameData)bf.Deserialize(file);
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
    void InitilizeGameData()
    {
        LoadGameData();
        if (gameData == null)
        {
            // running game for first time
            gameData = new GameData();
            SaveGameData();
        }
    }
}
