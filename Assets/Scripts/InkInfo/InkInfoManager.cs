using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

public class InkInfoManager : MonoBehaviour
{   
    private TextAsset inkExcelJosn;
    private List<NPCInkJosnInfo> npcInkInfo;
    // 根据npc的名字配置其下面的所有ink信息。
    public Dictionary<Tuple<int, int>, NPCInkJosnInfo> npcInkDic = new Dictionary<Tuple<int, int>, NPCInkJosnInfo>();
    // 根据npc名字和阶段获取对应的Ink名称
    public Dictionary<Tuple<string, InkStage>, string> inkStageDic = new Dictionary<Tuple<string, InkStage>, string>();
    // 通过Ink名称获取InkStageInfo的相关信息
    public Dictionary<string, InkStageInfo> inkStageInfoDic = new Dictionary<string, InkStageInfo>();

    public static InkInfoManager Instance;

    private List<InkStage> inkStageLists = new List<InkStage> {InkStage.PreTalk
                                                                ,InkStage.ComparisonTable
                                                                ,InkStage.SpecialComparisonTable
                                                                ,InkStage.JudgeCompelete
                                                                ,InkStage.Trial
                                                                ,InkStage.Resurrection
                                                                ,InkStage.Evidence
                                                                ,InkStage.Bribe
                                                                ,InkStage.TrialCompelete};

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning("InkInfoManager has already exsited!");
        }
        Instance = this;


        inkExcelJosn = Resources.Load<TextAsset>(InkPath.inkExcelPath);
        
        npcInkInfo = JsonMapper.ToObject<List<NPCInkJosnInfo>>(inkExcelJosn.ToString());
        Debug.Log(npcInkInfo[0]);

        foreach (var npcInfo in npcInkInfo) {
            npcInkDic.Add(new Tuple<int, int>((int)npcInfo.Day, (int)npcInfo.Index), npcInfo);

            foreach (var inkStage in inkStageLists) {
                BuildInkStageInfo(npcInfo, inkStage);
            }     
        }

        // 
        // Debug.Log(inkStageInfoDic["D2_5_preDialogues"].npcName);
        // inkStageInfoDic["D1_4_preDialogues"].memberTriggerlist[2] = true;
        // Debug.Log(inkStageInfoDic["D1_4_preDialogues"].memberTriggerlist);
    }

    private void BuildInkStageInfo(NPCInkJosnInfo npcInfo, InkStage inkStage) {
        InkStageInfo inkStageInfo = new InkStageInfo{
            npcName = npcInfo.NpcName
        };

        // TODO: 之后有不同阶段的ink需要进行添加
        switch (inkStage) {
            case InkStage.PreTalk:
                if (npcInfo.PreTalk == null)
                    return;
                inkStageInfo.inkName = npcInfo.PreTalk;
                inkStageInfo.inkStage = InkStage.PreTalk;
                inkStageDic.Add(new Tuple<string, InkStage>(npcInfo.NpcName, InkStage.PreTalk), npcInfo.PreTalk);
                break;
            case InkStage.ComparisonTable:
                if (npcInfo.ComparisonTable == null)
                    return;
                inkStageInfo.canMutliTrigger = true;
                inkStageInfo.inkName = npcInfo.ComparisonTable;
                inkStageInfo.inkStage = InkStage.ComparisonTable;
                inkStageDic.Add(new Tuple<string, InkStage>(npcInfo.NpcName, InkStage.ComparisonTable), npcInfo.ComparisonTable);
                break;
            case InkStage.SpecialComparisonTable:
                if (npcInfo.SpecialComparisonTable == null)
                    return;
                inkStageInfo.canMutliTrigger = true;
                inkStageInfo.inkName = npcInfo.SpecialComparisonTable;
                inkStageInfo.inkStage = InkStage.SpecialComparisonTable;
                inkStageDic.Add(new Tuple<string, InkStage>(npcInfo.NpcName, InkStage.SpecialComparisonTable), npcInfo.SpecialComparisonTable);
                break;
            case InkStage.JudgeCompelete:
                if (npcInfo.JudgeCompelete == null)
                    return;
                inkStageInfo.inkName = npcInfo.JudgeCompelete;
                inkStageInfo.inkStage = InkStage.JudgeCompelete;
                inkStageDic.Add(new Tuple<string, InkStage>(npcInfo.NpcName, InkStage.JudgeCompelete), npcInfo.JudgeCompelete);
                break;
            case InkStage.Trial:
                if (npcInfo.Trial == null)
                    return;
                inkStageInfo.inkName = npcInfo.Trial;
                inkStageInfo.inkStage = InkStage.Trial;
                inkStageDic.Add(new Tuple<string, InkStage>(npcInfo.NpcName, InkStage.Trial), npcInfo.Trial);
                break;
            case InkStage.Resurrection:
                if (npcInfo.Resurrection == null)
                    return;
                inkStageInfo.inkName = npcInfo.Resurrection;
                inkStageInfo.inkStage = InkStage.Resurrection;
                inkStageDic.Add(new Tuple<string, InkStage>(npcInfo.NpcName, InkStage.Trial), npcInfo.Resurrection);
                break;    
            case InkStage.Evidence:
                if (npcInfo.Evidence == null)
                    return;
                inkStageInfo.canMutliTrigger = true;
                inkStageInfo.inkName = npcInfo.Evidence;
                inkStageInfo.inkStage = InkStage.Evidence;
                inkStageDic.Add(new Tuple<string, InkStage>(npcInfo.NpcName, InkStage.Evidence), npcInfo.Evidence);
                break;
            case InkStage.Bribe:
                if (npcInfo.Bribe == null)
                    return;
                inkStageInfo.inkName = npcInfo.Bribe;
                inkStageInfo.inkStage = InkStage.Bribe;
                inkStageDic.Add(new Tuple<string, InkStage>(npcInfo.NpcName, InkStage.Bribe), npcInfo.Bribe);
                break;
            case InkStage.TrialCompelete:
                if (npcInfo.TrialCompelete == null)
                    return;
                inkStageInfo.inkName = npcInfo.TrialCompelete;
                inkStageInfo.inkStage = InkStage.TrialCompelete;
                inkStageDic.Add(new Tuple<string, InkStage>(npcInfo.NpcName, InkStage.TrialCompelete), npcInfo.TrialCompelete);
                break;
            default:
                Debug.LogWarning("ink不属于现有任意阶段");
                break;
        }

        inkStageInfoDic.Add(inkStageInfo.inkName, inkStageInfo);
    }

    // 返回当前天数，对应序号npc的所有ink文件名
    public NPCInkJosnInfo CheckNPCInfo(int day, int index) {
        Tuple<int, int> key = new Tuple<int, int>(day, index);
        if (npcInkDic.ContainsKey(key)) {
            return npcInkDic[key];
        }
        else {
            return null;
        }
    }

    // 返回对应npc名字，对应阶段的ink文件名
    public string NPCinkStageInfo(string npcName, InkStage inkStage) {
        Tuple<string, InkStage> key = new Tuple<string, InkStage>(npcName, inkStage);

        if (inkStageDic.ContainsKey(key)) {
            return inkStageDic[key];
        }
        else {
            Debug.LogWarning("该NPC:" + npcName +" 不存在状态在" + inkStage + "的Ink文件");
            return null;
        }
    }

    // 返回某个ink文件的InkStageInfo，其中包含该ink的各类信息
    public InkStageInfo CheckInkStageInfo(string inkName) {
        if (inkStageInfoDic.ContainsKey(inkName)) {
            return inkStageInfoDic[inkName];
        }
        else {
            Debug.LogWarning(inkName +" 不存在InkStageInfo");
            return null;
        }
    }
    
}
