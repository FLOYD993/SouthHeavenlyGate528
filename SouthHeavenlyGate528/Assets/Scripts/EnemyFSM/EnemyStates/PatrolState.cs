using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private FSM manager;
    private Parameter parameter;

    private int patrolPosition;

    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("EnemyMoving");
    }
    public void OnUpdate()
    {
        manager.FlipTo(parameter.patrolPoints[patrolPosition]);

        if (parameter.getHit)
        {
            manager.TransitionState(StateType.Hit);
        }

        manager.transform.position = Vector3.MoveTowards(manager.transform.position,
                                                        parameter.patrolPoints[patrolPosition].position,
                                                        parameter.moveSpeed * Time.deltaTime);

        if (parameter.target != null &&
            parameter.target.position.x >= parameter.chasePoints[0].position.x &&
            parameter.target.position.x <= parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.React);
        }

        if (Vector3.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < 0.1F)
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        patrolPosition++;
        if (patrolPosition >= parameter.patrolPoints.Length)
        {
            patrolPosition = 0;
        }

        parameter.animator.StopPlayback();
    }
}
