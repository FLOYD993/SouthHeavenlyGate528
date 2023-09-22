using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;

    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("EnemyAttack");
    }
    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if (manager.parameter.getHit)
        {
            manager.TransitionState(StateType.Hit);
        }

        if (info.normalizedTime >= 0.95F)
        {
            parameter.animator.StopPlayback(); // Í£Ö¹EnemyReact¶¯»­
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {
        parameter.animator.StopPlayback();
    }
}
