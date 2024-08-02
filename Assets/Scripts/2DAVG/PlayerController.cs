using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private EnhancedInput enhancedInput;

    public bool isWalkingForward { get; private set; }
    public bool isWalkingBack { get; private set; }

    private void Awake() {
        isWalkingForward = false;
        isWalkingBack = false;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector2 inputVec = enhancedInput.GetInputVectorNormalized();
        Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y);
        float moveDistance = walkSpeed * Time.deltaTime;

        //胶囊体碰撞检测
        // bool canMove = !QueryCapsuleCollision(playerHeight, playerRaidus, moveVec, moveDistance);

        if (moveVec.x > 0) {
            isWalkingForward = true;
            isWalkingBack = false;
        }
        else if (moveVec.x < 0) {
            isWalkingForward = false;
            isWalkingBack = true;
        }
        else {
            isWalkingForward = false;
            isWalkingBack = false;
        }

        transform.position += moveVec * moveDistance;      
    }
}
