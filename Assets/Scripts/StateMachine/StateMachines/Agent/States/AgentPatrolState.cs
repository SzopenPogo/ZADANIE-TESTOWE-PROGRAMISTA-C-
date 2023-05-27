using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPatrolState : AgentBaseState
{
    //Animation
    private readonly int MovementBlendTreeHash = Animator.StringToHash("Movement Blend Tree");
    private readonly int MovementSpeedHash = Animator.StringToHash("Speed");
    private const float AnimatorDampTime = 0.075f;
    private const float CrossFadeDuration = 0.2f;

    public AgentPatrolState(AgentStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Values
        SetMovementSpeed(stateMachine.MovementSpeed);

        //Animation
        stateMachine.Animator.CrossFadeInFixedTime(MovementBlendTreeHash, CrossFadeDuration);
        stateMachine.Animator.SetFloat(MovementSpeedHash, GetMovementSpeedPercent());
    }

    public override void Exit()
    {
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

        SetDestination(AgentSpawner.Instance.GetRandomPointInAgentArea());
    }
}
