using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator anim;
    private int isWalkingHash;
    private int isRunningHash;
    private int isWalkingBackHash;
    private int isRunningBackHash;



    void Start()
    {
        anim = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingBackHash = Animator.StringToHash("isWalkingBack");
        isRunningBackHash = Animator.StringToHash("isRunningBack");
    }

    // Update is called once per frame
    void Update()
    {
        bool runPressed = Input.GetKey("left shift");
        bool isWalking = anim.GetBool(isWalkingHash);
        bool isRunning = anim.GetBool(isRunningHash);

        bool forwardPressed = Input.GetKey("d");

        bool isWalkingBack = anim.GetBool(isWalkingBackHash);
        bool isRunningBack = anim.GetBool(isRunningBackHash);
        bool backwardPressed = Input.GetKey("a");

        //walking
        if (!isWalking && forwardPressed)
        {
            anim.SetBool(isWalkingHash, true);
        }

        if(isWalking && !forwardPressed)
        {
            anim.SetBool(isWalkingHash, false);
        }

        //running
        if (!isRunning && (forwardPressed && runPressed))
        {
            anim.SetBool(isRunningHash, true);
        }

        if (isRunning && (!forwardPressed || !runPressed))
        {
            anim.SetBool(isRunningHash, false);
        }

        //walking backwards
        if(!isWalkingBack && backwardPressed)
        {
            anim.SetBool(isWalkingBackHash, true);
        }

        if(isWalkingBack && !backwardPressed)
        {
            anim.SetBool(isWalkingBackHash, false);
        }
        
        //running backwards
        if (!isRunningBack && (backwardPressed && runPressed))
        {
            anim.SetBool(isRunningBackHash, true);
        }

        if (isRunningBack && (!backwardPressed || !runPressed))
        {
            anim.SetBool(isRunningBackHash, false);
        }
    }
}
