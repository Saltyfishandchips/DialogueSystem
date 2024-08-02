using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    [SerializeField] private TextAsset inkJosn;
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    private BoxCollider2D npcCollider;

    private float distance = 0f;
    public float followSpeed = 1f;
    public float targetDistance = 1f;

    private void Awake() {
        npcCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        distance = target.position.x - transform.position.x;
        // 玩家在NPC右侧，需要跟随

        if (transform.parent.position.x >= 8f) {
            animator.SetBool("isMovingRight", false);
        }
        else if (distance > targetDistance) {
            transform.parent.position =new Vector3(transform.position.x + followSpeed * Time.deltaTime, transform.position.y, 0);
            animator.SetBool("isMovingRight", true);
            npcCollider.isTrigger = false;
        }     
        else {
            animator.SetBool("isMovingRight", false);
        }

    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Player") {
            DialogueManager.Instance.InitializedStroy(inkJosn, 0);
        }
    }
}
