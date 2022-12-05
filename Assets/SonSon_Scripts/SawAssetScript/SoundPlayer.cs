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

public class SoundPlayer : MonoBehaviour
{
    //Components
    private AudioSource audioSource;

    //Values
    private double timeToRemove = 999.9f;
    private bool tryToRemove = false;

    #region Standart system methods

    private void Awake()
    {
        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (tryToRemove)
            TryToRemove();
    }

    #endregion

    public void LoadAndPlay(AudioClip newClip)
    {
        audioSource.clip = newClip;
        audioSource.Play();

        timeToRemove = newClip.length;
        tryToRemove = true;
    }

    public void TryToRemove()
    {
        timeToRemove -= Time.deltaTime;
        if (timeToRemove < 0)
            Destroy(gameObject);
    }
}
