using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPatrolState : AgentBaseState
{
    //Animation
    private readonly int MovementBlendTreeHash = Animator.StringToHash("Movement Blend Tree");
    private readonly int MovementSpeedHash = Animator.StringToHash("Speed");
    private const float CrossFadeDuration = 0.2f;

    public AgentPatrolState(AgentStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //State Info
        stateMachine.StateInfo.SetCurrentStateInfo(StateType.Patrol);

        //Values
        SetMovementSpeed(stateMachine.MovementSpeed);

        //Animation
        stateMachine.Animator.CrossFadeInFixedTime(MovementBlendTreeHash, CrossFadeDuration);
        stateMachine.Animator.SetFloat(MovementSpeedHash, GetMovementSpeedPercent());

        //Subs
        stateMachine.Combat.OnWaitingEvent += SetWaitingState;
        stateMachine.Combat.OnAttackEvent += SetAttackState;
        stateMachine.Health.OnDie += SetDeathState;
    }

    public override void Exit()
    {
        //Subs
        stateMachine.Combat.OnWaitingEvent -= SetWaitingState;
        stateMachine.Combat.OnAttackEvent -= SetAttackState;
        stateMachine.Health.OnDie -= SetDeathState;
    }

    public override void FixedTick(float deltaTime)
    {
    }

    public override void LateTick(float deltaTime)
    {
    }

    public override void Tick(float deltaTime)
    {
        AvoidRunningInPlace();
        MoveToDestination();
    }

    private void AvoidRunningInPlace()
    {
        if (!IsNavMeshAgentRunningInPlace())
            return;

        if (!stateMachine.NavMeshAgent.hasPath)
            return;

        stateMachine.NavMeshAgent.ResetPath();
    }

    private void MoveToDestination()
    {
        if (stateMachine.NavMeshAgent.hasPath)
            return;

        SetDestination(Area.Instance.GetRandomPointInActiveArea());
    }
}
