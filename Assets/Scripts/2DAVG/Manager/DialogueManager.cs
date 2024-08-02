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

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;    
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private float typeSpeed = 1f;
    [SerializeField] private GameObject dialogueMask;

    // 立绘区域
    [SerializeField] private GameObject leftPanel;
    [SerializeField] private Image leftImage;
    [SerializeField] private TextMeshProUGUI leftNameText;

    [SerializeField] private GameObject rightPanel;
    [SerializeField] private Image rightImage;
    [SerializeField] private TextMeshProUGUI rightNameText;
    private const string TAG_SPEAKER = "Speaker";
    private const string TAG_LAYOUT = "Layout";
    private const string TAG_NAME = "Name";
    private const string CHARACTER_PATH = "CharacterSprite/";


    private bool isDialogueIsContinue;
    private bool isDialogueIsInteract;
    private bool isInChose = false;

    public event EventHandler OnStoryStart;
    public event EventHandler OnStoryEnd;

    private void Awake() {
        if (Instance != null) {
            throw new System.Exception("DialogueManager.Instance has exsited!");
        }
        Instance = this;

        
    }

    private void Start()
    {
        isDialogueIsContinue = false;
        isDialogueIsInteract = false;
        dialoguePanel.SetActive(false);
        leftPanel.SetActive(false);
        rightPanel.SetActive(false);
        dialogueMask.SetActive(false);

        EnhancedInput.Instance.OnDialogueInteractEvent += DialogueInteract;
    }

    private void Update()
    {
        
        if (isDialogueIsContinue && isDialogueIsInteract) {
            isDialogueIsInteract = false;
            ContinueDialogue();
        }
        isDialogueIsInteract = false;
    }


    public void InitializedStroy(TextAsset inkJosn, int choice) {
        currentStory = new Story(inkJosn.text);

        if (choice != 0) {
            currentStory.variablesState["choice"] = choice;
        }
        

        dialoguePanel.SetActive(true);
        isDialogueIsContinue = true;

        //故事开始
        OnStoryStart?.Invoke(this, EventArgs.Empty);

        ContinueDialogue();
    }

    private void ExitStroy() {
        isDialogueIsContinue= false;
        dialoguePanel.SetActive(false);

        leftPanel.SetActive(false);
        rightPanel.SetActive(false);

        dialogueText.text = null;

        //故事结束
        OnStoryEnd?.Invoke(this, EventArgs.Empty);

    }

    private void ContinueDialogue() {
        if (isInChose) return;

        if (currentStory.canContinue) {
            
            // dialogueText.text = currentStory.Continue();

            string text = currentStory.Continue();

            if (currentStory.currentTags.Count > 0)
                HandleCharacterTags(currentStory.currentTags);

            //增加打字机动画
            var t = DOTween.To(() => string.Empty, value => dialogueText.text = value, text, typeSpeed).SetEase(Ease.Linear);
            //富文本
            t.SetOptions(true);
        }
        else if (currentStory.currentChoices.Count > 0) {
            for (int i = 0; i < currentStory.currentChoices.Count; ++i) {
                Choice choice = currentStory.currentChoices[i];
                Button button = CreateChoiceView (choice.text.Trim ());

                button.onClick.AddListener(delegate {
                    OnClickChoiceButton (choice);
                });

                isInChose = true;
            }
        }
        else{
            ExitStroy();
        }
    }

    private void DialogueInteract(object sender, EventArgs eventArgs) {
        isDialogueIsInteract = true;
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


    private void OnClickChoiceButton (Choice choice) {
		currentStory.ChooseChoiceIndex (choice.index);
        
        int childCount = buttonObject.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i) {
            Destroy(buttonObject.transform.GetChild(i).gameObject);
        }

        //选项选完直接进入下一条目录
        if (currentStory.canContinue) {
            // dialogueText.text = currentStory.Continue();
            string text = currentStory.Continue();

            while (text == "\n") {
                text = currentStory.Continue();
            }
            //增加打字机动画
            var t = DOTween.To(() => string.Empty, value => dialogueText.text = value, text, typeSpeed).SetEase(Ease.Linear);
            //富文本
            t.SetOptions(true);

        }

        isInChose = false;
		// ContinueDialogue();
	}

    /**
        现有Ink的Tag有两项：
        Layout: 立绘位置，left或者right
        Sprite: 说话人的立绘,与立绘图片同名，图片放在Asset/Resources/CharacterSprite下，不能是中文
        Name: 说话人的名字，中文
    */
    
    private void HandleCharacterTags(List<String> tags) {
        bool isLeft = false;
        bool isRight = false;
        Sprite characterSprite = null;
        string name = null;

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
                case TAG_SPEAKER:
                    Texture2D img = Resources.Load<Texture2D>(CHARACTER_PATH + strValue);
                    characterSprite = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
                    break;
                case TAG_NAME:
                    name = strValue;
                    break;
                default:
                    break;

            }
        }

        if (isLeft) {
            leftImage.sprite = characterSprite;
            leftNameText.text = name;
            leftPanel.SetActive(true);
            rightPanel.SetActive(false);
        }
        else if (isRight) {
            rightImage.sprite = characterSprite;
            rightNameText.text = name;
            rightPanel.SetActive(true);
            leftPanel.SetActive(false);
        }
        
    }
}
