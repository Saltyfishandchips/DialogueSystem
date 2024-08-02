using System;
 
namespace XlsWork
{
    public class IndividualData
    {
        public string ID {get;set;} 
        public string Name {get;set;} 
        public string IconName {get;set;} 
        public string PreDialogues{get;set;} 
        public NpcSpecialEvent SpecialEvent{get;set;} 
        public NpcResultHandle CorrectResultHandle{get;set;}
        public string AfterDialogues{get;set;} 
        public string QgwDialogues{get;set;}  

        public IndividualData(string _id, string _name, string _iconName,string _preDialogues,
                            NpcSpecialEvent _specialEvent,NpcResultHandle _npcResultHandle,string _afterDialogues,string _qgwDialogues)
        {
            ID = _id;
            Name = _name;
            IconName = _iconName;
            PreDialogues = _preDialogues;
            SpecialEvent = _specialEvent;
            CorrectResultHandle = _npcResultHandle;
            AfterDialogues =_afterDialogues;
            QgwDialogues =_qgwDialogues;
        }
    }

    public class DailyData
    {
        public int dayId {get;set;} 
        public string newsATitle {get;set;} 
        public string newsA {get;set;} 
        public string newsBTitle {get;set;} 
        public string newsB {get;set;} 
        public string newsCTitle {get;set;}
        public string newsC {get;set;}

        public DailyData(int _id, string _newsATitle, string _newsA, string _newsBTitle, string _newsB, string _newsCTitle, string _newsC)
        {
            dayId =  _id;
            newsATitle = _newsATitle;
            newsA = _newsA;
            newsBTitle = _newsBTitle;
            newsB = _newsB;
            newsCTitle = _newsCTitle;
            newsC = _newsC;
        }
    }

    public class ObituaryData
    {
        public string npcID {get;set;} 
        public string ssbName {get;set;} 
        public string npcTOB {get;set;} 
        public string npcTOD {get;set;} 
        public string npcDescription {get;set;} 
        public string ssbStaffNo {get;set;}

        public ObituaryData(string _npcID, string _ssbName, string _npcTOB, string _npcTOD, string _npcDescription, string _ssbStaffNo)
        {
            npcID = _npcID;
            ssbName = _ssbName;
            npcTOB = _npcTOB;
            npcTOD = _npcTOD;
            npcDescription = _npcDescription;
            ssbStaffNo = _ssbStaffNo;
        }
    }

    public class TravelPermitData
    {
        public string npcID {get;set;} 
        public string lyName {get;set;} 

        public bool npcGender {get;set;}
        public string npcBrithDay {get;set;} 
        public string npcDeadDay {get;set;} 
        public string npcCause {get;set;} 
        public string lyStaffNo {get;set;}

        public TravelPermitData(string _npcID, string _lyName, bool _npcGender, string _npcBrithDay,
                                string _npcDeadDay, string _npcCause, string _lyStaffNo)
        {
            npcID = _npcID;
            lyName = _lyName;
            npcGender = _npcGender;
            npcBrithDay = _npcBrithDay;
            npcDeadDay = _npcDeadDay;
            npcCause = _npcCause;
            lyStaffNo = _lyStaffNo;
        }
    }

}