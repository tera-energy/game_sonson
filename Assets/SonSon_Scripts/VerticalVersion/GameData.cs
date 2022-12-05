using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreInfo
{
    public string nickname { get; set; }

    public int score { get; set; }

}

[Serializable]
public class GameData 
{
    public int currentCoins { get; set; }
    public int currentObstacles { get; set; }

    public List<ScoreInfo> scoreList { get; set; }
   
}


