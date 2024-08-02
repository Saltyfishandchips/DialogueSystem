using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public List<GameObject> groupFirst; // 需要隐藏的 UI 组
    public List<GameObject> groupSecond; // 需要显示的 UI 组
    //public Animator cutsceneAnimator; // 过场动画的 Animator

    public GameObject hellDaily;
    public BackgroundAnimator backgroundAnimatorInstance;

    private void Start()
    {
        // 确保 GroupB 初始状态是隐藏的
        foreach(GameObject go in groupSecond)
        {
            go.SetActive(false);
        }

        foreach(GameObject go in groupFirst)
        {
            go.SetActive(true);
        }

        if(FlowDataManager.Instance.day == 1)
        {
            groupFirst[0].SetActive(false);//地府小报
        }
    }

    // private void Update() 
    // {
    //     // if (Input.GetKeyDown(KeyCode.Space))
    //     // {
    //     //     OnCutsceneEnd();
    //     // } 
    // }

    // 在动画结束时调用的方法
    public void OnCutsceneEnd()
    {
        // 隐藏 GroupA
        foreach(GameObject go in groupFirst)
        {
            go.SetActive(false);
        }

        //播放动画
        backgroundAnimatorInstance.PlayAnimation();

        // 显示 GroupB
        Invoke("showGroupB", 1.3f);
    }

    public void onHellDailyButtonClick()
    {
        hellDaily.SetActive(true);
    }

    public void onCloseButton()
    {
        hellDaily.SetActive(false);
    }

    private void showGroupB()
    {
        // 显示 GroupB
        foreach(GameObject go in groupSecond)
        {
            go.SetActive(true);
        }

        if(FlowDataManager.Instance.day == 1)
        {
            groupSecond[4].SetActive(false);
            groupSecond[11].SetActive(false);
        }

        groupSecond[0].SetActive(false);
    }
}
