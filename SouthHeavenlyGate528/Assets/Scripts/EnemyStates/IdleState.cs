using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }


    public void OnEnter()
    {
        parameter.animator.Play("EnemyIdle");
    }
    public void OnUpdate()
    {
        if(manager.parameter.getHit)
        {
            manager.TransitionState(StateType.Hit);
        }

        timer += Time.deltaTime;

        if (parameter.target != null &&
           parameter.target.position.x >= parameter.chasePoints[0].position.x &&
            parameter.target.position.x <= parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.React);
        }

        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);
        }
    }

    public void OnExit()
    {
        timer = 0;
        parameter.animator.StopPlayback();
    }
}