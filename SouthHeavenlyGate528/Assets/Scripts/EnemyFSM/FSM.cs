using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

/// <summary>
/// ��������״̬
/// </summary>
public enum StateType
{
    Idle,           // վ�������ã�
    Patrol,         // Ѳ��
    Chase,          // ׷��
    React,          // ������ң���Ӧ
    Attack,         // ����
    Hit,            // �ܵ�����
    Dead            // ����
}

[Serializable]      // ʹ���������������ϸ���������ʾ
public class Parameter
{
    public int health;                          // Ѫ��
    public float moveSpeed;                     // Ѳ���ٶ�
    public float chaseSpeed;                    // ׷���ٶ�
    public float idleTime;                      // վ��ʱ��

    public Transform[] patrolPoints;            // Ѳ�߷�Χ
    public Transform[] chasePoints;             // ׷����Χ
    
    public Transform target;                    // ׷��Ŀ��
    public LayerMask targetLayer;               // ׷��Ŀ��㼶��ͨ��ΪPlayer

    public Transform attackPoint;               // �����㣨������ΧԲ�ģ�
    public float attackArea;                    // ������Χ
    
    public Animator animator;                   // ����������

    public bool getHit;                         // �Ƿ��յ��˺������ڵ���
}

public class FSM : MonoBehaviour
{
    public Parameter parameter;         // ��ֵ��

    private IState currentState;        // ״̬
    public IState CurrentState { get => currentState; set => currentState = value; }

    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();         // ״̬���ֵ䣬<key: ״̬, value: ״̬����>



    private void Start()
    {
        // ע��״̬
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.React, new ReactState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.Hit, new HitState(this));
        states.Add(StateType.Dead, new DeadState(this));

        // ��ʼ״̬Ϊվ��
        TransitionState(StateType.Idle);

        // ���ⲿ��ֵ�ˣ����ﲻ�ٲ���
        // parameter.animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // ��ǰ״̬����
        if (CurrentState != null)
        {
            CurrentState.OnUpdate();            // ִ�е�ǰ״̬��OnUpdate����
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






