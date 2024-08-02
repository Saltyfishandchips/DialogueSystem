using System;
using UnityEngine;


public class AnimEventTrigger : MonoBehaviour
{
    // 指定animation 动画控制器，在脚本所在的物体上直接拖拽即可
    public Animator armAnimator;
    private const string IS_CLIMB = "isClimb";
    [SerializeField] private TextAsset inkJosn;
    // 主角是否有初次场景的对话
    [SerializeField] public bool hasFirstDialogue = true;

    public static event EventHandler OnFirstDialogStart;
    public static event EventHandler OnSkipFirstDialog;

    private bool firstClimb = true;

    // 脚步声
    [SerializeField] private AudioClip[] leftFootSound;
    [SerializeField] private AudioClip[] RightFootSound;

    private AudioSource audioSounce;

    private void Awake() {
        audioSounce = GetComponent<AudioSource>();
    }

    public void DeadAnimComplete()
    {
        // print("Dead completed! " + Time.time);
        armAnimator.SetBool(IS_CLIMB, true);
        armAnimator.SetBool("isDead", false);
    }

    // 主角初始场景的对话
    public void ClimbAnimComplete() {
        if (hasFirstDialogue) {
            hasFirstDialogue = false;
            firstClimb = false;
            
            OnFirstDialogStart?.Invoke(this, EventArgs.Empty);
            DialogueManager.Instance.InitializedStroy(inkJosn, 0);
        }
        else {
            if (firstClimb) {
                firstClimb = false;
                OnSkipFirstDialog?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    private void LeftFootStep() {
        audioSounce.clip = leftFootSound[UnityEngine.Random.Range(0, leftFootSound.Length)];
        audioSounce.Play();
    }

    private void RightFootStep() {
        audioSounce.clip = RightFootSound[UnityEngine.Random.Range(0, RightFootSound.Length)];
        audioSounce.Play();
    }

}
