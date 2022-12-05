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

public class DestroyAfterTime : MonoBehaviour
{
    //Values
    public bool isOn = true;
    public float time = 0.4f;

    #region Standart system methods

    void Update()
    {
        ChangeTimer();
    }

    #endregion

    #region Work with timer

    /// <summary>
    /// Change timer for destroy current gameObject
    /// </summary>
    private void ChangeTimer()
    {
        time -= Time.deltaTime;

        if (time <= 0f)
            Destroy(gameObject);
    }

    #endregion
}
