using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadState : IState 
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;


    public DeadState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("EnemyDead");

        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= 0.95F)
        {
            parameter.animator.StopPlayback();
        }

        manager.Fade();
    }
    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        
    }
}
