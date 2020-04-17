using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//using UnityStandardAssets.Vehicles.Car;

public class OwnThirdPersonController : MonoBehaviour {

    #region Variable
    public FixedJoystick LeftJoystick;
    private FixedButton ShootButton;
    private FixedButton JumpButton;
    private FixedButton CrouchButton;
    public FixedTouchField TouchField;
    private float JumpForce = 5f;
    protected Actions Actions;
    protected PlayerController PlayerController;
    protected Rigidbody Rigidbody;
    protected ParticleSystem ParticleSystem;

    public Animator animator;
    protected bool walk = false;

    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.1f;
    protected float CameraPosY = 3f;
    protected float CameraPosSpeed = 0.02f;

    protected bool isCrouching = false;
    protected CapsuleCollider CapCollider;

    //car
    //protected CarController carController;
    protected SkinnedMeshRenderer Renderer;
    private FixedButton CarButton;

    protected float Cooldown;

    public int rotationX = 180;
    public float speed = 5f;
    public float zoomInOut = 5f;

    public NavMeshAgent agent;

    #endregion

    #region Start Update
    // Use this for initialization
    void Start ()
    {
       Actions = GetComponent<Actions>();
        PlayerController = GetComponent<PlayerController>();
        Rigidbody = GetComponent<Rigidbody>();
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        CapCollider = GetComponent<CapsuleCollider>();
        Renderer = GetComponentInChildren<SkinnedMeshRenderer>();

        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
       // if (carController == null)
       // {
            Walk();
        //    Shoot();
        //    Jump();
        //    Crouch();
       // }
       // CarUpdate();
    }
    #endregion

    #region Crouch
    private void Crouch()
    {
        var crouchbutton = CrouchButton.Pressed || Input.GetKey(KeyCode.C);

        if (!isCrouching && crouchbutton)
        {
            //crouch
            CapCollider.height = 0.5f;
            CapCollider.center = new Vector3(CapCollider.center.x, 0.25f, CapCollider.center.z);
            isCrouching = true;
            //Actions.Sitting(true);
        }

        Debug.DrawRay(transform.position, Vector3.up * 2f, Color.green);
        if (isCrouching && !crouchbutton)
        {
            //try to stand up
            var cantStandUp = Physics.Raycast(transform.position, Vector3.up, 2f);

            if (!cantStandUp)
            {
                CapCollider.height = 1f;
                CapCollider.center = new Vector3(CapCollider.center.x, 0.5f, CapCollider.center.z);
                isCrouching = false;
               // Actions.Sitting(false);
            }
        }
    }
    #endregion

    #region Walk

    private void Walk()
    {
        //Control.m_Jump = Button.Pressed;
        //Control.Hinput = LeftJoystick.inputVector.x;
        //Control.Vinput = LeftJoystick.inputVector.y;
        Vector3 input = new Vector3(LeftJoystick.inputVector.x, 0, LeftJoystick.inputVector.y);

        var vel = Quaternion.AngleAxis(CameraAngleY + 180, Vector3.up) * input * speed;
        transform.rotation = Quaternion.AngleAxis(CameraAngleY + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.0001f, Vector3.up) + rotationX, Vector3.up);
        Rigidbody.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);

        //animator.SetTrigger("Walk");

        CameraAngleY += TouchField.TouchDist.x * CameraAngleSpeed;
        CameraPosY = Mathf.Clamp(CameraPosY - TouchField.TouchDist.y * CameraPosSpeed, 0, speed);

        Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, CameraPosY, zoomInOut);
        Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);
        walk = true;

        if (LeftJoystick.inputVector.x != 0 && LeftJoystick.inputVector.y != 0)
        {
            animator.Play("Run");
        }
        else if(LeftJoystick.inputVector.x == 0 && LeftJoystick.inputVector.y == 0)
        {
            animator.Play("Idle2");
        }

        //Debug.Log(LeftJoystick.inputVector.x);
        //Debug.Log(LeftJoystick.inputVector.y);

    }
    #endregion

    #region Shoot
    private void Shoot()
    {

        Cooldown -= Time.deltaTime;
        if (Cooldown < 0)
            ParticleSystem.Stop();

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (ShootButton.Pressed)
        {
            PlayerController.SetArsenal("Rifle");
           // Actions.Attack();

            if (Cooldown <= 0f)
            {
                Cooldown = 0.3f;
                ParticleSystem.Play();

                RaycastHit hitinfo;
                if (Physics.Raycast(ray, out hitinfo))
                {
                    var other = hitinfo.collider.GetComponent<Shootable>();
                    if (other != null)
                    {
                        other.GetComponent<Rigidbody>().AddForceAtPosition((hitinfo.point - ParticleSystem.transform.position).normalized * 500f, hitinfo.point);
                    }
                }

            }
        }
        else
        {

            if (Rigidbody.velocity.magnitude > 3f)
                Actions.Run();
            else if (Rigidbody.velocity.magnitude > 0.5f)
                Actions.Walk();
            else
                Actions.Stay();
        }
    }

    #endregion

    #region Jump
    private void Jump()
    {

        var grounded = Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, 0.1f);
        Debug.DrawRay(transform.position + Vector3.up * 0.05f, Vector3.down, Color.red, 0.1f);

        if (JumpButton.Pressed && grounded)
        {
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, JumpForce, Rigidbody.velocity.z);
            Actions.Jump();
        }

    }
}
#endregion

    #region Stunt
/*
var crouchbutton = CrouchButton.Pressed || Input.GetKey(KeyCode.C);

        if (!isCrouching && crouchbutton)
        {
            //croch
            CapCollider.height = 0.5f;
            CapCollider.center = new Vector3(CapCollider.center.x, 0.25f, CapCollider.center.z);
isCrouching = true;
            Actions.Sitting(true);
        }

        Debug.DrawRay(transform.position, Vector3.up* 2f, Color.green);
        if (isCrouching && !crouchbutton)
        {
            //try to stand up
            var cantStandUp = Physics.Raycast(transform.position, Vector3.up, 2f);

            if (!cantStandUp)
            {
                CapCollider.height = 1f;
                CapCollider.center = new Vector3(CapCollider.center.x, 0.5f, CapCollider.center.z);
isCrouching = false;
                Actions.Sitting(false);
            }
        }
        */
#endregion