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

public class GameValues : MonoBehaviour
{
    //Values
    public GameValue speedPlayer;
    public GameValue speedSaw;
    public GameValue speedSawGeneration;

    public int setCoinAfterEnemys = 5;

    #region Standart system methods

    private void Start()
    {
        speedPlayer.InitComponent();
        speedSaw.InitComponent();
        speedSawGeneration.InitComponent();
    }

    #endregion

    #region Values

    /// <summary>
    /// Call this for change all values
    /// </summary>
    public void ChangeValues()
    {
        speedPlayer.ChangeValue();
        speedSaw.ChangeValue();
        speedSawGeneration.ChangeValue();
    }

    #endregion
}
