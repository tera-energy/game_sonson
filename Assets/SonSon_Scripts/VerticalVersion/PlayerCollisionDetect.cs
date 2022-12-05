using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystemExplosion;
    private GameController gameController;

    [SerializeField] Sprite sonson_dizzy_sprite;

    PlayerControll playerControll;

    private void Awake()
    {
        playerControll= gameObject.GetComponent<PlayerControll>();
        //gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case MySonSonTags.Tags.Banana:
                GameController.instance.IncreaseCoindBy(1);
                TrAudio_UI.xInstance.zzPlay_ClickButtonNormal();
                break;
            case MySonSonTags.Tags.Obstacle:
                Kill();

                break;
            default:
                break;
        }
    }

    void Kill()
    {
        playerControll.bAlive = false;
        particleSystemExplosion.Play();
        GameController.instance.isPlay = false;
        if (sonson_dizzy_sprite)
        {
            var spriteRenderer= gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sonson_dizzy_sprite;
        }
        var gameData = GameController.instance.gameData;
        if (gameData!=null)
        {
            GameController.instance.SaveGameData();
        }
        StartCoroutine(nameof(ToScoreScene));
    }

    IEnumerator ToScoreScene()
    {
        yield return new WaitForSeconds(1f);
        GameController.instance.gameoverUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Result");
        
    }

}
