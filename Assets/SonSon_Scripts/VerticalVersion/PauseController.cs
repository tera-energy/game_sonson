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

public class PauseController : MonoBehaviour
{
    //Components
    [SerializeField]
    private GameObject canvasPause;
    private GameController gameController;

    #region Standart system methods

    void Start()
    {
        //Get components
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        canvasPause.SetActive(false);
    }

    #endregion

    #region Pause logic

    public void PauseBegin()
    {
        PauseCanvasShow();
        Time.timeScale = 0;
        gameController.PauseOn();
    }

    public void PauseEnd()
    {
        PauseCanvasHide();
        Time.timeScale = 1;
        gameController.PauseOff();
    }

    public void SetTimeScaleDefault()
    {
        Time.timeScale = 1;
    }

    #endregion

    #region Interface

    public void PauseCanvasShow()
    {
        canvasPause.SetActive(true);
    }

    public void PauseCanvasHide()
    {
        canvasPause.SetActive(false);
    }

    #endregion
}
