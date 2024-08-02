using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDialogueTrigger : MonoBehaviour
{
    public TextAsset inkJosn;
    private bool isPlayerinRange = false;

    [SerializeField] private bool onlyShowFirstTime = false;

    // 对话结束后NPC消失
    [SerializeField] private bool isDialogEndDispear = false;
    [SerializeField] private GameObject NPC;

    private void Start() {
        DialogueManager.Instance.OnStoryEnd += OnStoryEnd;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            isPlayerinRange = true;
            DialogueManager.Instance.InitializedStroy(inkJosn, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            isPlayerinRange = false;
        }
    }

    private void OnStoryEnd(object sender, EventArgs eventArgs) {
        if (isPlayerinRange) {
            if (isDialogEndDispear) {
                NPC.SetActive(false);
            }

            if (onlyShowFirstTime) {
                gameObject.SetActive(false);
            }
        }
    }
    
    private void OnDestroy() {
        DialogueManager.Instance.OnStoryEnd -= OnStoryEnd;
    }
}
