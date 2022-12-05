/* 
 *       _______             _____ __            ___     
 *      /_  __(_)___  __  __/ ___// /___  ______/ (_)___ 
 *       / / / / __ \/ / / /\__ \/ __/ / / / __  / / __ \
 *      / / / / / / / /_/ /___/ / /_/ /_/ / /_/ / / /_/ /
 *     /_/ /_/_/ /_/\__, //____/\__/\__,_/\__,_/_/\____/ 
 *                 /____/                                
 *
 *      Created by Alice Vinnik in 2022.
 * 
 *      If you want to reskin, customization or development contact me. Im available to hire.
 *      Website: https://alicevinnik.wixsite.com/tinystudio
 *      Email: tinystudio.main@gmail.com
 *      
 *      Thanks for buying my codes.
 *      Have a nice day!
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
class SoundsController : MonoBehaviour
{
    //Components
    private static SoundsController instance = null;
    public static SoundsController Instance { get { return instance; } }

    public GameObject soundPlayer;

    public AudioClip[] clipsToPlay;

    //Possible values
    [System.Serializable]
    public enum SoundName
    {
        ButtonClick = 0,
        Switch = 1,
        Coin = 2,
        Point = 3,
        Smash1 = 4,
        Smash2 = 5
    }

    #region Standart system methods

    void Awake()
    {
        //Singleton object return
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    #endregion

    public void Play(SoundName soundName)
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            GameObject newPlayer = Instantiate(soundPlayer);
            newPlayer.GetComponent<SoundPlayer>().LoadAndPlay(clipsToPlay[(int)soundName]);
        }
    }
}