using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YidianManager : MonoBehaviour
{
    [SerializeField] private GameObject GH1;
    [SerializeField] private GameObject GH2;
    [SerializeField] private GameObject GH3;
    [SerializeField] private GameObject Qingguangwang;
    [SerializeField] private GameObject Niutou;

    //GH1、GH2、GH3、牛头
    [SerializeField] private DialogueTrigger[] NPCLists;
    //Wall Left和Wall Right
    [SerializeField] private PlayerDialogueTrigger[] airWallLists;
    [SerializeField] private JudgeTrigger judgeTrigger;

    [SerializeField] private GameObject exitTrigger;
    [SerializeField] private GameObject judgeSceneJump;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        //
        if (DayManager.dayPass > 0) {
            GH2.SetActive(true);
            GH3.SetActive(true);
            Qingguangwang.SetActive(false);
            Niutou.SetActive(false);
            exitTrigger.SetActive(false);

            //TODO:之后需要配表
            //在GH1、GH2、GH3、牛头的对话
            NPCLists[0].inkJosn = Resources.Load<TextAsset>("Dialogues/yd_2/yd2_smalltalk1");

            judgeTrigger.textAsset = null;

            airWallLists[0].inkJosn = Resources.Load<TextAsset>("Dialogues/airwall/yd_airwall_after");
            airWallLists[1].inkJosn = Resources.Load<TextAsset>("Dialogues/airwall/yd_airwall_after");
        }
        else if (DayManager.dayPass == 0 && DayManager.hasPassRookie == true) { // 过完新手教程
            exitTrigger.SetActive(true);
            Qingguangwang.SetActive(false);
            GH1.SetActive(false);
            GH2.SetActive(false);
            GH3.SetActive(false);
            judgeSceneJump.SetActive(false);

            // 牛头更新对话
            NPCLists[3].inkJosn = Resources.Load<TextAsset>("Dialogues/yd_1/yd0_ntLeft");

            airWallLists[0].inkJosn = Resources.Load<TextAsset>("Dialogues/airwall/yd_airwall_after");
            airWallLists[1].inkJosn = Resources.Load<TextAsset>("Dialogues/airwall/yd_airwall_after");
        }
        else {
            Qingguangwang.SetActive(true);
            GH1.SetActive(true);
            GH2.SetActive(false);
            GH3.SetActive(false);
            exitTrigger.SetActive(false);
        }

    }
}
