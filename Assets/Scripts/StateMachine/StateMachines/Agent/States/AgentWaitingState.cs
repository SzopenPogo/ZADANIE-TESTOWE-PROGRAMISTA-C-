using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWaitingState : AgentBaseState
{
    //Animation
    private readonly int IdleAnimationHash = Animator.StringToHash("Idle");
    private const string IdleAnimationTag = "Idle";
    private const float CrossFadeDuration = 0.2f;

    public AgentWaitingState(AgentStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //State Info
        stateMachine.StateInfo.SetCurrentStateInfo(StateType.Idle);

        //Animation
        stateMachine.Animator.CrossFadeInFixedTime(IdleAnimationHash, CrossFadeDuration);

        //Subs
        stateMachine.Combat.OnAttackEvent += SetAttackState;

        //Reste Nav Mesh Agent Path
        if (stateMachine.NavMeshAgent.hasPath)
            stateMachine.NavMeshAgent.ResetPath();
    }

    public override void Exit()
    {
        //Subs
        stateMachine.Combat.OnAttackEvent -= SetAttackState;
    }

    public override void FixedTick(float deltaTime)
    {

    }

    public override void LateTick(float deltaTime)
    {

    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Combat.TryAttackEnemy();
    }
}
