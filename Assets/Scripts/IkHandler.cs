using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkHandler : MonoBehaviour
{
    private Animator anim;
    private Vector3 lookPos;
    private Vector3 IK_lookPos;
    private Vector3 targetPos;
    private PlayerController pl;

    [SerializeField]
    private float lerpRate = 15;

    [SerializeField]
    private float updateLookPosThreshold = 4;

    [SerializeField]
    private float lookWeight = 1;

    [SerializeField]
    private float bodyWeight = .9f;

    [SerializeField]
    private float headWeight = 1;

    [SerializeField]
    private float clampWeight = 1;

    [SerializeField]
    private float rightHandWeight = 1;

    [SerializeField]
    public float leftHandWeight = 1;


    [SerializeField]
    private Transform rightHandTarget;

    [SerializeField]
    private Transform rightElbowTarget;

    [SerializeField]
    private Transform leftHandTarget;

    [SerializeField]
    private Transform leftElbowTarget;


    // Start is called before the first frame update
    void Start()
    {
        this.anim = GetComponent<Animator>();
        pl = GetComponent<PlayerController>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);

        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);

        anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, rightHandWeight);
        anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, leftHandWeight);

        anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowTarget.position);
        anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowTarget.position);

        this.lookPos = pl.GetLookPosition();
        lookPos.z = transform.position.z;

        float distanceFromPlayer = Vector3.Distance(lookPos, transform.position);

        if (distanceFromPlayer > updateLookPosThreshold || distanceFromPlayer < updateLookPosThreshold)
        {
            targetPos = lookPos;
        }

        IK_lookPos = Vector3.Lerp(IK_lookPos, targetPos, Time.deltaTime * lerpRate);

        anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight, headWeight, clampWeight);
        anim.SetLookAtPosition(IK_lookPos);

    }
}

