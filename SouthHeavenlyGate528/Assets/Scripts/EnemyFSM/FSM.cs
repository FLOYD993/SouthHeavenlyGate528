using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

/// <summary>
/// 敌人所有状态
/// </summary>
public enum StateType
{
    Idle,           // 站立（闲置）
    Patrol,         // 巡航
    Chase,          // 追踪
    React,          // 发现玩家，反应
    Attack,         // 攻击
    Hit,            // 受到攻击
    Dead            // 死亡
}

[Serializable]      // 使该类各参数可以在细节面板中显示
public class Parameter
{
    public int health;                          // 血量
    public float moveSpeed;                     // 巡航速度
    public float chaseSpeed;                    // 追踪速度
    public float idleTime;                      // 站立时间

    public Transform[] patrolPoints;            // 巡逻范围
    public Transform[] chasePoints;             // 追击范围
    
    public Transform target;                    // 追踪目标
    public LayerMask targetLayer;               // 追踪目标层级，通常为Player

    public Transform attackPoint;               // 攻击点（攻击范围圆心）
    public float attackArea;                    // 攻击范围
    
    public Animator animator;                   // 动画控制器

    public bool getHit;                         // 是否收到伤害，用于调试
}

public class FSM : MonoBehaviour
{
    public Parameter parameter;         // 数值集

    private IState currentState;        // 状态
    public IState CurrentState { get => currentState; set => currentState = value; }

    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();         // 状态集字典，<key: 状态, value: 状态控制>



    private void Start()
    {
        // 注册状态
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.React, new ReactState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.Hit, new HitState(this));
        states.Add(StateType.Dead, new DeadState(this));

        // 初始状态为站立
        TransitionState(StateType.Idle);

        // 在外部赋值了，这里不再查找
        // parameter.animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 当前状态存在
        if (CurrentState != null)
        {
            CurrentState.OnUpdate();            // 执行当前状态的OnUpdate方法
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            parameter.getHit = true;
        }
    }

    public void TransitionState(StateType state)
    {


        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }
        CurrentState = states[state];
        CurrentState.OnEnter();


    }

    public void FlipTo(Transform target)
    {
        // Debug.Log(target.position);
        if (target != null)
        {
            // Debug.Log(transform.position.x + "/t" + target.position.x);
            if(transform.position .x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(transform.position.x < target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            parameter.target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = null;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.attackArea);
    }

    public void Fade()
    {
        Destroy(transform.parent.gameObject, 5.0F) ;
    }
}






