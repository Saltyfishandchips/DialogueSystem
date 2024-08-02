using System;
//using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using XNode;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics.Tracing;

public class FlowManager : MonoBehaviour
{
    public FlowGraph flowGraph;

    private BaseNode currentNode;
    private int soulTotalNum;

    public delegate void onExaminationEndDelegate();

    public GameObject settlementImageGo;
    public Sprite replaceImg;
    public Sprite originImg;
    public MoveImagesToCenter moveImagesToCenter;
    public TMP_Text[] tMP_Texts;
    public Button closeSceneBtn;
    public GameObject[] shouldBeHidden;

    // 特殊事件道具
    public GameObject[] speacialEventsGOs;
    public GameObject briberyPanel;
    private GameObject tempGO;

    public bool isDayOver;
    private void Start()
    {
        soulTotalNum = FlowDataManager.Instance.todayVisitorsNum;
        Debug.Log("soulNum " + soulTotalNum.ToString());
        StartFlow();
        isDayOver = false;
        DialogueManager.Instance.OnStoryEnd += onPreDialogueEnd;

        DialogueManager.Instance.OnStoryEnd += CheckSpecialEvent;

        AudioManager.Instance.PlayMusic("BackGround");
    }

    private void StartFlow()
    {
         // 查找标记为起始节点的节点
        foreach (Node node in flowGraph.nodes)
        {
            BaseNode baseNode = node as BaseNode;
            if (baseNode != null && baseNode.isStartNode)
            {
                currentNode = baseNode;
                break;
            }
        }
        // 如果没有找到起始节点，默认从第一个节点开始
        if (currentNode == null && flowGraph.nodes.Count > 0)
        {
            currentNode = flowGraph.nodes[0] as BaseNode;
        }

        if(currentNode is StartNode startNode)
        {   
            startNode.Execute();
        }

    }


    public void JumpToNode()
    {
        switch(currentNode)
        {
            case null:
                break;

            case StartNode:
                currentNode = currentNode.GetNextNode();
                Debug.Log("Jump to node:" + currentNode.name.ToString());
                break;

            case BellNode:
                currentNode = currentNode.GetNextNode();
                Destroy(tempGO);
                briberyPanel.SetActive(false);
                Debug.Log("Jump to node:" + currentNode.name.ToString());
                break;

            case AppearNode:
                currentNode = currentNode.GetNextNode();
                Debug.Log("Jump to node:" + currentNode.name.ToString());
                break;
            
            case SubmitNode:
                currentNode = currentNode.GetNextNode();
                Debug.Log("Jump to node:" + currentNode.name.ToString());
                break;

            case ExaminationNode:
                currentNode = currentNode.GetNextNode();
                Debug.Log("Jump to node:" + currentNode.name.ToString());
                break;
            
            case ResultNode:
                ResultNode customNode = currentNode as ResultNode;
                if(soulTotalNum > 1)
                {
                    soulTotalNum--;
                    currentNode = customNode.GetNextNode(false);
                }else
                {
                    currentNode = customNode.GetNextNode(true);
                    Debug.Log("over");
                    //FlowDataManager.Instance.day++;
                    DayManageInScene.dayCount++;
                    isDayOver = true;
                    //结算界面
                    onSettlementShow();
                }
                //currentNode = currentNode.GetNextNode();
                break;
        }
    }


    public void onJudgeGavelClick()
    {
        if(currentNode is StartNode startNode)
        {   
            JumpToNode();
            Debug.Log(currentNode.name.ToString());
        }
    }
    public void onSoulBellClick()
    {
        Debug.Log("soul bell btn");
        AudioManager.Instance.PlaySFX("Ring");
        if(currentNode is BellNode bellNode)
        {
            bellNode.Execute();
            JumpToNode();

            onDialouEnd();
        }
    }

    public void onDialouEnd()
    {
        if(currentNode is AppearNode appearNode)
        {
            appearNode.Execute();
            JumpToNode();
            onStorageBoxClick();
        }
    }

    public void onStorageBoxClick()
    {
        if(currentNode is SubmitNode submitNode)
        {
            submitNode.Execute();
            JumpToNode();
        }
    }

    public void CallExDelegate(onExaminationEndDelegate onExDelegate)
    {
        if (onExDelegate != null)
        {
            onExDelegate();
        }
    }
    
