using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ExaminationListener : MonoBehaviour
{
    private FlowManager flowManagerInstance;
    public GraphicRaycaster graphicRaycaster;
    public EventSystem eventSystem;
    // 结算区域
    public RectTransform[] panelRectTransforms;
    // public RectTransform guiYinPanelRectTransform; //归阴panel
    // public RectTransform fanYangPanelRectTransform; //返阳panel
    // public RectTransform transferPanelRectTransform; //移交panel

    private FlowManager.onExaminationEndDelegate onExDelegate;

    private bool checkRes;
    private bool pointIn;
    public MouseIconChanger mouseIconChanger;

    // Start is called before the first frame update
    void Start()
    {
        flowManagerInstance = FindObjectOfType<FlowManager>();
        onExDelegate = new FlowManager.onExaminationEndDelegate(flowManagerInstance.onExaminationEnd);

        // DialogueManager.Instance.OnStoryEnd += BirdTalk;
        //pointIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForClick();
        }
    }

    void CheckForClick()
    {
        // 创建指针事件数据
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        // 存储射线检测结果
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        // 遍历射线检测结果
        foreach (RaycastResult result in results)
        {
            foreach(RectTransform rectTransform in panelRectTransforms)
            {
                if(FlowDataManager.Instance.isWrittingBrushOn == true)
                {
                    if (result.gameObject == rectTransform.gameObject && flowManagerInstance.getCurrentNode().name == "Examination")
                    {
                        NpcResult temp;
                        int choice;
                        switch(rectTransform.gameObject.name)
                        {
                            case "GuiYinPanel" :
                                temp = NpcResult.GuiYin;
                                choice = 2;
                                FlowDataManager.Instance.guiyinNum++;
                                break;
                            case "FanYangPanel" :
                                temp = NpcResult.FanYang;
                                choice = 1;
                                FlowDataManager.Instance.fanyangNum++;
                                break;
                            case "TransferPanel" :
                                temp = NpcResult.Transfer;
                                choice = 3;
                                break;
                            default:
                                temp = NpcResult.donothing;
                                choice = 4;
                                break;
                        }

                        //还原鼠标
                        mouseIconChanger.ResetMouseIcon();
                        checkRes = FlowDataManager.Instance.currentNPC.Settings.correctResultHandle.HasValue(temp);
                        AudioManager.Instance.PlaySFX("BrushDropdown");
                        FlowDataManager.Instance.isWrittingBrushOn = false;

                        // 选择后日谈
                        if(choice!=0)
                        {
                            GameObject root = GameObject.Find("MainCanvas");
                            Transform DialogueCanvas = root.transform.Find("DialogueCanvas");
                            DialogueCanvas.gameObject.SetActive(true);

                            int currentIndex = DialogueCanvas.GetSiblingIndex();
                            // 将物体移动到最后，确保它在最前面显示
                            DialogueCanvas.SetSiblingIndex(root.transform.childCount - 1);

                            string str = FlowDataManager.Instance.currentNPC.Settings.afterDialogues;

                            TextAsset textAsset = Resources.Load("Dialogues/after/" + str) as TextAsset;
                            DialogueManager.Instance.InitializedStroy(textAsset,choice);
                            AudioManager.Instance.PlaySFX("GhostShowup");
                        }

                        if(checkRes == true)
                        {
                            FlowDataManager.Instance.trueNum++;        
                        }
                        else
                        {
                            // GameObject root = GameObject.Find("MainCanvas");
            
                            // Transform DialogueCanvas = root.transform.Find("DialogueCanvas");
                            // DialogueCanvas.gameObject.SetActive(true);

                            // int currentIndex = DialogueCanvas.GetSiblingIndex();
                            // // 将物体移动到最后，确保它在最前面显示
                            // DialogueCanvas.SetSiblingIndex(root.transform.childCount - 1);

                            // string str = FlowDataManager.Instance.currentNPC.Settings.qgwDialogues;

                            // TextAsset textAsset = Resources.Load("Dialogues/qgw/" + str) as TextAsset;
                            // DialogueManager.Instance.InitializedStroy(textAsset,0);
                        }

                        GameObject imageObject = GameObject.Find("SoulImage");
                        imageObject.GetComponent<Image>().color = new Color(0,0,0,0);

                        Debug.Log("Clicked on the target panel!"+ checkRes);

                        // 在这里执行点击 Panel 后的逻辑
                        // if(FlowDataManager.Instance.isWrittingBrushOn == true)
                        // {
                        flowManagerInstance.CallExDelegate(onExDelegate);
                        // }
                        return;
                    }
                }  
            }
        }
    }

    private bool IsPointerInsidePanel(PointerEventData eventData) 
    {
        Vector2 localMousePosition;
        foreach(RectTransform rectTransform in panelRectTransforms)
        {
            localMousePosition = rectTransform.InverseTransformPoint(eventData.position);
            bool res = rectTransform.rect.Contains(localMousePosition);
            if(res == true)
            {
                return true;
            }
        }
        return false;
    }

    // private void BirdTalk(object ob,EventArgs eventArgs)
    // {
    //     if(flowManagerInstance.getCurrentNode().name == "Examination")
    //     {
    //         if(checkRes == true)
    //         {
    //             flowManagerInstance.JumpToNode();
    //             flowManagerInstance.onSettlement();
    //             Debug.Log("111");
    //         }
    //         else
    //         {
    //             GameObject root = GameObject.Find("MainCanvas");
                
    //             Transform DialogueCanvas = root.transform.Find("DialogueCanvas");
    //             DialogueCanvas.gameObject.SetActive(true);

    //             int currentIndex = DialogueCanvas.GetSiblingIndex();
    //             // 将物体移动到最后，确保它在最前面显示
    //             DialogueCanvas.SetSiblingIndex(root.transform.childCount - 1);

    //             string str = FlowDataManager.Instance.currentNPC.Settings.qgwDialogues;

    //             TextAsset textAsset = Resources.Load("Dialogues/qgw/" + str) as TextAsset;
    //             DialogueManager.Instance.InitializedStroy(textAsset,0);

    //             flowManagerInstance.JumpToNode();
    //             flowManagerInstance.onSettlement();
    //         }
    //     }
    //     else if(flowManagerInstance.getCurrentNode().name == "Appear"){
    //         flowManagerInstance.JumpToNode();
    //         flowManagerInstance.onStorageBoxClick();
    //     }
    // }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        // DialogueManager.Instance.OnStoryEnd -= BirdTalk;
    }
}
