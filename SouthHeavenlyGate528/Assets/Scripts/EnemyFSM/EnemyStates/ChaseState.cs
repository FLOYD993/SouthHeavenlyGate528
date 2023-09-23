using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;


    public ChaseState(FSM manager)
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
        manager.FlipTo(parameter.target);

        if (manager.parameter.getHit)
        {
            manager.TransitionState(StateType.Hit);
        }

        if (parameter.target)
        {
            manager.transform.position = Vector3.MoveTowards(manager.transform.position,
                                                             new Vector3(parameter.target.position.x, 0, 0),
                                                             parameter.chaseSpeed * Time.deltaTime);
        }

        if (parameter.target == null ||
            manager.transform.position.x < parameter.chasePoints[0].position.x ||
            manager.transform.position.x > parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.Idle);
        }

        Collider[] colliders = Physics.OverlapSphere(parameter.attackPoint.position, parameter.attackArea, parameter.targetLayer);

        // Debug.Log(colliders.Length);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log(collider.tag);
                manager.TransitionState(StateType.Attack);

                // break;
            }


        }
    }

    public void OnExit()
    {
        parameter.animator.StopPlayback();
    }
}
