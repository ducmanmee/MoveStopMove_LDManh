using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent agent;
    private IState<Enemy> currentState;
    private bool isMoving;
    [SerializeField] private GameObject aim;
    private Vector3 target;

    private void Start()
    {
        OnInit();
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
        ChangeState(new IdleState());
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constain.TAG_ATTACKRANGE))
        {
            Player.Instance.AddCharacterInRange(this);
        }

        if (other.CompareTag(Constain.TAG_WEAPON))
        {
            ChangeAnim(Constain.ANIM_DEAD);
            OnDespawn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constain.TAG_ATTACKRANGE))
        {
            SetAim(false);
            Player.Instance.RemoveCharacterInRange(this);
        }
    }

    public override void SetAim(bool active)
    {
        aim.SetActive(active);
    }
}
