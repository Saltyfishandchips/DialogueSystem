using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject interactMark;
    [SerializeField] private GameObject homeSaveUI;
    [SerializeField] private Button restButton;
    [SerializeField] private Button hangoutButton;

    private AudioSource audioSource;
    [SerializeField] private AudioClip openDoorClip;

    [SerializeField] private GameObject dayPassCanvas;

    // 第二次触发
    private bool hasTriggered = false;


    private bool isPlayerinRange = false;

    private void Awake() {
        restButton.onClick.AddListener(() => {
            /* 
                增加存档系统
            */
            hasTriggered = true;

            DayManager.dayPass += 1;
            homeSaveUI.SetActive(false);

            //TODO: 增加鸡叫，之后需要修改
            dayPassCanvas.SetActive(true);
            openDoorClip = Resources.Load<AudioClip>("Sounds/Cock");
            if (audioSource && openDoorClip) {
                audioSource.clip = openDoorClip;
                audioSource.volume = 0.3f;
                audioSource.Play();
            }
        });

        hangoutButton.onClick.AddListener(() => {
            homeSaveUI.SetActive(false);
        });

        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        EnhancedInput.Instance.OnInteractEvent += InteractEvent;
        interactMark.SetActive(false);
        homeSaveUI.SetActive(false);
    }

    private void Update() {
        
        if (isPlayerinRange) {
            interactMark.SetActive(true);
        }
        else {
            interactMark.SetActive(false);
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

    private void InteractEvent(object sender, EventArgs eventArgs) {
        if (isPlayerinRange) {
            if (!hasTriggered) {
                homeSaveUI.SetActive(true);

                if (audioSource && openDoorClip) {
                    audioSource.clip = openDoorClip;
                    audioSource.Play();
                }
            }
            else {
                dayPassCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "该去一殿工作了……";
                dayPassCanvas.SetActive(true);
            }
            
        }
    }

    private void OnDestroy() {
        EnhancedInput.Instance.OnInteractEvent -= InteractEvent;
    }

}
