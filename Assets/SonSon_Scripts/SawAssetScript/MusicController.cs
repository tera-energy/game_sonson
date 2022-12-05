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
class MusicController : MonoBehaviour
{
    //Components
    private static MusicController instance = null;
    public static MusicController Instance { get { return instance; } }

    //Values
    public AudioClip musicMenu;
    public AudioClip musicGame;

    #region Standart system methods

    void Awake()
    {
        //Return singlton objects
        if (MusicController.instance == null)
        {
            DontDestroyOnLoad(this);
            MusicController.instance = this;
            Play();
        }
        else 
            Destroy(this.gameObject);
    }

    #endregion

    public void Play()
    {
        if (CustomPlayerPrefs.GetBool("music", true))
            GetComponent<AudioSource>().Play();
        else
            GetComponent<AudioSource>().Pause();
    }

    public void PlayGameMusic()
    {
        if (CustomPlayerPrefs.GetBool("music", true))
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = musicGame;
            GetComponent<AudioSource>().Play();
        }
        else
            GetComponent<AudioSource>().Pause();
    }

    public void PlayMenuMusic()
    {
        if (CustomPlayerPrefs.GetBool("music", true))
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = musicMenu;
            GetComponent<AudioSource>().Play();
        }
        else
            GetComponent<AudioSource>().Pause();
    }
}