using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetect_HV : MonoBehaviour
{
    public static PlayerCollisionDetect_HV instance;

    [SerializeField]
    private GameController_HV gameController;

    [SerializeField] GameObject sonson_sprite_Obj;
    [SerializeField] GameObject sonson_dizzy_sprite_Obj;

    PlayerControl_HV playerControll;

    float bananaCombMaxTime = 2f;
    float currentBananaCombTime = 2f;
    float bananaCombTimeSpeed = 1f;
    public float bananaCombo = 1;

    [Header("world space canvas")]
    [SerializeField] Image healthbar;

    [Header("prefabs")]
    [SerializeField] GameObject banana;

    [Header("vibration option")]
    bool _isOnVibration;

    [Header("Effect UI")]
    [SerializeField] GameObject bananaEffectObj;
    [SerializeField] GameObject powerupEffectObj;
    [SerializeField] float eatEffectScaler = 1f;

    [SerializeField] GameObject bombParticleObj;
    [SerializeField] ParticleSystem bombParticleSystem;
    [SerializeField] ParticleSystem bombParticleFlashSystem;

    [Header("구름 맞고나서 나오는 이펙트 관련")]
    [SerializeField] GameObject afterHitEffectObj;
    [SerializeField] GameObject[] afterHitEffectPosition;
    float[] afterHitWaitforSerc = new float[] { 0f, 0.1f, 0.2f, 0.3f };


    private void Awake()
    {
        MakeInstance();
        playerControll = gameObject.GetComponent<PlayerControl_HV>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthbar.fillAmount = ((float)playerControll.currentHp) / ((float)playerControll.maxHp);
        currentBananaCombTime = bananaCombMaxTime;
        _isOnVibration = PlayerPrefs.GetInt(TT.strConfigVibrate, 1) == 1 ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        currentBananaCombTime = currentBananaCombTime + (Time.deltaTime * bananaCombTimeSpeed);
        keepEffectObjLeftRead();
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!PlayerControl_HV.instance.bAlive)
        {
            return;
        }
        switch (other.transform.tag)
        {
            case MySonSonTags.Tags.Banana:
                RaiseBananaPoint_With_Combo_calc();
                GameController_HV.instance.bananaCnt++;
                TrAudio_UI.xInstance.zzPlaySFX(GameController_HV.instance.eatBananaClip);

                /* banana ate effect */
                // StartCoroutine(nameof(ShowBananaEffect));

                var instPos = new Vector3(transform.position.x, transform.position.y + Random.Range(0.1f, 0.5f), transform.position.z);
                instPos.x += Random.Range(-0.5f, 0.5f);
                var _effect1 = GameObject.Instantiate(bananaEffectObj, instPos, Quaternion.identity);
                _effect1.SetActive(true);
                var leftOrRightRand = Random.Range(0, 2);
                if (leftOrRightRand == 0)
                {
                    _effect1.transform.Rotate(0f, 0f, 50f);
                }
                _effect1.transform.localScale = Vector3.one * eatEffectScaler;
                CanvasGroup canvasGroup = _effect1.GetComponent<CanvasGroup>();
                var sprites = _effect1.GetComponentsInChildren<SpriteRenderer>();
                var text1 = _effect1.GetComponentInChildren<TextMeshProUGUI>();
                text1.text = $"{GameController_HV.instance.bananaCnt}";

                canvasGroup.alpha = 1;
                //_effect1.transform.DOMove(new Vector3(0, 0.1f, 0), 1f);
                _effect1.transform.DOMoveY(3f, 1f);
                foreach (var e in sprites)
                {
                    e.DOFade(0, 1f);
                }
                canvasGroup.DOFade(0, 1.5f);
                Destroy(_effect1, 1.5f);
                /* banana ate effect END */

                break;
            case MySonSonTags.Tags.GreenBanana:
                if (playerControll.currentHp >= playerControll.maxHp)
                {
                    RaiseBananaPoint_With_Combo_calc();
                    GameController_HV.instance.bananaCnt++;
                }
                else
                {
                    playerControll.currentHp+=10;
                    playerControll.currentHp = Mathf.Clamp(playerControll.currentHp, 0, playerControll.maxHp);
                    healthbar.fillAmount = ((float)playerControll.currentHp) / ((float)playerControll.maxHp);
                }
                TrAudio_UI.xInstance.zzPlaySFX(GameController_HV.instance.eatBananaClip);

                /* banana ate effect */
                // StartCoroutine(nameof(ShowPowerupEffect));

                var instPos2 = new Vector3(transform.position.x, transform.position.y + Random.Range(0.1f, 0.2f), transform.position.z);
                instPos2.x += Random.Range(-0.1f, 0.1f);
                var _effect2 = GameObject.Instantiate(powerupEffectObj, instPos2, Quaternion.identity);
                _effect2.SetActive(true);
                var leftOrRightRand2 = Random.Range(0, 2);
                if (leftOrRightRand2 == 1)
                {
                    _effect2.transform.Rotate(0f, 0f, -60f);
                    _effect2.transform.position = new Vector3(_effect2.transform.position.x + 1.3f, _effect2.transform.position.y, _effect2.transform.position.z);
                }
                _effect2.transform.localScale = Vector3.one * eatEffectScaler;
                CanvasGroup canvasGroup2 = _effect2.GetComponent<CanvasGroup>();
                var sprites2 = _effect2.GetComponentsInChildren<SpriteRenderer>();

                canvasGroup2.alpha = 1;
                //_effect2.transform.DOMove(new Vector3(0, 0.1f, 0), 1f);
                _effect2.transform.DOMoveY(3f, 1f);
                foreach (var e in sprites2)
                {
                    e.DOFade(0, 1f);
                }
                canvasGroup2.DOFade(0, 1.5f);
                Destroy(_effect2, 1.5f);
                /* banana ate effect END */

                break;
            case MySonSonTags.Tags.RedBanana:

                PlayerControl_HV.instance.currentRedBananaTimer = 0;
                TrAudio_UI.xInstance.zzPlaySFX(GameController_HV.instance.eatBananaClip);
                var _pos = other.transform.position;
                _pos.x = _pos.x - 1f;
                /*
                bombParticleObj.transform.position= _pos;
                bombParticleSystem.Play();
                bombParticleFlashSystem.Play();
                */
                TrAudio_Music.xInstance.zzPlayRedBananaBGM();
                break;
            case MySonSonTags.Tags.Obstacle:

                if (PlayerControl_HV.instance.currentRedBananaTimer < PlayerControl_HV.instance.maxRedbananaTimer)
                {
                    return;
                }
                var afterHitEffectCnt = Random.Range(1, afterHitEffectPosition.Length);

                for (int i = 0; i < afterHitEffectCnt; i++)
                {
                    var pos = afterHitEffectPosition[i].transform.position;
                    var rot = afterHitEffectPosition[i].transform.rotation;
                    pos.x += Random.Range(-0.1f, 0.1f);
                    pos.y += Random.Range(-0.1f, 0.1f);

                    StartCoroutine(ShowAfterHitEffect(afterHitEffectPosition[i], afterHitWaitforSerc[i], pos, rot));
                }
                TrAudio_UI.xInstance.zzPlaySFX(GameController_HV.instance.hitByObstacleClip);
                //playerControll.currentHp--;
                var tempHp=(playerControll.currentHp * 0.1f)-0.1f;
                tempHp = (int)tempHp;
                tempHp *= 10;
                playerControll.currentHp = tempHp;
               
                playerControll.currentHp = Mathf.Clamp(playerControll.currentHp, 0, playerControll.maxHp);
                healthbar.fillAmount = ((float)playerControll.currentHp) / ((float)playerControll.maxHp);
                if (playerControll.currentHp <= 0)
                {
                    Kill();
                }

                if (_isOnVibration)
                {
                    Handheld.Vibrate();
                }
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!PlayerControl_HV.instance.bAlive)
        {
            return;
        }

        switch (other.transform.tag)
        {
            case MySonSonTags.Tags.Banana:
                RaiseBananaPoint_With_Combo_calc();
                GameController_HV.instance.bananaCnt++;
                TrAudio_UI.xInstance.zzPlaySFX(GameController_HV.instance.eatBananaClip);

                /* banana ate effect */
                // StartCoroutine(nameof(ShowBananaEffect));

                var instPos = new Vector3(transform.position.x, transform.position.y + Random.Range(0.1f, 0.5f), transform.position.z);
                instPos.x += Random.Range(-0.5f, 0.5f);
                var _effect1 = GameObject.Instantiate(bananaEffectObj, instPos, Quaternion.identity);
                _effect1.SetActive(true);
                var leftOrRightRand = Random.Range(0, 2);
                if (leftOrRightRand == 0)
                {
                    _effect1.transform.Rotate(0f, 0f, 50f);
                }
                _effect1.transform.localScale = Vector3.one * eatEffectScaler;
                CanvasGroup canvasGroup = _effect1.GetComponent<CanvasGroup>();
                var sprites = _effect1.GetComponentsInChildren<SpriteRenderer>();
                var text1 = _effect1.GetComponentInChildren<TextMeshProUGUI>();
                text1.text = $"{GameController_HV.instance.bananaCnt}";

                canvasGroup.alpha = 1;
                //_effect1.transform.DOMove(new Vector3(0, 0.1f, 0), 1f);
                _effect1.transform.DOMoveY(3f, 1f);
                foreach (var e in sprites)
                {
                    e.DOFade(0, 1f);
                }
                canvasGroup.DOFade(0, 1.5f);
                Destroy(_effect1, 1.5f);
                /* banana ate effect END */

                break;
            case MySonSonTags.Tags.GreenBanana:
                if (playerControll.currentHp >= playerControll.maxHp)
                {
                    RaiseBananaPoint_With_Combo_calc();
                    GameController_HV.instance.bananaCnt++;
                }
                else
                {
                    playerControll.currentHp++;
                    playerControll.currentHp = Mathf.Clamp(playerControll.currentHp, 0, playerControll.maxHp);
                    healthbar.fillAmount = ((float)playerControll.currentHp) / ((float)playerControll.maxHp);
                }
                TrAudio_UI.xInstance.zzPlaySFX(GameController_HV.instance.eatBananaClip);

                /* banana ate effect */
                // StartCoroutine(nameof(ShowPowerupEffect));

                var instPos2 = new Vector3(transform.position.x, transform.position.y + Random.Range(0.1f, 0.2f), transform.position.z);
                instPos2.x += Random.Range(-0.1f, 0.1f);
                var _effect2 = GameObject.Instantiate(powerupEffectObj, instPos2, Quaternion.identity);
                _effect2.SetActive(true);
                var leftOrRightRand2 = Random.Range(0, 2);
                if (leftOrRightRand2 == 1)
                {
                    _effect2.transform.Rotate(0f, 0f, -60f);
                    _effect2.transform.position = new Vector3(_effect2.transform.position.x + 1.3f, _effect2.transform.position.y, _effect2.transform.position.z);
                }
                _effect2.transform.localScale = Vector3.one * eatEffectScaler;
                CanvasGroup canvasGroup2 = _effect2.GetComponent<CanvasGroup>();
                var sprites2 = _effect2.GetComponentsInChildren<SpriteRenderer>();

                canvasGroup2.alpha = 1;
                //_effect2.transform.DOMove(new Vector3(0, 0.1f, 0), 1f);
                _effect2.transform.DOMoveY(3f, 1f);
                foreach (var e in sprites2)
                {
                    e.DOFade(0, 1f);
                }
                canvasGroup2.DOFade(0, 1.5f);
                Destroy(_effect2, 1.5f);
                /* banana ate effect END */

                break;
            case MySonSonTags.Tags.RedBanana:
                
                PlayerControl_HV.instance.currentRedBananaTimer = 0;
                TrAudio_UI.xInstance.zzPlaySFX(GameController_HV.instance.eatBananaClip);
                var _pos = other.transform.position;
                _pos.x = _pos.x - 1f;
                /*
                bombParticleObj.transform.position= _pos;
                bombParticleSystem.Play();
                bombParticleFlashSystem.Play();
                */
                TrAudio_Music.xInstance.zzPlayRedBananaBGM();
                break;
            case MySonSonTags.Tags.Obstacle:

                if (PlayerControl_HV.instance.currentRedBananaTimer < PlayerControl_HV.instance.maxRedbananaTimer)
                {
                    return;
                }
                var afterHitEffectCnt = Random.Range(1, afterHitEffectPosition.Length);

                for (int i = 0; i < afterHitEffectCnt; i++)
                {
                    var pos = afterHitEffectPosition[i].transform.position;
                    var rot = afterHitEffectPosition[i].transform.rotation;
                    pos.x += Random.Range(-0.1f, 0.1f);
                    pos.y += Random.Range(-0.1f, 0.1f);

                    StartCoroutine(ShowAfterHitEffect(afterHitEffectPosition[i], afterHitWaitforSerc[i], pos, rot));
                }
                TrAudio_UI.xInstance.zzPlaySFX(GameController_HV.instance.hitByObstacleClip);
                playerControll.currentHp--;
                playerControll.currentHp = Mathf.Clamp(playerControll.currentHp, 0, playerControll.maxHp);
                healthbar.fillAmount = ((float)playerControll.currentHp) / ((float)playerControll.maxHp);
                if (playerControll.currentHp <= 0)
                {
                    Kill();
                }

                if (_isOnVibration)
                {
                    Handheld.Vibrate();
                }
                break;
            default:
                break;
        }
    }
    void RaiseBananaPoint_With_Combo_calc()
    {
        if (currentBananaCombTime < bananaCombMaxTime)
        {
            //banana combo applied
            bananaCombo++;
            bananaCombo=Mathf.Clamp(bananaCombo, 0, 2);
        }
        else
        {
            bananaCombo = 1f;
        }
        GameController_HV.instance.IncreaseCoindBy((int)(1 * bananaCombo));
        currentBananaCombTime = 0;
    }

    public void Kill()
    {
        playerControll.bAlive = false;
        //particleSystemExplosion.Play();
        GameController_HV.instance.isPlay = false;
        if (sonson_dizzy_sprite_Obj)
        {
            sonson_sprite_Obj.SetActive(false);
            sonson_dizzy_sprite_Obj.SetActive(true);
        }
        GameManager._score = GameController_HV.instance.coins;
        /*var gameData = GameController_HV.instance.gameData;
        if (gameData != null)
        {
            GameController_HV.instance.SaveGameData();
        }*/
        StartCoroutine(nameof(ToScoreScene));
    }

    IEnumerator ToScoreScene()
    {
        yield return new WaitForSeconds(1f);
        GameController_HV.instance.gameoverUI.SetActive(true);
        GameController_HV.instance.gameOverImageUI.SetActive(true);
        GameController_HV.instance.gameOverImageUI.transform.DOMoveY(0.5f, 1f).SetLoops(-1, LoopType.Yoyo);
        TrAudio_UI.xInstance.zPlaySFX(clip: GameController_HV.instance._gameover, delay: 0.2f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);

    }
    IEnumerator ShowBananaEffect()
    {
        bananaEffectObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        bananaEffectObj.SetActive(false);
    }
    IEnumerator ShowPowerupEffect()
    {
        powerupEffectObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        powerupEffectObj.SetActive(false);
    }
    IEnumerator ShowAfterHitEffect(GameObject parent,float waitSeconds,Vector3 pos,Quaternion rot)
    {
        yield return new WaitForSeconds(waitSeconds);
        var obj=Instantiate(afterHitEffectObj, pos, rot);
        obj.transform.SetParent(parent.transform);
        Destroy(obj,3f);
    }
    void keepEffectObjLeftRead()
    {
        var tempSbanana = bananaEffectObj.transform.localScale;
        var tempSpowerup = powerupEffectObj.transform.localScale;
        if (PlayerControl_HV.instance.dir <= -1)
        {
            tempSbanana.x = Mathf.Abs(tempSbanana.x);
            tempSpowerup.x = Mathf.Abs(tempSpowerup.x);
        }
        if (PlayerControl_HV.instance.dir >= 1)
        {
            tempSbanana.x = Mathf.Abs(tempSbanana.x) * -1;
            tempSpowerup.x = Mathf.Abs(tempSpowerup.x) * -1;
        }
        bananaEffectObj.transform.localScale = tempSbanana;
        powerupEffectObj.transform.localScale = tempSpowerup;
    }
}
