using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent agent;
    private IState<Enemy> currentState;
    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject animE;
    private Vector3 target;
    private float scaleEnemy;

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
        NameOfCharacter = NameCharacter.GetRandomName();
        WeaponToUse = Random.Range(0, 11);
        PantToUse = Random.Range(0, 8);
        HatToUse = Random.Range(0, 9);
        //scaleEnemy = Random.Range(0f, .5f);
        //ScaleCharacter(animE.transform, scaleEnemy);
        SetPant(PantToUse);
        SetHat(HatToUse);
        SetupWeapon();
        ActiveName();
    }  

    public override void Dead()
    {
        base.Dead();
        GameManager.ins.RemoveEnemy(this);
        StartCoroutine(OnDead());
    }

    public IEnumerator OnDead()
    {
        ChangeState(new DeadState());
        yield return new WaitForSeconds(Constain.TIMER_DEAD);   
        PoolingEnemy.ins.EnQueueObj(Constain.TAG_ENEMY, this);
        PoolingTarget.ins.EnQueueObj(Constain.TAG_TARGET, targetE);
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

    public void RandomPosition()
    {
        Vector3 randomDirection;
        do
        {
            float randomX = Random.Range(-37, 37);
            float randomZ = Random.Range(-30, 30);
            randomDirection = new Vector3(randomX, 0, randomZ);
        } while (Vector3.Distance(transform.position, randomDirection) > 5f);

        target = randomDirection;
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
