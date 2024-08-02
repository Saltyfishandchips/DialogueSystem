using UnityEngine;
using System;
using XlsWork.NPCsXls;
using XlsWork;

public class NPCInfomations 
{
    public NPCSettings Settings = new NPCSettings();

    [Header("配表内ID")]
    public int InitFromID;

}

[Serializable]
public class NPCSettings
{
    public string npcId; //npc编号
    public string npcName; //npc名称
    public string npcIcon; //npc立绘对应的文件名

    public string preDialogues; //对话

    public NpcSpecialEvent specialEvent; // 特殊事件
    public NpcResultHandle correctResultHandle; //预设结局
    public string afterDialogues; //后日谈
    public string qgwDialogues; //秦广王对话

    public static implicit operator NPCSettings(IndividualData item)
    {
        return new NPCSettings {
            npcId = item.ID,
            npcIcon = item.IconName,
            npcName = item.Name,
            preDialogues = item.PreDialogues,
            afterDialogues = item.AfterDialogues,
            qgwDialogues = item.QgwDialogues,
            specialEvent = item.SpecialEvent,
            correctResultHandle = item.CorrectResultHandle,
        };
    } 

    public void PrintLog()
    {
        Debug.Log("npcName:" + npcName.ToString());
    } 
}

public enum NpcSpecialEvent
{
    none = -1 ,
    nothing = 0, // 无事发生
    bribery = 1, // 贿赂
    takeout = 2, // 外卖
    receiveTakeout = 3, //收外卖 

}

public enum NpcResult
{
    FanYang = 1, //返阳
    GuiYin = 2, // 归阴
    Transfer = 4, //移交

    donothing = 0, //不处理
}

public class NpcResultHandle
{
    public NpcResult presetValues;

    public void SetValues(NpcResult values)
    {
        presetValues |= values;
    }

    public void PrintPresetValues()
    {
        Console.WriteLine("Preset Values: " + presetValues);
    }

    public bool HasValue(NpcResult value)
    {
        return (presetValues & value) == value;
    }
}

public static class EnumConverter
{
    public static NpcSpecialEvent StringToSpecialEvent(string value)
    {
        try
        {
            return (NpcSpecialEvent)Enum.Parse(typeof(NpcSpecialEvent), value, true);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid string for enum conversion: " + value);
            return NpcSpecialEvent.none; // 返回一个默认值
        }
    }

    public static NpcResultHandle StringToNpcResultHandle(string value)
    {

        string[] strs = value.Split("or");
        try
        {   
            NpcResultHandle res = new NpcResultHandle();
            foreach(string str in strs)
            {
                NpcResult temp = (NpcResult)Enum.Parse(typeof(NpcResult), str, true);
                res.SetValues(temp);
            }
            return res;
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid string for enum conversion: " + value);
            return null; 
        }
    }
}
