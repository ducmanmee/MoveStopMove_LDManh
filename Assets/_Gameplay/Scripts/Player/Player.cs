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
        OnInit();
        
    }
    private void Update()
    {
        if (GameManager.instance.currentState is LoseState) return;
        //Debug.Log(currentState);
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
        transform.position = Constain.START_POINT;
        ChangeState(new PIdleState());
    }

    public override void OnDespawn()
    {
        StartCoroutine(OnDead());
    }

    public IEnumerator OnDead()
    {
        yield return new WaitForSeconds(Constain.TIMER_DEAD+ 1f);
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
        if (isDead)
        {
            GameManager.instance.ChangeState(new LoseState());
            ChangeState(new PDeadState());
            return;
        }
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
