using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController2D : MonoBehaviour
{
    Animator animator;
    private Stamina stamina;

    float velocityX = 0.0f;
    float velocityY = 0.0f;
    public float acceleration = 0.35f;
    public float deceleration = 2f;
    public float maxWalkVelocity = 0.45f;
    public float maxRunVelocity = 2.0f;
    int VelocityXHash;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        stamina = GetComponent<Stamina>();
        animator = GetComponent<Animator>();
        VelocityXHash = Animator.StringToHash("Velocity X");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.D);
        bool backwardPressed = Input.GetKey(KeyCode.A);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool sneakPressed = Input.GetKey(KeyCode.LeftControl);
        if (stamina.GetStamina() < 5) runPressed = false;

        //maximum speed = maxRunVelocity, when runPressed is true
        //maximum speed = maxWalkSpeed, when runPressed is false
        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        if (player.transform.rotation.eulerAngles.y > 200)
        {
            forwardPressed = false;
            backwardPressed = true;
        }

        changeVelocity(forwardPressed, backwardPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, backwardPressed, runPressed, sneakPressed, currentMaxVelocity);

        animator.SetFloat(VelocityXHash, velocityX);
    }

    void changeVelocity(bool forwardPressed, bool backwardPressed, float currentMaxVelocity)
    {
        //walking and running forward
        if (forwardPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        //decreasing speed to 0 & stopping animations
        if (!forwardPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        //walking and running backwards
        if (backwardPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        //decreasing speed to 0 & stopping animations
        if (!backwardPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
    }

    void lockOrResetVelocity(bool forwardPressed, bool backwardPressed, bool runPressed, bool sneakPressed, float currentMaxVelocity)
    {
        ///****MOVING FORWARD****
        //make velocity capped to maximum velocity
        if (forwardPressed && runPressed && !sneakPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        //decrease speed to new max velocity when no longer running
        else if (forwardPressed && !sneakPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            //make velocity cap at maximum value when close to it
            if (velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }

        ///****MOVING BACKWARDS****
        //make max speed capped when running backwards
        if (backwardPressed && runPressed && !sneakPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        //decrease speed to new max velocity when no longer running
        else if (backwardPressed && !sneakPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            //make velocity cap at maximum value when close to it
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        //make max speed capped at maximum velocity when walking backwards
        else if (backwardPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

        if(sneakPressed && !runPressed)
        {
            animator.SetBool("isSneaking", true);
        }
        else if(!sneakPressed)
        {
            animator.SetBool("isSneaking", false);
        }
        

        //reset velocity when close to 0.0f
        if ((!forwardPressed && !backwardPressed) && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
    }
}
