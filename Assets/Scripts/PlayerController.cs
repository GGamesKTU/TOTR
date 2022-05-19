using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Public variables

    [SerializeField]
    private float maxWalkSpeed = 0.25f;

    [SerializeField]
    private float maxRunSpeed = 0.7f;

    [SerializeField]
    private float  runAccelaration = 0.1f;

    [SerializeField]
    private float deacelaration = 0.1f;

    [SerializeField]
    private float walkAccelaration = 0.05f;

    [SerializeField]
    private float maxSneakSpeed = 0.35f;

    [SerializeField]
    private float sneakAccelaration = 0.15f;

    [SerializeField]
    private float JumpSpeed = 8;

    [SerializeField]
    private float JumpDuration = 150;

    // Input variables
    private float horizontal;
    private float vertical;
    private float jumpInput;
    private bool run;

    // Internal variables
    private bool onTheGround;
    private float jumpDuration;
    private bool jumpKeyDown = false;
    private bool canVariableJump = false;
    private float movement_Anim;

    private float speed = 0f;
    private float prevHorizontal;

    private Rigidbody rigid;
    Animator anim;

    [SerializeField]
    LayerMask layerMask;

    Transform modelTrans;
    GameObject rsp;

    private Transform shoulderTrans;

    [SerializeField]
    private Transform rightShouder;

    [SerializeField]
    private Transform lookPos;

    private Stamina stamina;

    public bool isSneaking = false;

    private Backpack backpack;

    private void Start()
    {
        backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
        maxWalkSpeed = 0.025f + 0.003f * backpack.SpeedUp;
        maxRunSpeed = 0.07f + 0.003f * backpack.SpeedUp;
        maxSneakSpeed = 0.0125f + 0.003f * backpack.SpeedUp;

        shoulderTrans = GameObject.FindGameObjectWithTag("Shoulder").transform;
        stamina = GetComponent<Stamina>();
        rigid = GetComponent<Rigidbody>();
        SetupAnimator();

        rsp = new GameObject();
        rsp.name = transform.root.name + "Right Shoulder IK Helper";
    }

    private void Update()
    {
        run = Input.GetKey(KeyCode.LeftShift);
        if (stamina.GetStamina() < 5) run = false;
        isSneaking = Input.GetKey(KeyCode.LeftControl);
    }

    private void FixedUpdate()
    {
        InputHandler();
        UpdateRigidbodyValues();
        // HandleAnimations();
        MovementHandler();
        HandleRotation();
        HandleAimingPos();
        HandleShoulder();
    }

    private void InputHandler()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Fire2");
    }

    private void UpdateRigidbodyValues()
    {
        if (onTheGround)
        {
            rigid.drag = 4;
        }
        else
        {
            rigid.drag = 0;
        }
    }

    private void MovementHandler()
    {
        onTheGround = isOnGround();
        if(horizontal > prevHorizontal)
        {
            speed += deacelaration * Time.deltaTime;
            speed = Mathf.Min(speed, 0);
        }
        else if(horizontal < prevHorizontal)
        {
            speed -= deacelaration * Time.deltaTime;
            speed = Mathf.Max(speed, 0);
        }
        else if(horizontal > 0.1f)
        {
            if(isSneaking)
            {
                speed += sneakAccelaration * Time.deltaTime;
                speed = Mathf.Min(speed, maxSneakSpeed);

            }
            else if (run)
            {
                speed += runAccelaration * Time.deltaTime;
                speed = Mathf.Min(speed, maxRunSpeed);
            }
            else
            {
                speed += walkAccelaration * Time.deltaTime;
                speed = Mathf.Min(speed, maxWalkSpeed);
            }

        }
        else if(horizontal < -0.1f)
        {
            if(isSneaking)
            {
                speed -= sneakAccelaration * Time.deltaTime;
                speed = Mathf.Max(speed, -maxSneakSpeed);
            }
            else if (run)
            {
                speed -= runAccelaration * Time.deltaTime;
                speed = Mathf.Max(speed, -maxRunSpeed);
            }
            else
            {
                speed -= walkAccelaration * Time.deltaTime;
                speed = Mathf.Max(speed, -maxWalkSpeed);
            }
        }

        prevHorizontal = horizontal;
        Vector3 Ray = new Vector3(gameObject.transform.position.x, gameObject.transform.position.z + 0.5f, gameObject.transform.position.z);
        if((speed > 0 && !Physics.Raycast(Ray, Vector3.right, 0.8f, layerMask)) || (speed < 0 && !Physics.Raycast(Ray, Vector3.left, 0.8f, layerMask)))
        {
            gameObject.transform.position = new Vector2(transform.position.x + speed, 0);
        }

        if (jumpInput > 0.1f)
        {
            if (!jumpKeyDown)
            {
                jumpKeyDown = true;

                if (onTheGround)
                {
                    rigid.velocity = new Vector3(rigid.velocity.y, this.JumpSpeed, 0);
                    jumpDuration = 0.0f;
                }
            }
            else if (canVariableJump)
            {
                jumpDuration += Time.deltaTime;

                if (jumpDuration < this.JumpDuration / 1000)
                {
                    rigid.velocity = new Vector3(rigid.velocity.x, this.JumpSpeed, 0);
                }
            }
        }
        else
        {
            jumpKeyDown = false;
        }

    }

    private void HandleRotation()
    {
        Vector3 lookAt = GetLookPosition();
        Vector3 directionToLook = lookAt - transform.position;
        directionToLook.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15);
    }

    private void HandleAimingPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            lookPos.position = lookP;
        }
    }

    private void HandleAnimations()
    {
        anim.SetBool("OnAir", !onTheGround);
    }

    private void HandleShoulder()
    {
        shoulderTrans.LookAt(lookPos);

        Vector3 rightShoulderPos = rightShouder.TransformPoint(Vector3.zero);
        rsp.transform.position = rightShoulderPos;
        rsp.transform.parent = transform;

        shoulderTrans.position = rsp.transform.position;

    }

    private bool isOnGround()
    {
        bool retVal = false;
        float lengthToSearch = 1.5f;

        Vector3 lineStart = transform.position + Vector3.up;

        Vector3 vectorToSearch = -Vector3.up;

        RaycastHit hit;

        if (Physics.Raycast(lineStart, vectorToSearch, out hit, lengthToSearch, layerMask))
        {
            retVal = true;
        }

        return retVal;
    }

    private void SetupAnimator()
    {
        anim = GetComponent<Animator>();
    }

    public Vector3 GetLookPosition()
    {
        return lookPos.position;
    }

    public bool GetIsRunning()
    {
        return run;
    }
}
