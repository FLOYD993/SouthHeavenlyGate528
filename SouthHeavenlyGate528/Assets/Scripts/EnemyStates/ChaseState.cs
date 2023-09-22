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
        Debug.Log("Chase Start");
        parameter.animator.Play("EnemyRunning");
    }
    public void OnUpdate()
    {

        Debug.Log(parameter.target);
        manager.FlipTo(parameter.target);
        if (parameter.target)
        {
            manager.transform.position = Vector3.MoveTowards(manager.transform.position,
                                                        parameter.target.position,
                                                        parameter.chaseSpeed * Time.deltaTime);
        }

        if (parameter.target == null ||
            manager.transform.position.x < parameter.chasePoints[0].position.x ||
            manager.transform.position.x > parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.Idle);
        }

        Collider[] colliders = Physics.OverlapSphere(parameter.attackPoint.position, parameter.attackArea, parameter.targetLayer);

        if (colliders.Length != 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("count");
                    manager.TransitionState(StateType.Attack);
                    break;
                }
            }
        }
    }

    public void OnExit()
    {

    }
}
