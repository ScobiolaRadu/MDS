using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isRunningForwardHash;
    int isStrafingLeftHash;
    int isStrafingRightHash;
    int isRunningBackwardHash;
    int isJumpingHash;

    string forward = "w";
    string back = "s";
    string left = "a";
    string right = "d";
    string jump = "space";

    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningForwardHash = Animator.StringToHash("isRunningForward");
        isStrafingLeftHash = Animator.StringToHash("isStrafingLeft");
        isStrafingRightHash = Animator.StringToHash("isStrafingRight");
        isRunningBackwardHash = Animator.StringToHash("isRunningBackward");
        isJumpingHash = Animator.StringToHash("isJumping");
 
    }

    void Update()
    {
        bool isRunningForward = animator.GetBool(isRunningForwardHash);
        bool forwardPressed = Input.GetKey(forward);

        if (!isRunningForward && forwardPressed)
            animator.SetBool(isRunningForwardHash, true);
        if(isRunningForward && !forwardPressed)
            animator.SetBool(isRunningForwardHash, false);


        bool isRunningBackward = animator.GetBool(isRunningBackwardHash);
        bool backwardPressed = Input.GetKey(back);

        if (!isRunningBackward && backwardPressed)
            animator.SetBool(isRunningBackwardHash, true);
        if (isRunningBackward && !backwardPressed)
            animator.SetBool(isRunningBackwardHash, false);

        bool isStrafingLeft = animator.GetBool(isStrafingLeftHash);
        bool leftPressed = Input.GetKey(left);

        if (!isStrafingLeft && leftPressed)
            animator.SetBool(isStrafingLeftHash, true);
        if (isStrafingLeft && !leftPressed)
            animator.SetBool(isStrafingLeftHash, false);

        bool isStrafingRight = animator.GetBool(isStrafingRightHash);
        bool rightPressed = Input.GetKey(right);

        if (!isStrafingRight && rightPressed)
            animator.SetBool(isStrafingRightHash, true);
        if (isStrafingRight && !rightPressed)
            animator.SetBool(isStrafingRightHash, false);

        bool isJumping = animator.GetBool(isJumpingHash);
        bool jumpPressed = Input.GetKey(jump);

        if (!isJumping && jumpPressed)
            animator.SetBool(isJumpingHash, true);
        if (isJumping && !jumpPressed)
            animator.SetBool(isJumpingHash, false);

    }
}
