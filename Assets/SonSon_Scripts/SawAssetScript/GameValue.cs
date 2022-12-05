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

[System.Serializable]
public class GameValue
{
    //Values
    public float value = 2f;
    public float valueMax = 4f;
    [Range(0f, 100f)]
    public float valueChangeByPercent = 10f;
    private bool isIncrease;
    private bool isActive = true;

    #region Work with values

    /// <summary>
    /// Call this for initialise some value setting. Call before use
    /// </summary>
    public void InitComponent()
    {
        //Check is increase type
        isIncrease = value < valueMax;
    }

    /// <summary>
    /// Change value.
    /// Value will be automatically increase or decrease based by calculation of value and valueMax.
    /// </summary>
    public void ChangeValue()
    {
        if (isActive)
        {
            float changeBy = (value / 100) * valueChangeByPercent;

            value += isIncrease ? changeBy : -changeBy;
            if (isIncrease ? value >= valueMax : value <= valueMax)
                StopActivity();
        }
    }

    /// <summary>
    /// Stop activity. When you call this you lock all future increasing of values.
    /// </summary>
    private void StopActivity()
    {
        isActive = false;
        value = valueMax;
    }

    #endregion
}
