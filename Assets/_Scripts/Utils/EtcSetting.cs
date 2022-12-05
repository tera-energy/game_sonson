using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtcSetting
{

    public static string IMODE = "local";
    public static string API_URL
    {
        get
        {
            if (IMODE.ToLower() == "local")
            {
                return "http://118.67.143.233:8080";
            }
            else if (IMODE.ToLower() == "semi_local")
            {
                return "http://192.168.0.171:3003";
            }
            else
            {
                return "http://118.67.143.233:8080";
            }
        }
    }
}
