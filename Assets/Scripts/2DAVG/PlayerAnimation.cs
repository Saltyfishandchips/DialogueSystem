using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IS_WALKINGFORWARD = "isWalkingForward";
    private const string IS_WALKINGBACK = "isWalkingBack";

    // 黄泉路场景
    private const string IS_DEAD = "isDead";
    // 鬼街堡不需要倒下动画
    public bool isSkipDeath = false;
    

    private Animator animator;
    [SerializeField]private PlayerController playerController;
    private void Awake() {
        animator = GetComponent<Animator>();
        if (!isSkipDeath) {
            animator.SetBool(IS_DEAD, true);
        }
    }

    private void Update() {

        animator.SetBool(IS_WALKINGFORWARD, playerController.isWalkingForward);
        animator.SetBool(IS_WALKINGBACK, playerController.isWalkingBack);
    }
}
