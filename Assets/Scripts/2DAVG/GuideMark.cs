using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMark : MonoBehaviour
{
    [SerializeField] private float durationTime = 0f;
    [SerializeField] private GameObject guideGameObject;
    [SerializeField] private AnimEventTrigger animEventTrigger;
    private float timer = 0f;
    private bool showMark = false;
    
    private void Awake() {
        guideGameObject.SetActive(false);
        
    }

    void Start()
    {
        timer = 0f;

        // 玩家有初次对话
        if (animEventTrigger.hasFirstDialogue) {
            DialogueManager.Instance.OnStoryEnd += FirstDialogEnd;
        }
        else {
            showMark = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showMark) {
            timer += Time.deltaTime;
            if (timer < durationTime) {
                guideGameObject.SetActive(true);
            }
            else {
                guideGameObject.SetActive(false);
                Destroy(gameObject); 
            }
        }
    }

    private void FirstDialogEnd(object sender, EventArgs eventArgs) {
        showMark = true;
    }
}
