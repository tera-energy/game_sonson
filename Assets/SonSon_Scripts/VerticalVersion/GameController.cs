

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

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("SonSon save game data")]
    public GameData gameData;
    const string data_Path = "/GameData.dat";

    int coins = 0;
    public int bananaCnt = 0;
    int dodged_obstacle = 0;
    [SerializeField] TextMeshProUGUI coinUI_Text;
    [SerializeField] TextMeshProUGUI obstacleUI_Text;

    //Values
    [Header("Game values")]
    public bool isPlay = false;
    private bool isWaitOnPause = false;

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
    [SerializeField] public float paddingLeft = .3f;
    [SerializeField] public float paddingRight = .3f;

    [Header("Left, Right Bounder")]
    [SerializeField] GameObject leftBoundObject;
    [SerializeField] GameObject rightBoundObject;

    [Header("Left, Right Bird Spawn Point")]
    [SerializeField] GameObject birdSpawnPointL;
    [SerializeField] GameObject birdSpawnPointR;

    [Header("Audio Clips")]
    public AudioClip switchDirectionClip;
    public AudioClip hitByObstacleClip;
    public AudioClip eatBananaClip;

    private void Awake()
    {
        MakeInstance();
        InitBounds();
    }

    #region Standart system methods

    void Start()
    {
        isPlay = false;
        startUI.SetActive(true);
        InitilizeGameData();

        var tempVL = new Vector2(minBounds.x - paddingLeft, 0);
        if (leftBoundObject != null)
        {
            leftBoundObject.transform.position = tempVL;
        }
        var tempVR = new Vector2(maxBounds.x + paddingRight, 0);
        if (rightBoundObject != null)
        {
            rightBoundObject.transform.position = tempVR;
        }
        if (birdSpawnPointL)
        {
            birdSpawnPointL.transform.position = new Vector3(tempVL.x-0.1f, birdSpawnPointL.transform.position.y, birdSpawnPointL.transform.position.z);
        }
        if (birdSpawnPointR)
        {
            birdSpawnPointR.transform.position = new Vector3(tempVR.x + 0.1f, birdSpawnPointR.transform.position.y, birdSpawnPointR.transform.position.z);
        }
        Time.timeScale = 1f;
        coins = 0;
        dodged_obstacle = 0;
        
        //Accept some settings
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

    #endregion
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
        yield return new WaitForSeconds(0.5f);
        startImageUI.SetActive(true);
        startImageUI.transform.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f),1.1f);
        //startImageUI.transform.DOPunchRotation(new Vector3(5f, 5f, 5f), 1.1f);
        yield return new WaitForSeconds(1f);
        startUI.SetActive(false);
        isPlay = true;
        isWaitOnPause = false;

        var bgObjects = GameObject.FindGameObjectsWithTag(MySonSonTags.Tags.BGObject);
        foreach (var bgObject in bgObjects)
        {
            var theRb = bgObject.GetComponent<Rigidbody2D>();
            if (theRb)
            {
                theRb.bodyType = RigidbodyType2D.Dynamic;
                theRb.gravityScale = 0.15f;
            }
        }
        var birdObjects = GameObject.FindGameObjectsWithTag(MySonSonTags.Tags.Bird);
        foreach (var e in birdObjects)
        {
            var theRb = e.GetComponent<Rigidbody2D>();
            if (theRb)
            {
                theRb.bodyType = RigidbodyType2D.Dynamic;
                theRb.gravityScale = 0.04f;
                theRb.mass = 0.5f;
            }
        }
        TrAudio_Music.xInstance.zzPlayMain(0.1f);

    }


    #region Values

    /// <summary>
    /// Call when game is over. To save game process
    /// </summary>


    public void IncreaseCoindBy(int value)
    {
        coins += (value * 10);
        coinUI_Text.text = $" {coins}P";

    }
    public void IncreaseDodgedObstacleBy(int value)
    {
        dodged_obstacle += value;
        obstacleUI_Text.text = $" {dodged_obstacle}";

    }



    #endregion


    #region Pause

    public void PauseOn()
    {
        if (isPlay)
        {
            pauseUI.SetActive(true);
            isPlay = false;
            isWaitOnPause = true;
            Time.timeScale = 0f;
        }
    }

    public void PauseOff()
    {
        if (isWaitOnPause)
        {
            pauseUI.SetActive(false);
            isPlay = true;
            isWaitOnPause = false;
            Time.timeScale = 1f;
        }
    }
    public void OnRestart()
    {
        Time.timeScale = 1f;
        if (isWaitOnPause)
        {
            pauseUI.SetActive(false);
            isPlay = false;
            isWaitOnPause = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            /*
            var cg = fader.GetComponent<CanvasGroup>().DOFade(1, 2f).OnComplete(() => {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
            */

        }
    }
    public void OnToMain()
    {
        Time.timeScale = 1f;
        if (isWaitOnPause)
        {
            pauseUI.SetActive(false);
            isPlay = false;
            isWaitOnPause = true;
            var cg = fader.GetComponent<CanvasGroup>().DOFade(1, 2f).OnComplete(() =>
            {
                SceneManager.LoadScene(0);
            });

        }
    }

    #endregion
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
                gameData.currentObstacles = dodged_obstacle;
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
