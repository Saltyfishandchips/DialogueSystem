using UnityEngine;
using Ink.Runtime;
using System;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    
    public static DialogueManager Instance;
    private Story currentStory;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    public bool isDialogueIsContinue {get; private set;}
    private bool isDialogueIsInteract;

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

        ContinueDialogue();
    }

    private void ExitStroy() {
        isDialogueIsContinue= false;
        dialoguePanel.SetActive(false);
        dialogueText.text = null;

    }

    private void ContinueDialogue() {
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
}