    public void onExaminationEnd()
    {
        if(currentNode is ExaminationNode examinationNode)
        {
            examinationNode.Execute();
            JumpToNode();

            onSettlement();
        }
    }

    public void onSettlement()
    {
        if(currentNode is ResultNode resultNode)
        {
            resultNode.Execute();
            JumpToNode();
        }
    }

    private void onPreDialogueEnd(object ob,EventArgs eventArgs)
    {
        // GameObject root = GameObject.Find("MainCanvas");
        
        // Transform DialogueCanvas = root.transform.Find("DialogueCanvas");
        // DialogueCanvas.gameObject.SetActive(false);
    }


    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        DialogueManager.Instance.OnStoryEnd -= onPreDialogueEnd;
        DialogueManager.Instance.OnStoryEnd -= CheckSpecialEvent;
    }

    public BaseNode getCurrentNode()
    {
        return currentNode;
    }

    public void onSettlementShow()
    {
        foreach(var go in shouldBeHidden)
        {
            go.SetActive(false);
        }
        settlementImageGo.SetActive(true);
        

        settlementImageGo.GetComponent<RectTransform>().SetAsLastSibling();

        // wait for a few seconds
        StartCoroutine(ChangeImageWithDelay(1.0f));
    

        // cloud move
        moveImagesToCenter.startMove();
        // Texts show
        StartCoroutine(TextsShow(2.0f));

        isDayOver = false;

    }

    IEnumerator ChangeImageWithDelay(float delay)
    {
        // 等待指定的秒数
        yield return new WaitForSeconds(delay);
        // 切换图片
        settlementImageGo.GetComponent<Image>().sprite = replaceImg;
        AudioManager.Instance.PlaySFX("Fan");
        closeSceneBtn.gameObject.SetActive(true);
    }

    IEnumerator ChangeStopImageWithDelay(float delay)
    {
        // 等待指定的秒数
        yield return new WaitForSeconds(delay);
        // 切换图片
        settlementImageGo.GetComponent<Image>().sprite = originImg;
    }

    public void OnSceneSkipBtnClicker()
    {
        AudioManager.Instance.PlaySFX("FanClose");

        foreach(var text in tMP_Texts)
        {
            text.gameObject.SetActive(false);
        }

        settlementImageGo.GetComponent<Image>().sprite = originImg;

        StartCoroutine(SceneChanged(1.0f));
    }

    IEnumerator TextsShow(float delay)
    {
        // 等待指定的秒数
        yield return new WaitForSeconds(delay);
      
        foreach(var text in tMP_Texts)
        {
            text.gameObject.SetActive(true);
        }
        tMP_Texts[2].text += FlowDataManager.Instance.fanyangNum.ToString();
        tMP_Texts[3].text += FlowDataManager.Instance.guiyinNum.ToString();
        tMP_Texts[4].text += FlowDataManager.Instance.trueNum.ToString();
        tMP_Texts[5].text += (FlowDataManager.Instance.trueNum * 50 + 888 * FlowDataManager.Instance.brieryNum).ToString();
    }
    
    IEnumerator SceneChanged(float delay)
    {
        // 等待指定的秒数
        yield return new WaitForSeconds(delay);

        DayManager.hasPassRookie = true;
        // to do
        SceneManager.LoadScene("YidianScene");
      
    }

    public void CheckSpecialEvent(object ob,EventArgs eventArgs)
    {
        if(FlowDataManager.Instance.currentNPC.Settings.specialEvent == NpcSpecialEvent.takeout && currentNode is ExaminationNode  ) 
        {
            // 放下外卖
            Instantiate(speacialEventsGOs[1],GameObject.Find("MainCanvas").GetComponent<RectTransform>());
            // 节点跳跃
            JumpToNode();
            onSettlement();
            GameObject.Find("SoulImage").GetComponent<Image>().color = new Color(0.0f,0.0f,0.0f,0.0f);
        }
        else if(FlowDataManager.Instance.currentNPC.Settings.specialEvent == NpcSpecialEvent.receiveTakeout && currentNode is ExaminationNode )
        {
            // 收下外卖

        }
        else if(FlowDataManager.Instance.currentNPC.Settings.specialEvent == NpcSpecialEvent.bribery && currentNode is ExaminationNode)
        {
            // 放下钱
            tempGO = Instantiate(speacialEventsGOs[0],GameObject.Find("MainCanvas").GetComponent<RectTransform>());
            briberyPanel.SetActive(true);
        }
    }
    
}
