using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private float moveSpeed;
    public LayerMask groundLayer;
    

    private void MakeInstance()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public override void OnInit()
    {
        base.OnInit();
    }    

    public override void Movement()
    {
        Vector3 movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        GetRigibody().velocity = new Vector3(joystick.Horizontal * moveSpeed, GetRigibody().velocity.y, joystick.Vertical * moveSpeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.forward = movementDirection;
            //transform.rotation = Quaternion.LookRotation(GetRigibody().velocity);
            ChangeAnim(Constain.ANIM_RUN); 
        }
        else
        {
            ChangeAnim(Constain.ANIM_IDLE);
        }
    }
}
