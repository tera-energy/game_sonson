using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    [SerializeField] UI_StatsRadarChart uistatsRadarChart;
    void Start()
    {
        zSetStats(60, 50, 70, 50, 90);
    }

    /// <summary>
    /// ����, ���߷�, ����, ���߷�, �����ذ�ɷ�
    /// </summary>
    public void zSetStats(int mem, int con, int tho, int qui, int pro){
        Stats stats = new Stats(mem, con, tho, qui, pro);

        uistatsRadarChart.zSetStats(stats);
    }
}
