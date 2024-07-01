using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player ins;
    [SerializeField] private DynamicJoystick joystick;
    public float moveSpeed;
    private bool isEnemy;
    private IState<Player> currentState;

    //Param movement
    public Vector3 movementDirection;
    public bool canAttack;

    public bool isWin;

    private int pantPlayerToUse;
    public int rankPlayer;

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
        NameOfCharacter = DataManager.ins.dt.namePlayer;
        isWin = false;
        CountScale = 0;
        weaponBoosted = false;
        boosterVFX.SetActive(false);
        skinCharacterList[SetSkinToUse].transform.localScale = startTransformPlayer.localScale;
        transform.position = Constain.START_POINT;
        SetupSkin();
        ChangeState(new PIdleState());
        if(NameOfCharacter == null)
        {
            NameOfCharacter = "You";
        }    
    }

    public void SetupSkin()
    {
        WeaponToUse = DataManager.ins.dt.idWeapon;
        PantToUse = DataManager.ins.dt.idPant;
        SetSkinToUse = DataManager.ins.dt.idSetSkin;
        SetHat(DataManager.ins.dt.idHat);
        SetPant(PantToUse);
        SetSkin(SetSkinToUse);
        SetKhien(KhienToUse);
        SetupWeapon();
        weaponPoint = weaponPointList[SetSkinToUse];
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
        if (isWin)
        {
            movementDirection = Vector3.zero;
            return;
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
        ChangeState(new PDeadState());
        return;
    }

    public void ResetName()
    {
        PoolingTarget.ins.EnQueueObj(Constain.TAG_TARGET, targetE);
    }    

    public bool IsWin
    {
        get { return isWin; }
        set { isWin = value; }
    }

    public int RankPlayer
    {
        get { return rankPlayer; }
        set { rankPlayer = value; }
    }

    public void SetnameInput(string s)
    {
        NameOfCharacter = s;
        DataManager.ins.dt.namePlayer = NameOfCharacter;
    }    

    public Vector3 PositionPlayer() => transform.position;    
}
