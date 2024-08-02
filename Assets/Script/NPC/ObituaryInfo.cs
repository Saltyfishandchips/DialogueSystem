using UnityEngine;
using System;
using XlsWork.ObituariesXls;
using XlsWork;

public class ObituaryInfo
{
    public ObituarySettings Settings = new ObituarySettings();
}

public class ObituarySettings
{
    public string npcID {get;set;} 
    public string ssbName {get;set;} 
    public string npcTOB {get;set;} 
    public string npcTOD {get;set;} 
    public string npcDescription {get;set;} 
    public string ssbStaffNo {get;set;}

    public static implicit operator ObituarySettings(ObituaryData item)
    {
        return new ObituarySettings
        {
            
            npcID = item.npcID,
            ssbName = item.ssbName,
            npcTOB = item.npcTOB,
            npcTOD = item.npcTOD,
            npcDescription = item.npcDescription,
            ssbStaffNo = item.ssbStaffNo,
        };
    } 

    public void PrintLog()
    {
        //
    } 

}
