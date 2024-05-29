using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player ins;
    [SerializeField] private FixedJoystick joystick;
    public float moveSpeed;
    private bool isEnemy;
    private IState<Player> currentState;

    //Param movement
    public Vector3 movementDirection;
    public bool canAttack;



    private int pantPlayerToUse;

    private void MakeInstance()
    {
        if(ins == null)
        {
            ins = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
        startTransformPlayer = transform;
        startAttackPoint = attackPoint;
        
        
    }
    private void Update()
    {
        if (GameManager.ins.GetGameState() is LoseState) return;      
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
        skinCharacterList[SetSkinToUse].transform.localScale = startTransformPlayer.localScale;
        transform.position = Constain.START_POINT;
        WeaponToUse = DataManager.ins.playerData.idWeapon;
        PantToUse = DataManager.ins.playerData.idPant;
        SetSkinToUse = DataManager.ins.playerData.idSetSkin;
        SetHat(DataManager.ins.playerData.idHat);
        SetPant(PantToUse);
        SetSkin(SetSkinToUse);
        SetupWeapon();
        weaponPoint = weaponPointList[SetSkinToUse];
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
        if(isDead)
        {
            Dead();
        }    
        movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            isMoving = true;
            ChangeState(new PMoveState());
        }
        else
        {
            isMoving = false;
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
    public Quaternion PlayerRotation
    {
        get { return transform.rotation; } 
        set { transform.rotation = value; } 
    }

    public override void Dead()
    {
        base.Dead();
        canAttack = false;
        GameManager.ins.ChangeState(new LoseState());
        ChangeState(new PDeadState());
        return;
    }    

}
