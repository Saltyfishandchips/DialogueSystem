using UnityEngine;
using UnityEditor;
using System;
using XlsWork.NPCsXls;
using XlsWork;

[CustomEditor(typeof(NPCInfomations))]
public class NPCInfoEditor : Editor
{
    public override void OnInspectorGUI()//对UnitInfo在Inspector中的绘制方式进行接管
    {
        DrawDefaultInspector();//绘制常规内容
 
        if(GUILayout.Button("从配表ID刷新"))//添加按钮和功能——当组件上的按钮被按下时
        {
            //NPCInfomations npcInfo = (NPCInfomations)target;
            //Init(npcInfo);//令组件调用自身的InitSelf方法
        }
    }

    public void Init(NPCInfomations instance)
    {
        // Action init;
        // string str = "";
        // var dictionary = NPCXls.LoadExcelAsDictionary(str); // 调用读表方法并获取生成的字典

        // // 如果字典中没有查到所需的ID，说明表内没有相应ID的数据，报出异常
        // if (!dictionary.ContainsKey(instance.InitFromID))
        // {
        //     Debug.LogErrorFormat("未能在配表中找到指定的ID:{0}", instance.InitFromID);
        //     return;
        // }

        // IndividualData item = dictionary[instance.InitFromID]; // 如果字典中查到了所需的数据，则将该操作单元记录下来

        // // 将操作单元内的数据应用到自身
        // init = (() =>
        // {
        //     instance.Settings.npcId = item.ID;
        //     instance.Settings.npcName = item.Name;
        //     instance.Settings.npcIcon = item.IconName;
        //     instance.Settings.preDialogues = item.PreDialogues;
        //     instance.Settings.afterDialogues = item.AfterDialogues;
        //     instance.Settings.specialEvent = item.SpecialEvent;
        //     instance.Settings.correctResultHandle = item.CorrectResultHandle;
        //     instance.Settings.qgwDialogues = item.QgwDialogues;
        // });
        // init();

        // Debug.Log("NPC initialized: " + instance.Settings.npcName);
    }
}
