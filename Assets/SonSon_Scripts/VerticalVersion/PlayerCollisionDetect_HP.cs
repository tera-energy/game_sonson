using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class PlayerCollisionDetect_HP : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystemExplosion;
    private GameController gameController;

    [SerializeField] GameObject sonson_sprite_Obj;
    [SerializeField] GameObject sonson_dizzy_sprite_Obj;

    PlayerControll_KeepDirection playerControll;

    float bananaCombMaxTime = 2f;
    float currentBananaCombTime = 2f;
    float bananaCombTimeSpeed = 2f;
    public float bananaCombo = 1;

    [Header("world space canvas")]
    [SerializeField] Transform playerWS_CanvasTransform;
    [SerializeField] Image healthbar;

    [Header("prefabs")]
    [SerializeField] GameObject banana;

    [Header("vibration option")]
    bool _isOnVibration;

    [Header("Effect UI")]
    [SerializeField] TextMeshProUGUI bananaCntText;
    [SerializeField] GameObject bananaEffectObj;
    [SerializeField] GameObject powerupEffectObj;
    [SerializeField] Transform bananaEffectPos;
    [SerializeField] Transform powerupEffectPos;

    [SerializeField] GameObject bombParticleObj;
    [SerializeField] ParticleSystem bombParticleSystem;
    [SerializeField] ParticleSystem bombParticleFlashSystem;
    private void Awake()
    {
        playerControll = gameObject.GetComponent<PlayerControll_KeepDirection>();
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
    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {
        playerWS_CanvasTransform.LookAt(transform.position + Camera.main.transform.forward);
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!playerControll.bAlive)
        {
            return;
        }
        switch (other.transform.tag)
        {
            case MySonSonTags.Tags.Banana:
                RaiseBananaPoint_With_Combo_calc();
                GameController.instance.bananaCnt++;
                bananaCntText.text = $"{GameController.instance.bananaCnt}";
                TrAudio_UI.xInstance.zzPlaySFX(GameController.instance.eatBananaClip);

                /* banana ate effect */
                // StartCoroutine(nameof(ShowBananaEffect));

                var instPos = transform.position;
                instPos.x += Random.Range(-0.5f, 0.5f);
                var _effect1 = GameObject.Instantiate(bananaEffectObj, instPos, Quaternion.identity);
                _effect1.SetActive(true);
                var leftOrRightRand = Random.Range(0, 2);
                if (leftOrRightRand == 0)
                {
                    _effect1.transform.Rotate(0f, 0f, 80f);
                }
                _effect1.transform.localScale = Vector3.one * 0.6f;
                CanvasGroup canvasGroup = _effect1.GetComponent<CanvasGroup>();
                var sprites = _effect1.GetComponentsInChildren<SpriteRenderer>();
                var text1 = _effect1.GetComponentInChildren<TextMeshProUGUI>();
                text1.text = $"{GameController.instance.bananaCnt}";

                canvasGroup.alpha = 1;
                _effect1.transform.DOMove(new Vector3(0, 0.1f, 0), 1f);
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
                    GameController.instance.bananaCnt++;
                    bananaCntText.text = $"{GameController.instance.bananaCnt}";
                }
                else
                {
                    playerControll.currentHp++;
                    Mathf.Clamp(playerControll.currentHp, 0, playerControll.maxHp);
                    healthbar.fillAmount = ((float)playerControll.currentHp) / ((float)playerControll.maxHp);
                }
                TrAudio_UI.xInstance.zzPlaySFX(GameController.instance.eatBananaClip);

                /* banana ate effect */
                // StartCoroutine(nameof(ShowPowerupEffect));

                var instPos2 = transform.position;
                instPos2.x += Random.Range(-0.1f, 0.1f);
                var _effect2 = GameObject.Instantiate(powerupEffectObj, instPos2, Quaternion.identity);
                _effect2.SetActive(true);
                var leftOrRightRand2 = Random.Range(0, 2);
                if (leftOrRightRand2 == 1)
                {
                    _effect2.transform.Rotate(0f, 0f, -60f);
                    _effect2.transform.position = new Vector3(_effect2.transform.position.x + 1.3f, _effect2.transform.position.y, _effect2.transform.position.z);
                }
                _effect2.transform.localScale = Vector3.one * 0.6f;
                CanvasGroup canvasGroup2 = _effect2.GetComponent<CanvasGroup>();
                var sprites2 = _effect2.GetComponentsInChildren<SpriteRenderer>();

                canvasGroup2.alpha = 1;
                _effect2.transform.DOMove(new Vector3(0, 0.1f, 0), 1f);
                foreach (var e in sprites2)
                {
                    e.DOFade(0, 1f);
                }
                canvasGroup2.DOFade(0, 1.5f);
                Destroy(_effect2, 1.5f);
                /* banana ate effect END */

                break;
            case MySonSonTags.Tags.RedBanana:
                if (PlayerControll_KeepDirection.instance.redBannaMode == RedBanaMode.more_banana)
                {
                    ChangeObstaclesToBanana();
                }
                PlayerControll_KeepDirection.instance.currentRedBananaTimer = 0;
                TrAudio_UI.xInstance.zzPlaySFX(GameController.instance.eatBananaClip);
                var _pos = other.transform.position;
                _pos.y = _pos.y + 2f;
                //bombParticleObj.transform.position= _pos;
                bombParticleSystem.Play();
                bombParticleFlashSystem.Play();
                break;
            case MySonSonTags.Tags.Obstacle:
                if (!PlayerControll_KeepDirection.instance.bAlive)
                {
                    return;
                }
                TrAudio_UI.xInstance.zzPlaySFX(GameController.instance.hitByObstacleClip);
                playerControll.currentHp--;
                Mathf.Clamp(playerControll.currentHp, 0, playerControll.maxHp);
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
            Mathf.Clamp(bananaCombo, 0, 20);
        }
        else
        {
            bananaCombo = 1f;
        }
        GameController.instance.IncreaseCoindBy((int)(1 * bananaCombo));
        currentBananaCombTime = 0;
    }
    void ChangeObstaclesToBanana()
    {
        var obstacles = GameObject.FindGameObjectsWithTag(MySonSonTags.Tags.Obstacle);
        foreach (var obstacle in obstacles)
        {
            var obstaclePos = obstacle.transform.position;
            Destroy(obstacle);
            GameObject.Instantiate(banana, obstaclePos, Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
        }
    }

    void Kill()
    {
        playerControll.bAlive = false;
        particleSystemExplosion.Play();
        GameController.instance.isPlay = false;
        if (sonson_dizzy_sprite_Obj)
        {
            sonson_sprite_Obj.SetActive(false);
            sonson_dizzy_sprite_Obj.SetActive(true);
        }
        var gameData = GameController.instance.gameData;
        if (gameData != null)
        {
            GameController.instance.SaveGameData();
        }
        StartCoroutine(nameof(ToScoreScene));
    }

    IEnumerator ToScoreScene()
    {
        yield return new WaitForSeconds(1f);
        GameController.instance.gameoverUI.SetActive(true);
        GameController.instance.gameOverImageUI.SetActive(true);
        GameController.instance.gameOverImageUI.transform.DOMoveY(0.5f, 1f).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Result");

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
    void keepEffectObjLeftRead()
    {
        var tempSbanana = bananaEffectObj.transform.localScale;
        var tempSpowerup = powerupEffectObj.transform.localScale;
        if (PlayerControll_KeepDirection.instance.dir <= -1)
        {
            tempSbanana.x = Mathf.Abs(tempSbanana.x);
            tempSpowerup.x = Mathf.Abs(tempSpowerup.x);
        }
        if (PlayerControll_KeepDirection.instance.dir >= 1)
        {
            tempSbanana.x = Mathf.Abs(tempSbanana.x) * -1;
            tempSpowerup.x = Mathf.Abs(tempSpowerup.x) * -1;
        }
        bananaEffectObj.transform.localScale = tempSbanana;
        powerupEffectObj.transform.localScale = tempSpowerup;
    }

}
