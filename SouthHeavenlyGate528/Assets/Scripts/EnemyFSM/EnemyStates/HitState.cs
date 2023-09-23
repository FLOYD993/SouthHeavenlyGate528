using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info; 


    public HitState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("EnemyHit");
        parameter.health -= 1;
        manager.HealthBarUpdate();
    }
    public void OnUpdate()
    {
        if (parameter.health <= 0)
        {
            manager.TransitionState(StateType.Dead);
        }

        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= 0.95F)
        {
            parameter.target = GameObject.FindWithTag("Player").transform;

            manager.TransitionState(StateType.Chase);
        }

    }

    public void OnExit()
    {
        parameter.animator.StopPlayback();
        parameter.getHit = false;
        
    }


}
