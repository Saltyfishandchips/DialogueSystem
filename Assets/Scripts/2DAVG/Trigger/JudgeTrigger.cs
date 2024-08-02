using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JudgeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject judgeCanvas;
    [SerializeField] private GameObject judgeMark;
    [SerializeField] private Button judgeButton;
    [SerializeField] private Button waitButton;
    [SerializeField] public TextAsset textAsset;
    // Start is called before the first frame update
    
    private bool isPlayerinRange = false;

    private void Awake() {
        judgeCanvas.SetActive(false);
        judgeMark.SetActive(false);
    }

    private void Start()
    {
        EnhancedInput.Instance.OnInteractEvent += JudgementStart;
        DialogueManager.Instance.OnStoryEnd += JudgementSceneLoad;

        judgeButton.onClick.AddListener(() => {
            if (textAsset != null) {
                DialogueManager.Instance.InitializedStroy(textAsset, 0);
            }
            else {
                JudgementSceneLoad(this, EventArgs.Empty);
            }
            
            judgeCanvas.SetActive(false);
        });
        waitButton.onClick.AddListener(() => {
            judgeCanvas.SetActive(false);
        });
    }

    private void JudgementSceneLoad(object sender, EventArgs eventArgs) {
        if (isPlayerinRange) {
            SceneManager.LoadScene("ConparsionScene");
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPlayerinRange) {
            judgeMark.SetActive(true);
        }
        else {
            judgeMark.SetActive(false);
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
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
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            isPlayerinRange = false;
        }
    }

    private void JudgementStart(object sender, EventArgs args) {
        if (isPlayerinRange) {
            judgeCanvas.SetActive(true);
        }
    }
    
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        EnhancedInput.Instance.OnInteractEvent -= JudgementStart;
        DialogueManager.Instance.OnStoryEnd -= JudgementSceneLoad;
    }
}
