using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDialogueTrigger : MonoBehaviour
{
    // public string josnPath;
    // Start is called before the first frame update
    public TextAsset inkJosn;
    public bool canJumpScene = false;
    private bool isTrigger = false;
    [SerializeField] private GameObject dialogueMask;
    [SerializeField] private string sceneName;

    private void Awake() {
        // inkJosn = Resources.Load<TextAsset>(DialoguePath.AirWallPath);
    }

    private void Start() {
        DialogueManager.Instance.OnStoryEnd += JumpScene;
        //获取遮罩
        GameObject dialogueCanvas = GameObject.Find("DialogueCanvas");
        dialogueMask = dialogueCanvas.transform.Find("Mask").gameObject;
        dialogueMask.SetActive(false);  
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            if (canJumpScene) {
                dialogueMask.SetActive(true);
            }
            DialogueManager.Instance.InitializedStroy(inkJosn, 0);
        }
        isTrigger = true;   
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            DialogueManager.Instance.InitializedStroy(inkJosn, 0);
        }

    }

    private void JumpScene(object sender, EventArgs eventArgs) {
        if (isTrigger && canJumpScene) {
            SceneManager.LoadScene(sceneName);
        }  
    }

    private void OnDestroy() {
        DialogueManager.Instance.OnStoryEnd -= JumpScene;
    }
    
}
