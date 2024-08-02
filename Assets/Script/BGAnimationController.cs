using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAnimationController : MonoBehaviour
{
    public Animator animator;  // 引用到 Animator 组件
    public string animationTriggerName = "BGPlay";  // 动画触发器的名称

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned.");
            return;
        }
    }

    public void PlayAnimation()
    {
        animator.SetTrigger(animationTriggerName);
        Debug.Log("PlayEnd");
    }
}
