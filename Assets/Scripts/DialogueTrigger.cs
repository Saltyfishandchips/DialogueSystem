using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject interactMark;
    
    [SerializeField] private TextAsset inkJosn;
    
    private bool isPlayerInRange;

    private void Awake() {
        HideInteractMark();
        isPlayerInRange = false;
    }

    private void Start() {
        EnhancedInput.Instance.OnInteractEvent += DialogueStart;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            isPlayerInRange = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange) {
            ShowInteractMark();
        }
        else {
            HideInteractMark();
        }
    }

    private void ShowInteractMark() {
        interactMark.SetActive(true);
    }

    private void HideInteractMark() {
        interactMark.SetActive(false);
    }

    private void DialogueStart(object sender, EventArgs eventArgs) {
        // 只有当玩家在距离范围内并且对话未触发时调用
        if (isPlayerInRange && !DialogueManager.Instance.isDialogueIsContinue) {
            // Debug.Log(inkJosn);
            DialogueManager.Instance.InitializedStroy(inkJosn);
        }
    }
}
