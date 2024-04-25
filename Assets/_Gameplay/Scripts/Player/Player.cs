using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance;
    [SerializeField] private FixedJoystick joystick;
    public float moveSpeed;
    private bool isEnemy;
    private IState<Player> currentState;

    //Param movement
    public Vector3 movementDirection;
    public bool canAttack;

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

    private void Update()
    {
        Moving();
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        NearestEnemy();
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public void ChangeState(IState<Player> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public override void Moving()
    {
        movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            ChangeState(new PMoveState());
        }
        else
        {
            if(currentState is PMoveState)
            {
                ChangeState(new PIdleState());
            }
            if(nearestCharacter != null && canAttack)
            {
                ChangeState(new PAttackState());
            }
                
        }
    }

    public override void Attack()
    {
        base.Attack();
    }  

    public Vector3 GetDirectionToAim() => directionToCharacter;
}
