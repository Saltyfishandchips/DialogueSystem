using UnityEngine;
using System;
using XlsWork.DailyXls;
using XlsWork;

public class DailyInfo
{
    public DailySettings Settings = new DailySettings();
}

public class DailySettings
{
    public int dayId; 
    public string newsATitle;
    public string newsA; 
    public string newsBTitle ;
    public string newsB;
    public string newsCTitle;
    public string newsC;

    public static implicit operator DailySettings(DailyData item)
    {
        return new DailySettings {
            dayId = item.dayId,
            newsATitle = item.newsATitle,
            newsA = item.newsA,
            newsBTitle = item.newsBTitle,
            newsB = item.newsB,
            newsCTitle = item.newsCTitle,
            newsC = item.newsC,
        };
    } 

    public void PrintLog()
    {
        Debug.Log("dayId:" + dayId.ToString());
    } 

}
