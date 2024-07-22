using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    
    public static DialogueManager Instance;
    private Story currentStory;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;    
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private Canvas canvas;

    private bool isDialogueIsContinue;
    private bool isDialogueIsInteract;

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

        EnhancedInput.Instance.OnDialogueInteractEvent += DialogueInteract;
    }

    private void Update()
    {
        
        if (isDialogueIsContinue && isDialogueIsInteract) {
            ContinueDialogue();
        }
        isDialogueIsInteract = false;
    }


    public void InitializedStroy(TextAsset inkJosn) {
        currentStory = new Story(inkJosn.text);

        dialoguePanel.SetActive(true);
        isDialogueIsContinue = true;

        //故事开始
        OnStoryStart?.Invoke(this, EventArgs.Empty);

        ContinueDialogue();
    }

    private void ExitStroy() {
        isDialogueIsContinue= false;
        dialoguePanel.SetActive(false);
        dialogueText.text = null;

        //故事结束
        OnStoryEnd?.Invoke(this, EventArgs.Empty);

    }

    private void ContinueDialogue() {
        if (currentStory.currentChoices.Count > 0) {
            for (int i = 0; i < currentStory.currentChoices.Count; ++i) {
                Choice choice = currentStory.currentChoices[i];
                Button button = CreateChoiceView (choice.text.Trim ());

                button.onClick.AddListener(delegate {
                    OnClickChoiceButton (choice);
                });
            }
        }


        if (currentStory.canContinue) {
            dialogueText.text = currentStory.Continue();
        }
        else {
            ExitStroy();
        }
    }

    private void DialogueInteract(object sender, EventArgs eventArgs) {
        isDialogueIsInteract = true;
    }

    Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (canvas.transform, false);
		
		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}


    void OnClickChoiceButton (Choice choice) {
		currentStory.ChooseChoiceIndex (choice.index);
		ContinueDialogue();
	}
}
