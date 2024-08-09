using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
// using Ink.UnityIntegration;
using System;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    private Story currentStory;
    [SerializeField] private ChatPanelManager chatPanelManager;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject buttonObject;
    private const string TAG_LAYOUT = "Layout";
    private const string TAG_CHOICEEFFECT = "ChoiceEffect";
    [SerializeField] private float duration;
    private bool isLeft = false;
    private bool isRight = false;
    private bool isDialogueIsContinue;
    private bool isInChose = false;
    public event EventHandler OnStoryStart;
    public event EventHandler OnStoryEnd;
    public event EventHandler OnSpecialComprasion;

    private TimerManager timerManager;
    private int timerID;
    
    // 特殊鬼魂对话选项对应的string
    private string choiceEffect;

    // 建立Ink的缓存机制，即玩家交互的Ink需要在之前的对话完成之后再触发。
    private List<Tuple<string, int>> inkLists = new List<Tuple<string, int>>();
    private InkStage globalInkStage = InkStage.PreTalk;
    private string currentInkName;

    private void Awake() {
        if (Instance != null) {
            throw new Exception("DialogueManager.Instance has exsited!");
        }
        Instance = this;
    }

    private void Start()
    {
        isDialogueIsContinue = false;
        chatPanelManager.Init();
        // 定时器
        timerManager = new TimerManager();
        timerManager.Init();
    }

    private void Update()
    {
        if (isDialogueIsContinue) {
            timerManager.Update();
        }
    }

    public void InitializedStroy(TextAsset inkJosn, int choice) {
        currentStory = new Story(inkJosn.text);

        if (choice != 0) {
            currentStory.variablesState["choice"] = choice;
        }
        
        isDialogueIsContinue = true;

        //故事开始
        OnStoryStart?.Invoke(this, EventArgs.Empty);
        timerID = timerManager.Schedule(ContinueDialogue, 0, duration);
    }

    private void ExitStroy() {
        isDialogueIsContinue= false;
        //故事结束
        // TODO: 增加Ink所包含的信息
        if (currentInkName != null) {
            InkStageInfoArgs inkStageInfoArgs = new InkStageInfoArgs(currentInkName, InkInfoManager.Instance.CheckInkStageInfo(currentInkName));
            OnStoryEnd?.Invoke(this, inkStageInfoArgs);
        }
        

        timerManager.Unschedule(timerID);

        if (inkLists.Count >= 1) {
            currentInkName = inkLists[0].Item1;
            globalInkStage = InkInfoManager.Instance.CheckInkStageInfo(currentInkName).inkStage;
            TextAsset inkJosn = LoadInkPath(inkLists[0].Item1);
            InitializedStroy(inkJosn, inkLists[0].Item2);
            inkLists.RemoveAt(0);
        }

    }

    private void ContinueDialogue() {
        if (isInChose) return;

        if (currentStory.canContinue) {
            string text = currentStory.Continue();
            if (currentStory.currentTags.Count > 0)
                HandleCharacterTags(currentStory.currentTags);
            if (isLeft && !isRight) {
                chatPanelManager.AddBubble(text, false);
            } 
            else if (!isLeft && isRight) {
                chatPanelManager.AddBubble(text, true);
            }
            else {
                Debug.LogWarning("Ink的Tag中未写明左右分区!!");
            }
        }
        else if (currentStory.currentChoices.Count > 0) {
            for (int i = 0; i < currentStory.currentChoices.Count; ++i) {

                if (currentStory.currentTags.Count > 0)
                    HandleCharacterTags(currentStory.currentTags);

                Choice choice = currentStory.currentChoices[i];
                Button button = CreateChoiceView (choice.text.Trim ());

                button.onClick.AddListener(delegate {
                    OnClickChoiceButton (choice, InkStage.PreTalk, choiceEffect);
                });

                isInChose = true;
            }
        }
        else{
            ExitStroy();
        }
    }

    /// </summary>
    /// <param name="inkName"><ink文件名>
    /// <param name="inkStage"><该ink属于何种阶段>
    /// <param name="idx"><某些ink会有分支选项，可能影响局内或局外>
    public void CacheInkFile(string inkName, int idx) {
        InkStageInfo inkStageInfo = InkInfoManager.Instance.CheckInkStageInfo(inkName);
        // TextAsset inkJosn = LoadInkPath(inkName);

        switch(inkStageInfo.inkStage) {
            case(InkStage.PreTalk):
                if (inkStageInfo.isTrigger) {
                    return;
                }
                inkStageInfo.isTrigger = true;
                inkLists.Add(new Tuple<string, int>(inkName, idx));
                break;
            case(InkStage.ComparisonTable):
                // TODO: 对照表的ink中需要说明choice的数量，这里需要验证
                // 判断对比表的选项是否已经触发
                if (inkStageInfo.memberTriggerlist[idx])
                    return;
                inkStageInfo.memberTriggerlist[idx] = true;
                inkLists.Add(new Tuple<string, int>(inkName, idx));
                break;
            case(InkStage.SpecialComparisonTable):
                // TODO:增加委托，告知游戏外玩家的选择，用于填充异常鬼魂登记表
                
                break;
            case(InkStage.JudgeCompelete):
                // 选项是否已经触发
                // if (inkStageInfo.memberTriggerlist[idx])
                //     return;
                // inkStageInfo.memberTriggerlist[idx] = true;
                // inkLists.Add(new Tuple<string, int>(inkName, idx));
                break;
            case(InkStage.Trial):
                if (inkStageInfo.isTrigger) {
                    return;
                }
                inkStageInfo.isTrigger = true;
                inkLists.Add(new Tuple<string, int>(inkName, idx));
                break;
            case(InkStage.Resurrection):
                if (inkStageInfo.isTrigger) {
                    return;
                }
                inkStageInfo.isTrigger = true;
                inkLists.Add(new Tuple<string, int>(inkName, idx));
                break;  
            case(InkStage.Evidence):
                // 选项是否已经触发
                if (inkStageInfo.memberTriggerlist[idx])
                    return;
                inkStageInfo.memberTriggerlist[idx] = true;
                inkLists.Add(new Tuple<string, int>(inkName, idx));
                break;
            case(InkStage.Bribe):
                if (inkStageInfo.isTrigger)
                    return;
                inkStageInfo.isTrigger = true;
                inkLists.Add(new Tuple<string, int>(inkName, idx));
                break;
            case(InkStage.TrialCompelete):
                // if (inkStageInfo.isTrigger)
                //     return;
                // inkStageInfo.isTrigger = true;
                // inkLists.Add(new Tuple<string, int>(inkName, idx));
                break;
            default:
                Debug.LogWarning("ink不属于现有任意阶段");
                break;
        }

        // 后日谈会直接打断所有对话
        if (inkStageInfo.inkStage == InkStage.JudgeCompelete || inkStageInfo.inkStage == InkStage.TrialCompelete) {
            if (inkStageInfo.isTrigger)
                return;
            inkStageInfo.isTrigger = true;
            inkLists.RemoveRange(0, inkLists.Count);
            ExitStroy();
            InitializedStroy(LoadInkPath(inkName), idx);
        }

        if (inkLists.Count == 1 && !isDialogueIsContinue) {
            globalInkStage = inkStageInfo.inkStage;
            currentInkName = inkLists[0].Item1;
            InitializedStroy(LoadInkPath(inkLists[0].Item1), idx);
            inkLists.RemoveAt(0);
        }

        
    }

    Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (buttonObject.transform, false);
		
		// Gets the text from the button prefab
		TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI> ();
		choiceText.text = text;

		return choice;
	}


    private void OnClickChoiceButton (Choice choice, InkStage inkStage, string choiceEffect) {
		currentStory.ChooseChoiceIndex (choice.index);

        // TODO：选择完对话选项是否会对游戏中某些部分产生影响
        // 处理特殊鬼魂的Ink
        if (inkStage == InkStage.SpecialComparisonTable) {
            InkChoiceEffectArgs inkChoiceEffectArgs = new InkChoiceEffectArgs(choiceEffect);
            // TODO:可能还需要增加信息
            OnSpecialComprasion?.Invoke(this, inkChoiceEffectArgs);
        }

        
        int childCount = buttonObject.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i) {
            Destroy(buttonObject.transform.GetChild(i).gameObject);
        }

        

        //选项选完直接进入下一条目录
        if (currentStory.canContinue) {
            string text = currentStory.Continue();

            if (text == "") {
                isInChose = false;
                return;
            }    

            while (text == "\n") {
                text = currentStory.Continue();
            }

            if (currentStory.currentTags.Count > 0)
                HandleCharacterTags(currentStory.currentTags);

            if (isLeft && !isRight) {
                chatPanelManager.AddBubble(text, false);
            } 
            else if (!isLeft && isRight) {
                chatPanelManager.AddBubble(text, true);
            }
            else {
                Debug.LogWarning("Ink的Tag中未写明左右分区!!");
            }
        }
        isInChose = false;
	}

    /**
        现有Ink的Tag有两项：
        Layout: 立绘位置，left或者right
        Sprite: 说话人的立绘,与立绘图片同名，图片放在Asset/Resources/CharacterSprite下，不能是中文
        Name: 说话人的名字，中文
    */
    private void HandleCharacterTags(List<String> tags) {
        foreach (string tag in tags) {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length > 2) {
                Debug.LogError("Ink Tag's count error!");
            }

            string strKey = splitTag[0].Trim();
            string strValue = splitTag[1].Trim();

            switch (strKey) {
                case TAG_LAYOUT:
                    if (strValue == "Left") {
                        isLeft = true;
                        isRight = false;
                    }
                    else {
                        isRight = true;
                        isLeft = false;
                    }
                    break;
                case TAG_CHOICEEFFECT:
                    choiceEffect = strValue;
                    break;
                default:
                    break;
            }
        }
    }

    private TextAsset LoadInkPath(string inkName) {
        TextAsset inkJosn = Resources.Load<TextAsset>(InkPath.inkPath + inkName);
        if (inkJosn != null) {
            return inkJosn;
        }
        else {
            Debug.LogWarning(inkName + "doesn't existed in Resources file!");
            return null;
        }
    }

    private void OnDestroy() {
        // timerManager.Unschedule(timerID);
    }
}
