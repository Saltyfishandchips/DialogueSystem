using UnityEngine;
using System;
using XlsWork.TravelPermitsXls;
using XlsWork;

public class TravelPermitInfo
{
    public TravelPermitSettings Settings = new TravelPermitSettings();
}

public class TravelPermitSettings
{
    public string npcID; 
    public string lyName;

    public bool npcGender;
    public string npcBrithDay;
    public string npcDeadDay; 
    public string npcCause ; 
    public string lyStaffNo;

    public static implicit operator TravelPermitSettings(TravelPermitData item)
    {
        return new TravelPermitSettings 
        {
            npcID = item.npcID,
            lyName = item.lyName,
            npcGender = item.npcGender,
            npcBrithDay = item.npcBrithDay,
            npcDeadDay = item.npcDeadDay,
            npcCause = item.npcCause,
            lyStaffNo = item.lyStaffNo,
        };
    } 

    public void PrintLog()
    {
        //Debug.Log("dayId:" + dayId.ToString());
    } 

}
