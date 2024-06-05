using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent agent;
    private IState<Enemy> currentState;
    [SerializeField] private GameObject aim;
    private Vector3 target;

    private void Start()
    {
        
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Enemy> newState)
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

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        WeaponToUse = Random.Range(0, 11);
        PantToUse = Random.Range(0, 8);
        HatToUse = Random.Range(0, 9);
        SetPant(PantToUse);
        SetHat(HatToUse);
        SetupWeapon();
    }

    public override void Dead()
    {
        base.Dead();
        StartCoroutine(OnDead());
    }

    public IEnumerator OnDead()
    {
        ChangeState(new DeadState());
        GameManager.ins.RemoveEnemy(this);
        yield return new WaitForSeconds(Constain.TIMER_DEAD);
        GameManager.ins.CheckWin();
        PoolingEnemy.ins.EnQueueObj(Constain.TAG_ENEMY, this);
    }


    public override void Moving()
    {
        ChangeAnim(Constain.ANIM_RUN);
        agent.SetDestination(target);
        if(Vector3.Distance(transform.position, target) < .1f)
        {
            ChangeState(new IdleState());
        }    
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void StopMoving()
    {

    }

    public void RandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        target = transform.position + randomDirection * 6f;
        target.y = 0f;
        transform.LookAt(target);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constain.TAG_ATTACKRANGE))
        {
            SetAim(false);
        }
    }

    public override void SetAim(bool active)
    {
        aim.SetActive(active);
    }
}
