using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    // 统一的转换场景触发器，当有inkJosn的时候，会在跳转场景前先进行对话，后跳转场景
    // 如果没有ink文件，则交互后直接跳转。

    [SerializeField] private TextAsset inkJosn;
    [SerializeField] private GameObject exitMark;
    [SerializeField] private string sceneName;
    private bool isPlayerinRange = false;

    private void Start() {
        if (inkJosn != null) {
            DialogueManager.Instance.OnStoryEnd += SceneLoad;
        }
        EnhancedInput.Instance.OnInteractEvent += OnInteractEvent;
    }

    private void SceneLoad(object sender, EventArgs eventArgs) {
        if (isPlayerinRange) {
            SceneManager.LoadScene(sceneName);
        }
        
    }

    private void OnInteractEvent(object sender, EventArgs eventArgs) {
        if (isPlayerinRange) {
            if (inkJosn != null) {
                DialogueManager.Instance.InitializedStroy(inkJosn, 0);
            }
            else {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (isPlayerinRange) {
            exitMark.SetActive(true);
        }
        else {
            exitMark.SetActive(false);
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            isPlayerinRange = true;
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            isPlayerinRange = false;
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        if (inkJosn != null) {
            DialogueManager.Instance.OnStoryEnd -= SceneLoad;
        }
        EnhancedInput.Instance.OnInteractEvent -= OnInteractEvent;
    }
}
